import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {
	provideHttpClient,
	withInterceptorsFromDi,
} from '@angular/common/http';
import { provideKeycloak } from './authentication/keycloak-initializer';
import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';

export const appConfig: ApplicationConfig = {
	providers: [
		provideRouter(routes),
		provideAnimationsAsync(),
		provideHttpClient(withInterceptorsFromDi()),
    importProvidersFrom(KeycloakAngularModule),
		provideKeycloak(),
		KeycloakService,
	],
};
