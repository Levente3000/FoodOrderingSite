import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewRestaurantsCarouselComponent } from './new-restaurants-carousel.component';

describe('NewRestaurantsCarouselComponent', () => {
	let component: NewRestaurantsCarouselComponent;
	let fixture: ComponentFixture<NewRestaurantsCarouselComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [NewRestaurantsCarouselComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(NewRestaurantsCarouselComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
