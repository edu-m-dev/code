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

# 6. redis
REDIS_NAME="edu-m-redis-${ENV}"
REDIS_SKU="Basic"
REDIS_SIZE="C0"

az redis show --name $REDIS_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az redis create \
       --name $REDIS_NAME \
       --resource-group $RG_NAME \
       --location $LOCATION \
       --sku $REDIS_SKU \
       --vm-size $REDIS_SIZE

# 7. Application Insights
AI_NAME="edu-m-ai-${ENV}"

az monitor app-insights component show \
  --app $AI_NAME \
  --resource-group $RG_NAME >/dev/null 2>&1 \
  || az monitor app-insights component create \
       --app $AI_NAME \
       --location $LOCATION \
       --resource-group $RG_NAME \
       --application-type web