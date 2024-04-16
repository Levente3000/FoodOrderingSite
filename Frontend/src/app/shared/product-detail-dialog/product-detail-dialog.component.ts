import { Component, Inject } from '@angular/core';
import {
	MAT_DIALOG_DATA,
	MatDialogActions,
	MatDialogContent,
	MatDialogRef,
} from '@angular/material/dialog';
import { Product } from '../../model/product.model';
import { MatFabButton } from '@angular/material/button';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { QuantityComponent } from '../quantity/quantity.component';
import { NgIf } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-product-detail-dialog',
	standalone: true,
	imports: [
		MatDialogContent,
		MatFabButton,
		MatDialogActions,
		QuantityComponent,
		NgIf,
	],
	templateUrl: './product-detail-dialog.component.html',
	styleUrl: './product-detail-dialog.component.scss',
})
export class ProductDetailDialogComponent {
	protected quantity = 1;

	constructor(
		public dialogRef: MatDialogRef<ProductDetailDialogComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: { product: Product; showActions: boolean },
		private shoppingCartService: ShoppingCartService,
		private snackBar: MatSnackBar
	) {
		this.dialogRef
			.backdropClick()
			.subscribe(() => this.dialogRef.close(this.quantity));
	}

	public onCancelClick(): void {
		this.dialogRef.close();
	}

	public addProductToShoppingCart(): void {
		this.shoppingCartService
			.addProduct(this.data.product.id, this.quantity)
			.subscribe(() => {
				this.snackBar.open('Product successfully added!', 'Ok', {
					duration: 5000,
				});
				this.dialogRef.close();
			});
	}
}
