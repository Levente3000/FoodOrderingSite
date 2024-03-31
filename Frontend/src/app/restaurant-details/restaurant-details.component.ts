import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { MatIcon } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RestaurantMoreInfo } from '../model/restaurant-more-info.model';
import { OpeningHours } from '../model/opening-hours.model';
import { RestaurantMoreInfoDialogComponent } from './restaurant-more-info-dialog/restaurant-more-info-dialog.component';
import { RestaurantDetail } from '../model/restaurant-detail.model';
import { MatFabButton } from '@angular/material/button';
import { ProductCardComponent } from './product-card/product-card.component';

@Component({
	selector: 'app-restaurant-details',
	standalone: true,
	imports: [MatIcon, MatFabButton, ProductCardComponent],
	templateUrl: './restaurant-details.component.html',
	styleUrl: './restaurant-details.component.scss',
})
export class RestaurantDetailsComponent implements OnInit {
	protected restaurant?: RestaurantDetail;
	protected dialogData: RestaurantMoreInfo;

	constructor(
		private restaurantService: RestaurantService,
		private _activatedRoute: ActivatedRoute,
		private dialog: MatDialog
	) {
		this.dialogData = {
			restaurantName: '',
			address: '',
			phoneNumber: '',
			openingHours: new OpeningHours(),
			closingHours: new OpeningHours(),
		};
	}

	public ngOnInit(): void {
		this._activatedRoute.params.subscribe(params => {
			this.restaurantService
				.getRestaurantByIdWithLogo(params['id'])
				.subscribe(restaurant => {
					this.restaurant = restaurant;
					this.dialogData.restaurantName = this.restaurant.name;
					this.dialogData.address = this.restaurant.address;
					this.dialogData.phoneNumber = this.restaurant.phoneNumber;
					this.dialogData.openingHours = this.restaurant.openingHours;
					this.dialogData.closingHours = this.restaurant.closingHours;
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
			data: this.dialogData,
		});
	}
}
