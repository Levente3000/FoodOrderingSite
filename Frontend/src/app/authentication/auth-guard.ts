import { Injectable } from '@angular/core';
import {
	ActivatedRouteSnapshot,
	Router,
	RouterStateSnapshot,
	UrlTree,
} from '@angular/router';
import { KeycloakAuthGuard, KeycloakService } from 'keycloak-angular';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard extends KeycloakAuthGuard {
	constructor(
		protected override router: Router,
		protected override keycloakAngular: KeycloakService
	) {
		super(router, keycloakAngular);
	}

	public async isAccessAllowed(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): Promise<boolean | UrlTree> {
		const loggedIn = this.keycloakAngular.isLoggedIn();

		if (!loggedIn) {
			await this.keycloakAngular.login({
				redirectUri: window.location.href,
			});
			return false;
		}

		const requiredRoles = (route.data['roles'] as string[] | undefined) ?? [];
		if (requiredRoles.length === 0) return true;

		const hasAll = requiredRoles.every(r => this.roles.includes(r));
		return hasAll ? true : this.router.parseUrl('/not-found');
	}
}
