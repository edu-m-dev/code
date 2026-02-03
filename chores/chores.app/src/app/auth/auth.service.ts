import { Injectable } from '@angular/core';
import { PublicClientApplication, SilentRequest, AuthenticationResult } from '@azure/msal-browser';
import { msalConfig, loginRequest } from './auth.config';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private pca = new PublicClientApplication(msalConfig as any);
  private initialized: Promise<void> | null = null;

  private ensureInitialized(): Promise<void> {
    if (!this.initialized) {
      this.initialized = this.pca.initialize();
    }
    return this.initialized;
  }

  async login(): Promise<AuthenticationResult> {
    await this.ensureInitialized();
    try {
      const result = await this.pca.loginPopup(loginRequest as any);
      if (result && result.account) {
        this.pca.setActiveAccount(result.account);
      }
      return result as AuthenticationResult;
    } catch (err) {
      throw err;
    }
  }

  logout(): Promise<void> {
    const account = this.pca.getActiveAccount();
    // ensure initialized before logout
    const ready = this.ensureInitialized();
    return ready.then(() => {
      if (account) {
        return this.pca.logoutPopup({ account }).then(() => {});
      }
      return Promise.resolve();
    });
  }

  async getToken(): Promise<string | null> {
    await this.ensureInitialized();
    const account = this.pca.getActiveAccount();
    const silentRequest: SilentRequest = {
      scopes: loginRequest.scopes,
      account: account || undefined
    };

    try {
      const response = await this.pca.acquireTokenSilent(silentRequest as any);
      return response.accessToken;
    } catch (e) {
      // fall back to interactive popup if silent fails
      const response = await this.pca.acquireTokenPopup(loginRequest as any);
      if (response && response.account) this.pca.setActiveAccount(response.account);
      return response.accessToken;
    }
  }

  getAccountUsername(): string | null {
    const account = this.pca.getActiveAccount();
    return account?.username ?? null;
  }
}
