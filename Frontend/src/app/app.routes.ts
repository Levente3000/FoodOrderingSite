import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { RestaurantDetailsComponent } from './restaurant-details/restaurant-details.component';
import { AuthGuard } from './authentication/auth-guard';

export const routes: Routes = [
	{
		path: '',
		component: HomeComponent,
		canActivate: [AuthGuard],
		// data: { roles: ['ADMIN'] },
	},
	{ path: 'home', component: HomeComponent },
	{ path: 'restaurants/details/:id', component: RestaurantDetailsComponent },
	{ path: '**', component: PageNotFoundComponent },
];
