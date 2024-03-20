import { Component, Inject, OnInit } from '@angular/core';
import {
	MAT_DIALOG_DATA,
	MatDialogActions,
	MatDialogContent,
	MatDialogRef,
	MatDialogTitle,
} from '@angular/material/dialog';
import { FilterDialogData } from '../../model/filter-dialog-data.model';
import { MatActionList, MatList, MatListItem } from '@angular/material/list';
import { MatFormField, MatOption, MatSelect } from '@angular/material/select';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	ReactiveFormsModule,
} from '@angular/forms';
import { MatButton } from '@angular/material/button';

@Component({
	selector: 'app-filter-dialog',
	standalone: true,
	imports: [
		MatList,
		MatSelect,
		MatFormField,
		MatOption,
		ReactiveFormsModule,
		MatDialogContent,
		MatDialogTitle,
		MatActionList,
		MatListItem,
		MatDialogActions,
		MatButton,
	],
	templateUrl: './filter-dialog.component.html',
	styleUrl: './filter-dialog.component.scss',
})
export class FilterDialogComponent implements OnInit {
	protected formGroup: FormGroup;
	protected priceCategories: number[] = [];
	protected foodCategories: string[] = [];

	constructor(
		public fb: FormBuilder,
		public dialogRef: MatDialogRef<FilterDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: FilterDialogData
	) {
		this.formGroup = this.fb.group({
			priceCategoryControl: [''],
			foodCategoryControl: [''],
		});
	}

	public ngOnInit(): void {
		if (this.data) {
			this.foodCategories = this.data.foodCategories;
			this.priceCategories = this.data.priceCategories;

			this.formGroup.patchValue({
				foodCategoryControl: this.data.selectedFoodCategories,
				priceCategoryControl: this.data.selectedPriceCategories,
			});
		}

		this.dialogRef.backdropClick().subscribe(_ => this.dialogRef.close());
	}

	public onFilterClick(): void {
		this.data.selectedPriceCategories = this.formGroup.get(
			'priceCategoryControl'
		)?.value;
		this.data.selectedFoodCategories = this.formGroup.get(
			'foodCategoryControl'
		)?.value;

		this.dialogRef.close();
	}

	public onCancelClick(): void {
		this.dialogRef.close();
	}
}
