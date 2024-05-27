import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { NgClass, NgIf } from '@angular/common';
import { RestaurantsCarouselComponent } from './new-restaurants-carousel/restaurants-carousel.component';
import { CategoryCarouselComponent } from './category-carousel/category-carousel.component';
import { Category } from '../model/category.model';
import { CategoryService } from '../services/category.service';
import { Restaurant } from '../model/restaurant/restaurant.model';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { RestaurantCardComponent } from '../shared/restaurant-card/restaurant-card.component';
import { SearchAndFilterComponent } from '../shared/filter-and-restaurants/search-and-filter/search-and-filter.component';
import { FilterAndRestaurantsComponent } from '../shared/filter-and-restaurants/filter-and-restaurants.component';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [
		RouterLink,
		RestaurantCardComponent,
		NgIf,
		NgClass,
		RestaurantsCarouselComponent,
		CategoryCarouselComponent,
		SearchAndFilterComponent,
		FilterAndRestaurantsComponent,
		MatProgressSpinner,
	],
	templateUrl: './home.component.html',
	styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
	protected latestTenRestaurant: Restaurant[] = [];
	protected restaurantsWithMostOrders: Restaurant[] = [];
	protected allCategories: Category[] = [];
	protected allCategoriesDataCameIn: boolean = false;

	public get isLoading(): boolean {
		return !this.allCategoriesDataCameIn;
	}

	constructor(
		private restaurantService: RestaurantService,
		private categoryService: CategoryService
	) {}

	public ngOnInit() {
		this.restaurantService
			.getLatestRestaurantsWithLogo()
			.subscribe(restaurants => {
				this.latestTenRestaurant = restaurants;
			});

		this.restaurantService
			.getRestaurantsWithTheMostOrdersWithLogo()
			.subscribe(restaurants => {
				this.restaurantsWithMostOrders = restaurants;
			});

		this.categoryService.getCategoriesWithLogo().subscribe(categories => {
			this.allCategories = categories;
			this.allCategoriesDataCameIn = true;
		});
	}
}
