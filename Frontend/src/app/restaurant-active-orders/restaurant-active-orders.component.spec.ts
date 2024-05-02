import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantActiveOrdersComponent } from './restaurant-active-orders.component';

describe('RestaurantOrdersComponent', () => {
	let component: RestaurantActiveOrdersComponent;
	let fixture: ComponentFixture<RestaurantActiveOrdersComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RestaurantActiveOrdersComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RestaurantActiveOrdersComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
