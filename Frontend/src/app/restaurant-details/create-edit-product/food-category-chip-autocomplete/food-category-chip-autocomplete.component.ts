import { Component, forwardRef, Input } from '@angular/core';
import {
	MatChipGrid,
	MatChipInput,
	MatChipRemove,
	MatChipRow,
} from '@angular/material/chips';
import { MatIcon } from '@angular/material/icon';
import {
	MatAutocomplete,
	MatAutocompleteSelectedEvent,
	MatAutocompleteTrigger,
	MatOption,
} from '@angular/material/autocomplete';
import { map, Observable, of, startWith } from 'rxjs';
import {
	ControlValueAccessor,
	FormControl,
	NG_VALUE_ACCESSOR,
	ReactiveFormsModule,
} from '@angular/forms';
import { AsyncPipe } from '@angular/common';
import { MatFormField, MatLabel } from '@angular/material/form-field';

@Component({
	selector: 'app-food-category-chip-autocomplete',
	standalone: true,
	imports: [
		MatIcon,
		MatChipRemove,
		MatChipRow,
		MatChipGrid,
		MatLabel,
		MatFormField,
		MatChipInput,
		MatAutocompleteTrigger,
		ReactiveFormsModule,
		MatAutocomplete,
		AsyncPipe,
		MatOption,
	],
	templateUrl: './food-category-chip-autocomplete.component.html',
	styleUrl: './food-category-chip-autocomplete.component.scss',
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => FoodCategoryChipAutocompleteComponent),
			multi: true,
		},
	],
})
export class FoodCategoryChipAutocompleteComponent
	implements ControlValueAccessor
{
	@Input() set allFoodCategories(categories: string[]) {
		if (categories) {
			this._allFoodCategories = categories;
			this._updateFilteredFoodCategories();
		}
	}

	public get allFoodCategories(): string[] {
		return this._allFoodCategories;
	}

	private _allFoodCategories: string[] = [];

	protected inputControl = new FormControl('');
	protected filteredFoodCategories: Observable<string[]> = of([]);
	protected foodCategories: string[] = [];

	private onChange: (value: string[]) => void = () => {};
	private onTouched: () => void = () => {};

	constructor() {}

	public remove(foodCategory: string): void {
		const index = this.foodCategories.indexOf(foodCategory);

		if (index >= 0) {
			this.foodCategories.splice(index, 1);
			this.onChange(this.foodCategories);
			this.inputControl.setValue(null);
		}
	}

	public selected(event: MatAutocompleteSelectedEvent): void {
		this.foodCategories.push(event.option.viewValue);
		this.onChange(this.foodCategories);
		this.inputControl.setValue(null);
	}

	private _filter(value: string): string[] {
		const filterValue = value.toLowerCase();
		return this.allFoodCategories.filter(fruit =>
			fruit.toLowerCase().includes(filterValue)
		);
	}

	public writeValue(foodCategories: string[]): void {
		this.foodCategories = foodCategories || [];
		this._updateFilteredFoodCategories();
	}

	public registerOnChange(fn: (value: string[]) => void): void {
		this.onChange = fn;
	}

	public registerOnTouched(fn: () => void): void {
		this.onTouched = fn;
	}

	public setDisabledState?(isDisabled: boolean): void {
		isDisabled ? this.inputControl.disable() : this.inputControl.enable();
	}

	public onBlur(): void {
		this.onTouched();
	}

	private _updateFilteredFoodCategories(): void {
		this.filteredFoodCategories = this.inputControl.valueChanges.pipe(
			startWith(null),
			map(foodCategory =>
				foodCategory
					? this._filter(foodCategory)
					: this._allFoodCategories
							.filter(foodCat => !this.foodCategories.includes(foodCat))
							.slice()
			)
		);
	}
}
