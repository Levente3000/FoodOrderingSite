import { Component, Input } from '@angular/core';
import { SearchAndFilterComponent } from '../search-and-filter/search-and-filter.component';
import { NewRestaurantsCarouselComponent } from '../new-restaurants-carousel/new-restaurants-carousel.component';
import { Restaurant } from '../../model/restaurant.model';
import { RestaurantCardComponent } from '../../restaurant-card/restaurant-card.component';
import { FilterData } from '../../model/filter-data.model';

@Component({
	selector: 'app-filter-and-restaurants',
	standalone: true,
	imports: [
		SearchAndFilterComponent,
		NewRestaurantsCarouselComponent,
		RestaurantCardComponent,
	],
	templateUrl: './filter-and-restaurants.component.html',
	styleUrl: './filter-and-restaurants.component.scss',
})
export class FilterAndRestaurantsComponent {
	protected _allRestaurants: Restaurant[] = [];
	protected filteredRestaurants: Restaurant[] = [];
	protected filterData: FilterData;

	@Input({ required: true })
	public set allRestaurants(allRestaurants: Restaurant[]) {
		this._allRestaurants = allRestaurants;
		this.filteredRestaurants = allRestaurants;
	}

	constructor() {
		this.filterData = {
			text: '',
			filterDialogData: {
				selectedFoodCategories: [],
				priceCategories: [],
				selectedPriceCategories: [],
				foodCategories: [],
			},
		};
	}

	public onFilter($event: FilterData): void {
		this.filterData = $event;
		this.filteredRestaurants = this._allRestaurants.filter(
			restaurant =>
				this.filterName(restaurant) &&
				this.filterPriceCategories(restaurant) &&
				this.filterFoodCategories(restaurant)
		);
	}

	private filterName(restaurant: Restaurant): boolean {
		if (this.filterData.text === '') {
			return true;
		}
		return restaurant.name.includes(this.filterData.text);
	}

	private filterPriceCategories(restaurant: Restaurant): boolean {
		if (this.filterData.filterDialogData.selectedPriceCategories.length === 0) {
			return true;
		}

		return this.filterData.filterDialogData.selectedPriceCategories.includes(
			restaurant.priceCategory
		);
	}

	private filterFoodCategories(restaurant: Restaurant): boolean {
		if (this.filterData.filterDialogData.selectedFoodCategories.length === 0) {
			return true;
		}

		return restaurant.products.some(product =>
			product.categoryNames.some(category =>
				this.filterData.filterDialogData.selectedFoodCategories.includes(
					category
				)
			)
		);
	}
}
