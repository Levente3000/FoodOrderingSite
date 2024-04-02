import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantMoreInfoDialogComponent } from './restaurant-more-info-dialog.component';

describe('RestaurantMoreInfoDialogComponent', () => {
	let component: RestaurantMoreInfoDialogComponent;
	let fixture: ComponentFixture<RestaurantMoreInfoDialogComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RestaurantMoreInfoDialogComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RestaurantMoreInfoDialogComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
