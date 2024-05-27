import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavouriteRestaurantsComponent } from './favourite-restaurants.component';

describe('FavouriteRestaurantsComponent', () => {
	let component: FavouriteRestaurantsComponent;
	let fixture: ComponentFixture<FavouriteRestaurantsComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [FavouriteRestaurantsComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(FavouriteRestaurantsComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
