import { importProvidersFrom } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { AppComponent } from './app.component';
import { routes } from './app.routes';

export const appProviders = [
  importProvidersFrom(),
  provideAnimations(),
  provideHttpClient()
];
