import { Component, Input } from '@angular/core';
import { Restaurant } from '../model/restaurant.model';
import { NgOptimizedImage } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
	selector: 'app-restaurant-card',
	standalone: true,
	imports: [NgOptimizedImage, RouterLink],
	templateUrl: './restaurant-card.component.html',
	styleUrl: './restaurant-card.component.scss',
})
export class RestaurantCardComponent {
	@Input() restaurant: Restaurant | undefined;

	constructor() {}
}
