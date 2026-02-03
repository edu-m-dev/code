export const msalConfig = {
  auth: {
    clientId: '697b17b5-9bcc-4504-b514-ec55f55e2f26',
    authority: 'https://login.microsoftonline.com/4ff270ac-352c-404c-ac7e-d6280a937f61',
    redirectUri: window.location.origin
  },
  cache: {
    cacheLocation: 'localStorage',
    storeAuthStateInCookie: false
  }
};

// Scopes requested for access tokens. Using backend ApiScope from chores.webapi/appsettings.json
export const loginRequest = {
  scopes: [
    'openid',
    'profile',
    'api://07d90b1e-4149-4147-ad04-382cd0c9c6f8/api.read',
    'api://07d90b1e-4149-4147-ad04-382cd0c9c6f8/api.write'
  ]
};
