import {
	APP_INITIALIZER,
	type EnvironmentProviders,
	importProvidersFrom,
	makeEnvironmentProviders,
} from '@angular/core';
import {
	KeycloakAngularModule,
	type KeycloakOptions,
	KeycloakService,
} from 'keycloak-angular';

/**
 * @returns provider for keycloak interceptor
 */
export function provideKeycloak(): EnvironmentProviders {
	return makeEnvironmentProviders([
		importProvidersFrom(KeycloakAngularModule),
		{
			provide: APP_INITIALIZER,
			useFactory: initializeKeycloak,
			multi: true,
			deps: [KeycloakService],
		},
	]);
}

/**
 * @param keycloak keycloak service to use
 * @returns keycloak initializer
 */
function initializeKeycloak(keycloak: KeycloakService) {
	return () =>
		keycloak.init(<KeycloakOptions>{
			config: {
				url: 'http://localhost:8090',
				realm: 'food-ordering-site',
				clientId: 'food-ordering-site',
			},
			initOptions: {
				onLoad: 'login-required',
        pkceMethod: 'S256',
        checkLoginIframe: false,
        redirectUri: window.location.href,
			},
      enableBearerInterceptor: true,
      bearerPrefix: 'Bearer',
      bearerExcludedUrls: ['/assets', '/favicon.ico'],
      loadUserProfileAtStartUp: false,
      shouldAddToken: () => true,
		});
}
