import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Restaurant } from '../model/restaurant.model';
import { RestaurantCardComponent } from '../restaurant-card/restaurant-card.component';
import { RestaurantService } from '../services/restaurant.service';
import { NgClass, NgIf } from '@angular/common';
import { NewRestaurantsCarouselComponent } from './new-restaurants-carousel/new-restaurants-carousel.component';
import { CategoryCarouselComponent } from './category-carousel/category-carousel.component';
import { Category } from '../model/category.model';
import { CategoryService } from '../services/category.service';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [
		RouterLink,
		RestaurantCardComponent,
		NgIf,
		NgClass,
		NewRestaurantsCarouselComponent,
		CategoryCarouselComponent,
	],
	templateUrl: './home.component.html',
	styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
	protected allRestaurants: Restaurant[] = [];
	protected allCategories: Category[] = [];

	constructor(
		private restaurantService: RestaurantService,
		private categoryService: CategoryService
	) {}

	ngOnInit() {
		this.restaurantService.getRestaurantsWithLogo().subscribe(restaurants => {
			this.allRestaurants = restaurants;
		});

		this.categoryService.getCategoriesWithLogo().subscribe(categories => {
			this.allCategories = categories;
		});
	}
}
