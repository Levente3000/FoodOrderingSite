import {
	AfterViewInit,
	ChangeDetectorRef,
	Component,
	ElementRef,
	Input,
	OnChanges,
	ViewChild,
} from '@angular/core';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { Restaurant } from '../../model/restaurant/restaurant.model';
import { RestaurantCardComponent } from '../../shared/restaurant-card/restaurant-card.component';

@Component({
	selector: 'app-restaurants-carousel',
	standalone: true,
	imports: [RestaurantCardComponent, NgClass, NgOptimizedImage],
	templateUrl: './restaurants-carousel.component.html',
	styleUrl: './restaurants-carousel.component.scss',
})
export class RestaurantsCarouselComponent implements AfterViewInit, OnChanges {
	@ViewChild('sliderWrapper') sliderWrapper!: ElementRef<HTMLDivElement>;
	scrollPosition = 0;
	maxScroll = 0;

	protected _allRestaurants: Restaurant[] = [];
	@Input() title: string = '';

	@Input({ required: true })
	public set allRestaurants(allRestaurants: Restaurant[]) {
		this._allRestaurants = allRestaurants;

		this.cd.detectChanges();
	}

	constructor(private readonly cd: ChangeDetectorRef) {}

	ngAfterViewInit() {
		window.addEventListener('resize', this.calculateMaxScroll.bind(this));
	}

	ngOnChanges() {
		this.calculateMaxScroll();
	}

	calculateMaxScroll() {
		const sliderElement = this.sliderWrapper.nativeElement;
		const visibleWidth = sliderElement.clientWidth;
		const contentWidth = sliderElement.scrollWidth;
		this.maxScroll = contentWidth - visibleWidth;
		if (this.maxScroll === 0) {
			this.scrollPosition = 0;
		}
	}

	scrollRight(): void {
		const newPosition = this.scrollPosition + 200;
		this.scrollPosition = Math.min(newPosition, this.maxScroll);
	}

	scrollLeft(): void {
		const newPosition = this.scrollPosition - 200;
		this.scrollPosition = Math.max(newPosition, 0);
	}
}
