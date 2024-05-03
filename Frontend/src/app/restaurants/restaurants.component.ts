import { Component, OnInit } from '@angular/core';
import { FilterAndRestaurantsComponent } from '../home/filter-and-restaurants/filter-and-restaurants.component';
import { Restaurant } from '../model/restaurant/restaurant.model';
import { RestaurantService } from '../services/restaurant.service';
import { CategoryService } from '../services/category.service';

@Component({
	selector: 'app-restaurants',
	standalone: true,
	imports: [FilterAndRestaurantsComponent],
	templateUrl: './restaurants.component.html',
	styleUrl: './restaurants.component.scss',
})
export class RestaurantsComponent implements OnInit {
	protected allRestaurants: Restaurant[] = [];

	constructor(private restaurantService: RestaurantService) {}

	public ngOnInit() {
		this.restaurantService.getRestaurantsWithLogo().subscribe(restaurants => {
			this.allRestaurants = restaurants;
		});
	}
}
