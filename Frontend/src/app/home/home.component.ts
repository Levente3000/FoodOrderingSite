import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HomeService } from '../services/home.service';
import { Restaurant } from '../model/restaurant.model';
import { Subject, takeUntil } from 'rxjs';
import { RestaurantCardComponent } from '../restaurant-card/restaurant-card.component';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [RouterLink, RestaurantCardComponent],
	templateUrl: './home.component.html',
	styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit, OnDestroy {
	protected allRestaurants: Restaurant[] = [];

	private onDestroy$ = new Subject<void>();

	constructor(private homeService: HomeService) {}

	ngOnInit() {
		this.homeService
			.getAllRestaurants()
			.pipe(takeUntil(this.onDestroy$))
			.subscribe(restaurant => (this.allRestaurants = restaurant));
	}

	ngOnDestroy() {
		this.onDestroy$.next();
		this.onDestroy$.complete();
	}
}
