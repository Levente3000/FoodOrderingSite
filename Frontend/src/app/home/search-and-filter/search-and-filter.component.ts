import { Component, Input, OnInit } from '@angular/core';
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { RestaurantService } from '../../services/restaurant.service';
import { CategoryService } from '../../services/category.service';
import { PriceCategoryService } from '../../services/price-category.service';
import { MatDialog } from '@angular/material/dialog';
import { FilterDialogData } from '../../model/filter-dialog-data.model';

@Component({
	selector: 'app-search-and-filter',
	standalone: true,
	imports: [MatIcon, MatButton],
	templateUrl: './search-and-filter.component.html',
	styleUrl: './search-and-filter.component.scss',
})
export class SearchAndFilterComponent implements OnInit {
	private readonly filter: FilterDialogData;

	constructor(
		private categoryService: CategoryService,
		private priceCategoryService: PriceCategoryService,
		private dialog: MatDialog
	) {
		this.filter = {
			selectedFoodCategories: [],
			priceCategories: [],
			selectedPriceCategories: [],
			foodCategories: [],
		};
	}

	public ngOnInit() {
		this.categoryService.getCategoriesWithLogo().subscribe(categories => {
			this.filter.foodCategories = categories.map(c => c.name);
		});

		this.priceCategoryService
			.getPriceCategories()
			.subscribe(priceCategories => {
				this.filter.priceCategories = priceCategories;
			});
	}

	public openFilterDialog(): void {
		const dialogRef = this.dialog.open(FilterDialogComponent, {
			maxWidth: '100vw',
			width: 'auto',
			maxHeight: '100vh',
			height: 'auto',
			data: this.filter,
		});
	}

	// public applyFilter() {
	// 	TODO: need to filter the products of the restaurants
	// 	this.filteredRestaurants = this.allRestaurants.filter(restaurant =>
	// 		restaurant.name.toLowerCase().includes(this.filterText.toLowerCase())
	// 	);
	// }
}
