import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodCategoryChipAutocompleteComponent } from './food-category-chip-autocomplete.component';

describe('FoodCategoryChipAutocompleteComponent', () => {
	let component: FoodCategoryChipAutocompleteComponent;
	let fixture: ComponentFixture<FoodCategoryChipAutocompleteComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [FoodCategoryChipAutocompleteComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(FoodCategoryChipAutocompleteComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
