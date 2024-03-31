import { Component, Input } from '@angular/core';
import { Product } from '../../model/product.model';
import { RestaurantMoreInfoDialogComponent } from '../restaurant-more-info-dialog/restaurant-more-info-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../product-detail-dialog/product-detail-dialog.component';

@Component({
	selector: 'app-product-card',
	standalone: true,
	imports: [],
	templateUrl: './product-card.component.html',
	styleUrl: './product-card.component.scss',
})
export class ProductCardComponent {
	@Input({ required: true }) public product?: Product;

	constructor(private dialog: MatDialog) {}

	public productDetailDialogOpen(): void {
		this.dialog.open(ProductDetailDialogComponent, {
			maxWidth: '80vw',
			width: 'auto',
			maxHeight: '80vh',
			height: 'auto',
			autoFocus: false,
			panelClass: 'custom-dialog',
			data: this.product,
		});
	}
}
