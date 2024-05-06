import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RestaurantCardComponent } from '../restaurant-card/restaurant-card.component';
import { RestaurantService } from '../services/restaurant.service';
import { NgClass, NgIf } from '@angular/common';
import { NewRestaurantsCarouselComponent } from './new-restaurants-carousel/new-restaurants-carousel.component';
import { CategoryCarouselComponent } from './category-carousel/category-carousel.component';
import { Category } from '../model/category.model';
import { CategoryService } from '../services/category.service';
import { SearchAndFilterComponent } from './search-and-filter/search-and-filter.component';
import { FilterAndRestaurantsComponent } from './filter-and-restaurants/filter-and-restaurants.component';
import { Restaurant } from '../model/restaurant/restaurant.model';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { LogicalFileSystem } from '@angular/compiler-cli';

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
