import { Component, Input } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Restaurant } from '../../model/restaurant/restaurant.model';

@Component({
	selector: 'app-restaurant-card',
	standalone: true,
	imports: [NgOptimizedImage, RouterLink],
	templateUrl: './restaurant-card.component.html',
	styleUrl: './restaurant-card.component.scss',
})
export class RestaurantCardComponent {
	@Input({ required: true }) public restaurant?: Restaurant;

	constructor() {}
}
