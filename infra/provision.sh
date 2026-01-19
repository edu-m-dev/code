#!/usr/bin/env bash
set -euo pipefail

# Environment passed in from GitHub Actions (e.g. dev, uat, staging, prod)
ENV="${ENV}"

RG_NAME="edu-m-rg-${ENV}"
LOCATION="${AZURE_REGION}"
PLAN_NAME="edu-m-plan-${ENV}"
APPS=("enigma-api-edu-m-${ENV}" "chores-api-edu-m-${ENV}")

# SQL Server settings (one per env)
SQL_SERVER_NAME="edu-m-sqlserver-${ENV}"
SQL_ADMIN_USER="sqladminuser"
SQL_ADMIN_PASS="${AZURE_SQL_PASSWORD}"   # injected from GitHub Actions secret
DB_NAME="code"

# 1. Resource group
az group create --name $RG_NAME --location $LOCATION

# 2. App Service Plan
az appservice plan show --name $PLAN_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az appservice plan create --name $PLAN_NAME --resource-group $RG_NAME --sku F1 --is-linux

# 3. Web Apps
for APP in "${APPS[@]}"; do
  az webapp show --name $APP --resource-group $RG_NAME >/dev/null 2>&1 \
    || az webapp create --name $APP --resource-group $RG_NAME --plan $PLAN_NAME --runtime "${DOTNET_RUNTIME}"
done

# 4. SQL Server (unique per env)
az sql server show --name $SQL_SERVER_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az sql server create \
       --resource-group $RG_NAME \
       --location $LOCATION \
       --name $SQL_SERVER_NAME \
       --admin-user $SQL_ADMIN_USER \
       --admin-password $SQL_ADMIN_PASS

# 4b. Firewall rule (allow all IPs)
az sql server firewall-rule show --name allow-all-ips --server $SQL_SERVER_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az sql server firewall-rule create \
       --resource-group $RG_NAME \
       --server $SQL_SERVER_NAME \
       --name allow-all-ips \
       --start-ip-address 0.0.0.0 \
       --end-ip-address 255.255.255.255

# 5. Database (always named 'code')
az sql db show --name $DB_NAME --server $SQL_SERVER_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az sql db create \
       --resource-group $RG_NAME \
       --server $SQL_SERVER_NAME \
       --name $DB_NAME \
       --edition Free

# 6. Redis (FREE via Azure Container Apps)
CAE_NAME="edu-m-cae-${ENV}"
REDIS_APP_NAME="edu-m-redis-${ENV}"
REDIS_IMAGE="redis:7-alpine"

# 6a. Container Apps Environment (with explicit Log Analytics workspace)
WORKSPACE_NAME="edu-m-law-${ENV}"

# Create workspace if missing
az monitor log-analytics workspace show \
  --resource-group $RG_NAME \
  --workspace-name $WORKSPACE_NAME >/dev/null 2>&1 \
  || az monitor log-analytics workspace create \
       --resource-group $RG_NAME \
       --workspace-name $WORKSPACE_NAME \
       --location $LOCATION

# Retrieve workspace ID + key
WORKSPACE_ID=$(az monitor log-analytics workspace show \
  --resource-group $RG_NAME \
  --workspace-name $WORKSPACE_NAME \
  --query customerId -o tsv)

WORKSPACE_KEY=$(az monitor log-analytics workspace get-shared-keys \
  --resource-group $RG_NAME \
  --workspace-name $WORKSPACE_NAME \
  --query primarySharedKey -o tsv)

# Create Container Apps Environment using the explicit workspace
az containerapp env show \
  --name $CAE_NAME \
  --resource-group $RG_NAME >/dev/null 2>&1 \
  || az containerapp env create \
       --name $CAE_NAME \
       --resource-group $RG_NAME \
       --location $LOCATION \
       --logs-workspace-id $WORKSPACE_ID \
       --logs-workspace-key $WORKSPACE_KEY

# 6b. Redis Container App
az containerapp show \
  --name $REDIS_APP_NAME \
  --resource-group $RG_NAME >/dev/null 2>&1 \
  || az containerapp create \
       --name $REDIS_APP_NAME \
       --resource-group $RG_NAME \
       --environment $CAE_NAME \
       --image $REDIS_IMAGE \
       --target-port 6379 \
       --ingress internal \
       --min-replicas 1 \
       --max-replicas 1

# 6c. Get internal Redis URL
REDIS_HOST=$(az containerapp show \
  --name $REDIS_APP_NAME \
  --resource-group $RG_NAME \
  --query "properties.configuration.ingress.fqdn" -o tsv)

echo "Redis internal hostname: $REDIS_HOST"

# 7. Application Insights
AI_NAME="edu-m-ai-${ENV}"

az monitor app-insights component show \
  --app $AI_NAME \
  --resource-group $RG_NAME >/dev/null 2>&1 \
  || az monitor app-insights component create \
       --app $AI_NAME \
       --location $LOCATION \
       --resource-group $RG_NAME \
       --workspace $WORKSPACE_ID \
       --application-type web

# 8. Create a €1 monthly budget that alerts at 1% (~€0.01)

SUBSCRIPTION_ID=$(az account show --query id -o tsv)
BUDGET_NAME="edu-m-budget-${ENV}"
START_DATE="$(date +%Y-%m-01)"
END_DATE="2100-01-01"

az consumption budget show \
  --budget-name $BUDGET_NAME \
  --subscription $SUBSCRIPTION_ID >/dev/null 2>&1 \
  || az consumption budget create \
       --amount 1 \
       --budget-name $BUDGET_NAME \
       --category cost \
       --time-grain monthly \
       --start-date $START_DATE \
       --end-date $END_DATE \
       --subscription $SUBSCRIPTION_ID \
       --notifications actual_greater_than_1percent='{
         "enabled": true,
         "operator": "GreaterThan",
         "threshold": 1,
         "contactEmails": ["'"${AZURE_ALERT_EMAIL}"'"]
       }'