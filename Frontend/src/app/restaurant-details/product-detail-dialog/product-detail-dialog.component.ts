import { Component, Inject } from '@angular/core';
import {
	MAT_DIALOG_DATA,
	MatDialogActions,
	MatDialogContent,
	MatDialogRef,
} from '@angular/material/dialog';
import { Product } from '../../model/product.model';
import { MatFabButton } from '@angular/material/button';

@Component({
	selector: 'app-product-detail-dialog',
	standalone: true,
	imports: [MatDialogContent, MatFabButton, MatDialogActions],
	templateUrl: './product-detail-dialog.component.html',
	styleUrl: './product-detail-dialog.component.scss',
})
export class ProductDetailDialogComponent {
	protected quantity = 1;

	constructor(
		public dialogRef: MatDialogRef<ProductDetailDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: Product
	) {
		this.dialogRef
			.backdropClick()
			.subscribe(() => this.dialogRef.close(this.quantity));
	}

	public onCancelClick(): void {
		this.dialogRef.close();
	}

	public addProduct(): void {
		this.quantity++;
	}

	public subtractProduct(): void {
		if (this.quantity > 1) {
			this.quantity--;
		} else {
			this.quantity = 1;
		}
	}
}
