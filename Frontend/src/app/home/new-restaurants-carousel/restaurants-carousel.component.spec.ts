import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantsCarouselComponent } from './restaurants-carousel.component';

describe('NewRestaurantsCarouselComponent', () => {
	let component: RestaurantsCarouselComponent;
	let fixture: ComponentFixture<RestaurantsCarouselComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RestaurantsCarouselComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(RestaurantsCarouselComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
