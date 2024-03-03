import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Restaurant } from '../model/restaurant.model';
import { RestaurantCardComponent } from '../restaurant-card/restaurant-card.component';
import { RestaurantService } from '../services/restaurant.service';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [RouterLink, RestaurantCardComponent],
	templateUrl: './home.component.html',
	styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
	protected allRestaurants: Restaurant[] = [];

	constructor(private restaurantService: RestaurantService) {}

	ngOnInit() {
		this.restaurantService.getRestaurantsWithLogo().subscribe(restaurants => {
			this.allRestaurants = restaurants;
			console.log(restaurants);
		});
	}
}
