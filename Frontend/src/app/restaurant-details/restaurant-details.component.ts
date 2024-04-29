import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { MatIcon } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RestaurantMoreInfoDialogComponent } from './restaurant-more-info-dialog/restaurant-more-info-dialog.component';
import { MatFabButton } from '@angular/material/button';
import { ProductCardComponent } from './product-card/product-card.component';
import { RestaurantDetail } from '../model/restaurant/restaurant-detail.model';
import { CreateEditProductComponent } from './create-edit-product/create-edit-product.component';

@Component({
	selector: 'app-restaurant-details',
	standalone: true,
	imports: [MatIcon, MatFabButton, ProductCardComponent],
	templateUrl: './restaurant-details.component.html',
	styleUrl: './restaurant-details.component.scss',
})
export class RestaurantDetailsComponent implements OnInit {
	protected restaurant?: RestaurantDetail;
	protected isAuthorized: boolean = false;

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
				});
			this.restaurantService
				.isAuthorized(params['id'])
				.subscribe(result => (this.isAuthorized = result));
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

	public routeToEditRestaurant(): void {
		this.router.navigate(['/edit-restaurant', this.restaurant?.id]);
	}

	public openCreateProductDialog(): void {
		this.router.navigate(['/create-product', this.restaurant?.id]);
	}
}
