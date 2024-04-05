import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { MatIcon } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RestaurantMoreInfoDialogComponent } from './restaurant-more-info-dialog/restaurant-more-info-dialog.component';
import { RestaurantDetail } from '../model/restaurant-detail.model';
import { MatFabButton } from '@angular/material/button';
import { ProductCardComponent } from './product-card/product-card.component';
import { KeycloakService } from 'keycloak-angular';

@Component({
	selector: 'app-restaurant-details',
	standalone: true,
	imports: [MatIcon, MatFabButton, ProductCardComponent],
	templateUrl: './restaurant-details.component.html',
	styleUrl: './restaurant-details.component.scss',
})
export class RestaurantDetailsComponent implements OnInit {
	protected restaurant?: RestaurantDetail;

	constructor(
		private restaurantService: RestaurantService,
		private _activatedRoute: ActivatedRoute,
		private dialog: MatDialog
	) {}

	public ngOnInit(): void {
		this._activatedRoute.params.subscribe(params => {
			this.restaurantService
				.getRestaurantByIdWithLogo(params['id'])
				.subscribe(restaurant => {
					this.restaurant = restaurant;
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
}
