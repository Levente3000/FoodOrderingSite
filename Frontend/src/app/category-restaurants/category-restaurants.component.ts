import { Component, OnInit } from '@angular/core';
import { Restaurant } from '../model/restaurant/restaurant.model';
import { RestaurantService } from '../services/restaurant.service';
import { ActivatedRoute } from '@angular/router';
import { FilterAndRestaurantsComponent } from '../home/filter-and-restaurants/filter-and-restaurants.component';

@Component({
	selector: 'app-category-restaurants',
	standalone: true,
	imports: [FilterAndRestaurantsComponent],
	templateUrl: './category-restaurants.component.html',
	styleUrl: './category-restaurants.component.scss',
})
export class CategoryRestaurantsComponent implements OnInit {
	protected restaurantsByCategory: Restaurant[] = [];

	constructor(
		private restaurantService: RestaurantService,
		private activatedRoute: ActivatedRoute
	) {}

	public ngOnInit() {
		this.activatedRoute.params.subscribe(params => {
			if (params['category']) {
				this.restaurantService
					.getRestaurantsByCategoryWithLogo(params['category'])
					.subscribe(restaurant => {
						this.restaurantsByCategory = restaurant;
					});
			}
		});
	}
}
