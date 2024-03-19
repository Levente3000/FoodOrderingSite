import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { CategoryService } from '../../services/category.service';
import { PriceCategoryService } from '../../services/price-category.service';
import { MatDialog } from '@angular/material/dialog';
import { FilterDialogData } from '../../model/filter-dialog-data.model';
import { FilterData } from '../../model/filter-data.model';
import { filter } from 'rxjs';

@Component({
	selector: 'app-search-and-filter',
	standalone: true,
	imports: [MatIcon, MatButton],
	templateUrl: './search-and-filter.component.html',
	styleUrl: './search-and-filter.component.scss',
})
export class SearchAndFilterComponent implements OnInit {
	private readonly filterDialogData: FilterDialogData;
	private readonly filter: FilterData;

	@Output() filterApplied = new EventEmitter<FilterData>();

	constructor(
		private categoryService: CategoryService,
		private priceCategoryService: PriceCategoryService,
		private dialog: MatDialog
	) {
		this.filterDialogData = {
			selectedFoodCategories: [],
			priceCategories: [],
			selectedPriceCategories: [],
			foodCategories: [],
		};

		this.filter = {
			text: '',
			filterDialogData: this.filterDialogData,
		};
	}

	public ngOnInit() {
		this.categoryService.getCategoriesWithLogo().subscribe(categories => {
			this.filterDialogData.foodCategories = categories.map(c => c.name);
		});

		this.priceCategoryService
			.getPriceCategories()
			.subscribe(priceCategories => {
				this.filterDialogData.priceCategories = priceCategories;
			});
	}

	public openFilterDialog(): void {
		this.dialog
			.open(FilterDialogComponent, {
				maxWidth: '100vw',
				width: 'auto',
				maxHeight: '100vh',
				height: 'auto',
				data: this.filterDialogData,
			})
			.afterClosed()
			.subscribe(_ => {
				this.filter.filterDialogData = this.filterDialogData;
				this.filterApplied.emit(this.filter);
			});
	}

	public applyFilterText(event: Event): void {
		const inputElement = event.target as HTMLInputElement;
		this.filter.text = inputElement.value;
		this.filterApplied.emit(this.filter);
	}
}
