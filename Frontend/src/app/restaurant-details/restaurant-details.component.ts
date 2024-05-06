import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { MatIcon } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RestaurantMoreInfoDialogComponent } from './restaurant-more-info-dialog/restaurant-more-info-dialog.component';
import { MatFabButton } from '@angular/material/button';
import { ProductCardComponent } from './product-card/product-card.component';
import { RestaurantDetail } from '../model/restaurant/restaurant-detail.model';
import { NgIf } from '@angular/common';
import { OpeningHours } from '../model/opening-hours.model';

@Component({
	selector: 'app-restaurant-details',
	standalone: true,
	imports: [MatIcon, MatFabButton, ProductCardComponent, NgIf],
	templateUrl: './restaurant-details.component.html',
	styleUrl: './restaurant-details.component.scss',
})
export class RestaurantDetailsComponent implements OnInit {
	protected restaurant?: RestaurantDetail;
	protected isAuthorized: boolean = false;
	protected isClosedToday: boolean = false;
	protected alreadyInFavourites: boolean = false;
	protected favouriteTitle: string = 'Add to favourites';

	constructor(
		private restaurantService: RestaurantService,
		private _activatedRoute: ActivatedRoute,
		private dialog: MatDialog,
		private router: Router
	) {}

	public ngOnInit() {
		this._activatedRoute.params.subscribe(params => {
			this.restaurantService
				.getRestaurantByIdWithLogo(params['id'])
				.subscribe(restaurant => {
					this.restaurant = restaurant;
					this.restaurant.id = params['id'];
					this.isClosedToday =
						this.areTodayHoursSet(restaurant.openingHours) &&
						this.areTodayHoursSet(restaurant.closingHours);
				});
			this.restaurantService.isAuthorized(params['id']).subscribe(result => {
				this.isAuthorized = result;
			});
			this.restaurantService.isInFavourites(params['id']).subscribe(result => {
				this.alreadyInFavourites = result;
			});
		});
	}

	public moreInfoDialogOpen(): void {
		this.dialog.open(RestaurantMoreInfoDialogComponent, {
			maxWidth: '80vw',
			width: 'auto',
			maxHeight: '80vh',
			height: 'auto',
			autoFocus: false,
			data: {
				restaurantName: this.restaurant?.name,
				address: this.restaurant?.address,
				phoneNumber: this.restaurant?.phoneNumber,
				openingHours: this.restaurant?.openingHours,
				closingHours: this.restaurant?.closingHours,
			},
		});
	}

	public changeFavouriteState(): void {
		if (this.restaurant) {
			this.restaurantService
				.changeStateOfFavouriteRestaurant(this.restaurant.id)
				.subscribe(() => {
					this.alreadyInFavourites = !this.alreadyInFavourites;
				});
		}
	}

	public routeToEditRestaurant(): void {
		this.router.navigate(['/edit-restaurant', this.restaurant?.id]);
	}

	public routeToCreateProduct(): void {
		this.router.navigate(['/create-product', this.restaurant?.id]);
	}

	public routeToRestaurantOrders(): void {
		this.router.navigate(['/restaurant-orders/active', this.restaurant?.id]);
	}

	private getTodayHours(hours: OpeningHours): string {
		const today = new Date();
		const dayOfWeek = today
			.toLocaleDateString('en-US', { weekday: 'long' })
			.toLowerCase();

		return hours[dayOfWeek];
	}

	private areTodayHoursSet(hours: OpeningHours): boolean {
		const todayHours = this.getTodayHours(hours);
		return todayHours === '' || todayHours === null;
	}
}
