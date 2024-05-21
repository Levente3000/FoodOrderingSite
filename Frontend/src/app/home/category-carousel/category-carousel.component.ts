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
import { Category } from '../../model/category.model';
import { CategoryCardComponent } from './category-card/category-card.component';
import { RestaurantCardComponent } from '../../shared/restaurant-card/restaurant-card.component';

@Component({
	selector: 'app-category-carousel',
	standalone: true,
	imports: [
		NgOptimizedImage,
		RestaurantCardComponent,
		NgClass,
		CategoryCardComponent,
	],
	templateUrl: './category-carousel.component.html',
	styleUrl: './category-carousel.component.scss',
})
export class CategoryCarouselComponent implements AfterViewInit, OnChanges {
	@ViewChild('sliderWrapper') sliderWrapper!: ElementRef<HTMLDivElement>;
	scrollPosition = 0;
	maxScroll = 0;

	protected _allCategories: Category[] = [];

	@Input({ required: true })
	public set allCategories(allCategories: Category[]) {
		this._allCategories = allCategories;

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
