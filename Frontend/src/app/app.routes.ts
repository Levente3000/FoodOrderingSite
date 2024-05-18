import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RestaurantDetailsComponent } from './restaurant-details/restaurant-details.component';
import { AuthGuard } from './authentication/auth-guard';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { CreateEditRestaurantComponent } from './create-edit-restaurant/create-edit-restaurant.component';
import { CreateEditProductComponent } from './restaurant-details/create-edit-product/create-edit-product.component';
import { ProfileComponent } from './profile/profile.component';
import { RestaurantActiveOrdersComponent } from './restaurant-active-orders/restaurant-active-orders.component';
import { RestaurantInactiveOrdersComponent } from './restaurant-inactive-orders/restaurant-inactive-orders.component';
import { CategoryRestaurantsComponent } from './category-restaurants/category-restaurants.component';
import { RestaurantsComponent } from './restaurants/restaurants.component';
import { FavouriteRestaurantsComponent } from './favourite-restaurants/favourite-restaurants.component';
import { PageNotFoundComponent } from './shared/page-not-found/page-not-found.component';

export const routes: Routes = [
	{
		path: '',
		component: HomeComponent,
	},
	{ path: 'home', component: HomeComponent },
	{ path: 'profile', component: ProfileComponent },
	{ path: 'restaurants/details/:id', component: RestaurantDetailsComponent },
	{ path: 'restaurants', component: RestaurantsComponent },
	{ path: 'favourite-restaurants', component: FavouriteRestaurantsComponent },
	{
		path: 'restaurant-orders/active/:id',
		component: RestaurantActiveOrdersComponent,
		canActivate: [AuthGuard],
		data: { roles: ['RESTAURANT_OWNER'] },
	},
	{
		path: 'restaurant-orders/inactive/:id',
		component: RestaurantInactiveOrdersComponent,
		canActivate: [AuthGuard],
		data: { roles: ['RESTAURANT_OWNER'] },
	},
	{
		path: 'category-restaurants/:category',
		component: CategoryRestaurantsComponent,
	},
	{ path: 'shopping-cart', component: ShoppingCartComponent },
	{ path: 'create-restaurant', component: CreateEditRestaurantComponent },
	{
		path: 'edit-restaurant/:id',
		component: CreateEditRestaurantComponent,
		canActivate: [AuthGuard],
		data: { roles: ['RESTAURANT_OWNER'] },
	},
	{
		path: 'create-product/:restaurantId',
		component: CreateEditProductComponent,
		canActivate: [AuthGuard],
		data: { roles: ['RESTAURANT_OWNER'] },
	},
	{
		path: 'edit-product/:restaurantId/:productId',
		component: CreateEditProductComponent,
		canActivate: [AuthGuard],
		data: { roles: ['RESTAURANT_OWNER'] },
	},
	{ path: '**', component: PageNotFoundComponent },
];
