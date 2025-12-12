#!/usr/bin/env bash
set -euo pipefail

# Environment passed in from GitHub Actions (e.g. dev, uat, staging, prod)
ENV="${ENV}"

RG_NAME="edu-m-rg-${ENV}"
LOCATION="westeurope"
PLAN_NAME="edu-m-plan-${ENV}"
APPS=("enigma-api_edu-m-${ENV}" "chores-api_edu-m-${ENV}")

# SQL Server settings (shared across all envs)
SQL_SERVER_NAME="edu-m-sqlserver"
SQL_ADMIN_USER="sqladminuser"
SQL_ADMIN_PASS="${AZURE_SQL_PASSWORD}"   # injected from GitHub Actions secret
DB_NAME="code-${ENV}"

# 1. Resource group
az group create --name $RG_NAME --location $LOCATION

# 2. App Service Plan
az appservice plan show --name $PLAN_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az appservice plan create --name $PLAN_NAME --resource-group $RG_NAME --sku F1 --is-linux true

# 3. Web Apps
for APP in "${APPS[@]}"; do
  az webapp show --name $APP --resource-group $RG_NAME >/dev/null 2>&1 \
    || az webapp create --name $APP --resource-group $RG_NAME --plan $PLAN_NAME
done

# 4. SQL Server (shared across all envs)
az sql server show --name $SQL_SERVER_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az sql server create \
       --resource-group $RG_NAME \
       --location $LOCATION \
       --name $SQL_SERVER_NAME \
       --admin-user $SQL_ADMIN_USER \
       --admin-password $SQL_ADMIN_PASS

# 5. Database (one per env)
az sql db show --name $DB_NAME --server $SQL_SERVER_NAME --resource-group $RG_NAME >/dev/null 2>&1 \
  || az sql db create \
       --resource-group $RG_NAME \
       --server $SQL_SERVER_NAME \
       --name $DB_NAME \
       --edition Free
