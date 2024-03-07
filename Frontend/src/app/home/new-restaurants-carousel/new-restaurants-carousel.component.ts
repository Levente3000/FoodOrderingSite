import {
	AfterViewInit,
	ChangeDetectorRef,
	Component,
	ElementRef,
	Input,
	OnChanges,
	ViewChild,
} from '@angular/core';
import { RestaurantCardComponent } from '../../restaurant-card/restaurant-card.component';
import { Restaurant } from '../../model/restaurant.model';
import { NgClass, NgOptimizedImage } from '@angular/common';

@Component({
	selector: 'app-new-restaurants-carousel',
	standalone: true,
	imports: [RestaurantCardComponent, NgClass, NgOptimizedImage],
	templateUrl: './new-restaurants-carousel.component.html',
	styleUrl: './new-restaurants-carousel.component.scss',
})
export class NewRestaurantsCarouselComponent
	implements AfterViewInit, OnChanges
{
	@ViewChild('sliderWrapper') sliderWrapper!: ElementRef<HTMLDivElement>;
	scrollPosition = 0;
	maxScroll = 0;

	protected _allRestaurants: Restaurant[] = [];

	@Input()
	public set allRestaurants(allRestaurants: Restaurant[]) {
		this._allRestaurants = allRestaurants;

		this.cd.detach();
		this.cd.detectChanges();
		this.cd.reattach();
	}

	constructor(private readonly cd: ChangeDetectorRef) {}

	ngAfterViewInit() {
		window.addEventListener('resize', this.calculateMaxScroll.bind(this));
		this.calculateMaxScroll();
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
