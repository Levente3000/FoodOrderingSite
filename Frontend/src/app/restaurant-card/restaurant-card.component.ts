import { Component, Input, OnInit } from '@angular/core';
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
export class RestaurantCardComponent implements OnInit {
	@Input() restaurant?: Restaurant;

	constructor() {}

	public ngOnInit() {
		console.log(this.restaurant);
	}
}
