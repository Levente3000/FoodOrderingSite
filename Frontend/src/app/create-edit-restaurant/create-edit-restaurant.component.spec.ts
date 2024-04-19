import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditRestaurantComponent } from './create-edit-restaurant.component';

describe('CreateRestaurantComponent', () => {
	let component: CreateEditRestaurantComponent;
	let fixture: ComponentFixture<CreateEditRestaurantComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [CreateEditRestaurantComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(CreateEditRestaurantComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
