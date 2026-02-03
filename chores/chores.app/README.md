# Chores App (Angular)

Quick start:

```bash
cd chores/chores.app
npm install
npm start
```

Dev proxy: `proxy.conf.json` forwards `/api` to `http://localhost:5000` (adjust port to match `chores.webapi` run port).

Auth: backend uses Azure AD; for local development either run without auth (use test handlers from `chores.webapi.tests`) or integrate MSAL and request scopes based on `AzureAd:ApiScope`.
