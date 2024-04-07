import { Component, Input } from '@angular/core';
import { Product } from '../../model/product.model';
import { MatDialog } from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../product-detail-dialog/product-detail-dialog.component';
import { ShoppingCartService } from '../../services/shopping-cart.service';

@Component({
	selector: 'app-product-card',
	standalone: true,
	imports: [],
	templateUrl: './product-card.component.html',
	styleUrl: './product-card.component.scss',
})
export class ProductCardComponent {
	@Input({ required: true }) public product?: Product;

	constructor(
		private dialog: MatDialog,
		private shoppingCartService: ShoppingCartService
	) {}

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

	public addProductToShoppingCart(): void {
		if (this.product) {
			this.shoppingCartService.addProduct(this.product?.id, 1);
		}
	}
}
