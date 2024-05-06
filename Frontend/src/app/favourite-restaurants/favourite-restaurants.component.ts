import { Component, OnInit } from '@angular/core';
import { FilterAndRestaurantsComponent } from '../home/filter-and-restaurants/filter-and-restaurants.component';
import { Restaurant } from '../model/restaurant/restaurant.model';
import { RestaurantService } from '../services/restaurant.service';

@Component({
	selector: 'app-favourite-restaurants',
	standalone: true,
	imports: [FilterAndRestaurantsComponent],
	templateUrl: './favourite-restaurants.component.html',
	styleUrl: './favourite-restaurants.component.scss',
})
export class FavouriteRestaurantsComponent implements OnInit {
	protected favouriteRestaurants: Restaurant[] = [];

	constructor(private restaurantService: RestaurantService) {}

	public ngOnInit() {
		this.restaurantService
			.getFavouriteRestaurantsWithLogo()
			.subscribe(restaurants => {
				this.favouriteRestaurants = restaurants;
			});
	}
}
