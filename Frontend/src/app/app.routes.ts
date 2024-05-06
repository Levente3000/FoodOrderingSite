import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
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

export const routes: Routes = [
	{
		path: '',
		component: HomeComponent,
		canActivate: [AuthGuard],
		// data: { roles: ['ADMIN'] },
	},
	{ path: 'home', component: HomeComponent },
	{ path: 'profile', component: ProfileComponent },
	{ path: 'restaurants/details/:id', component: RestaurantDetailsComponent },
	{ path: 'restaurants', component: RestaurantsComponent },
	{ path: 'favourite-restaurants', component: FavouriteRestaurantsComponent },
	{
		path: 'restaurant-orders/active/:id',
		component: RestaurantActiveOrdersComponent,
	},
	{
		path: 'restaurant-orders/inactive/:id',
		component: RestaurantInactiveOrdersComponent,
	},
	{
		path: 'category-restaurants/:category',
		component: CategoryRestaurantsComponent,
	},
	{ path: 'shopping-cart', component: ShoppingCartComponent },
	{ path: 'create-restaurant', component: CreateEditRestaurantComponent },
	{ path: 'edit-restaurant/:id', component: CreateEditRestaurantComponent },
	{
		path: 'create-product/:restaurantId',
		component: CreateEditProductComponent,
	},
	{
		path: 'edit-product/:restaurantId/:productId',
		component: CreateEditProductComponent,
	},
	{ path: '**', component: PageNotFoundComponent },
];
