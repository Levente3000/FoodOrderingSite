import { Component, Input } from '@angular/core';
import { Product } from '../../model/product.model';
import { MatDialog } from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../product-detail-dialog/product-detail-dialog.component';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { MatSnackBar } from '@angular/material/snack-bar';

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
		private snackBar: MatSnackBar,
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

	public addProductToShoppingCart(event: Event): void {
		event.stopPropagation();
		if (this.product) {
			console.log(1);
			this.shoppingCartService.addProduct(this.product?.id, 1).subscribe(() => {
				this.snackBar.open('Product successfully added!', 'Ok', {
					duration: 5000,
				});
			});
		}
	}
}
