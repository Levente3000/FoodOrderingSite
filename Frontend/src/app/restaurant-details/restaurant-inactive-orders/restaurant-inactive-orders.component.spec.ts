import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantInactiveOrdersComponent } from './restaurant-inactive-orders.component';

describe('RestaurantInactiveOrdersComponent', () => {
	let component: RestaurantInactiveOrdersComponent;
	let fixture: ComponentFixture<RestaurantInactiveOrdersComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RestaurantInactiveOrdersComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RestaurantInactiveOrdersComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
