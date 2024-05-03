import {
	AfterViewInit,
	Component,
	Input,
	OnInit,
	ViewChild,
} from '@angular/core';
import { SearchAndFilterComponent } from '../search-and-filter/search-and-filter.component';
import { NewRestaurantsCarouselComponent } from '../new-restaurants-carousel/new-restaurants-carousel.component';
import { RestaurantCardComponent } from '../../restaurant-card/restaurant-card.component';
import { Restaurant } from '../../model/restaurant/restaurant.model';
import { FilterData } from '../../model/filter/filter-data.model';
import { MatPaginator } from '@angular/material/paginator';

@Component({
	selector: 'app-filter-and-restaurants',
	standalone: true,
	imports: [
		SearchAndFilterComponent,
		NewRestaurantsCarouselComponent,
		RestaurantCardComponent,
		MatPaginator,
	],
	templateUrl: './filter-and-restaurants.component.html',
	styleUrl: './filter-and-restaurants.component.scss',
})
export class FilterAndRestaurantsComponent implements AfterViewInit {
	protected _allRestaurants: Restaurant[] = [];
	protected filteredRestaurants: Restaurant[] = [];
	protected filteredRestaurantsToPaginate: Restaurant[] = [];
	protected filterData: FilterData;

	@ViewChild(MatPaginator) paginator!: MatPaginator;

	@Input({ required: true })
	public set allRestaurants(allRestaurants: Restaurant[]) {
		this._allRestaurants = allRestaurants;
		this.filteredRestaurants = allRestaurants;
		this.filteredRestaurantsToPaginate = allRestaurants;
		this.updateCurrentRestaurants(this.filteredRestaurantsToPaginate);
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

	public ngAfterViewInit() {
		this.updateCurrentRestaurants(this.filteredRestaurantsToPaginate);
	}

	public onFilter($event: FilterData): void {
		this.filterData = $event;
		this.filteredRestaurantsToPaginate = this._allRestaurants.filter(
			restaurant =>
				this.filterName(restaurant) &&
				this.filterPriceCategories(restaurant) &&
				this.filterFoodCategories(restaurant)
		);

		this.updateCurrentRestaurants(this.filteredRestaurantsToPaginate);
	}

	private filterName(restaurant: Restaurant): boolean {
		if (this.filterData.text === '') {
			return true;
		}
		return restaurant.name
			.toLowerCase()
			.includes(this.filterData.text.toLowerCase());
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

	updateCurrentRestaurants(filteredRestaurantsToPaginate: Restaurant[]): void {
		if (!this.paginator) {
			return;
		}
		const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
		const endIndex = startIndex + this.paginator.pageSize;
		this.filteredRestaurants = filteredRestaurantsToPaginate.slice(
			startIndex,
			endIndex
		);
	}

	onPageChange() {
		this.updateCurrentRestaurants(this.filteredRestaurantsToPaginate);
	}
}
