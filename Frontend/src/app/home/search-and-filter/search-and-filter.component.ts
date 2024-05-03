import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { CategoryService } from '../../services/category.service';
import { PriceCategoryService } from '../../services/price-category.service';
import { MatDialog } from '@angular/material/dialog';
import { FilterData } from '../../model/filter/filter-data.model';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-search-and-filter',
	standalone: true,
	imports: [MatIcon, MatButton],
	templateUrl: './search-and-filter.component.html',
	styleUrl: './search-and-filter.component.scss',
})
export class SearchAndFilterComponent implements OnInit {
	@Input({ required: true }) public filter?: FilterData;

	@Output() filterApplied = new EventEmitter<FilterData>();

	constructor(
		private categoryService: CategoryService,
		private priceCategoryService: PriceCategoryService,
		private dialog: MatDialog,
		private activatedRoute: ActivatedRoute
	) {}

	public ngOnInit() {
		this.categoryService.getCategoriesWithLogo().subscribe(categories => {
			if (this.filter) {
				this.filter.filterDialogData.foodCategories = categories.map(
					c => c.name
				);
				this.activatedRoute.params.subscribe(params => {
					if (params['category'] && this.filter) {
						this.filter.filterDialogData.foodCategories =
							this.filter.filterDialogData.foodCategories.filter(
								c => c != params['category']
							);
					}
				});
			}
		});

		this.priceCategoryService
			.getPriceCategories()
			.subscribe(priceCategories => {
				if (this.filter) {
					this.filter.filterDialogData.priceCategories = priceCategories;
				}
			});
	}

	public openFilterDialog(): void {
		this.dialog
			.open(FilterDialogComponent, {
				maxWidth: '100vw',
				width: 'auto',
				maxHeight: '100vh',
				height: 'auto',
				data: this.filter?.filterDialogData,
			})
			.afterClosed()
			.subscribe(() => {
				this.filterApplied.emit(this.filter);
			});
	}

	public applyFilterText(event: Event): void {
		const inputElement = event.target as HTMLInputElement;
		if (this.filter) {
			this.filter.text = inputElement.value;
		}
		this.filterApplied.emit(this.filter);
	}
}
