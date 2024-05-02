import { Component, Input } from '@angular/core';
import { Product } from '../../model/product.model';
import { MatDialog } from '@angular/material/dialog';
import { ProductDetailDialogComponent } from '../../shared/product-detail-dialog/product-detail-dialog.component';
import { NgIf } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
	selector: 'app-product-card',
	standalone: true,
	imports: [NgIf, MatIcon],
	templateUrl: './product-card.component.html',
	styleUrl: './product-card.component.scss',
})
export class ProductCardComponent {
	@Input({ required: true }) public product?: Product;
	@Input() public isAuthorized: boolean = false;

	constructor(
		private dialog: MatDialog,
		private router: Router
	) {}

	public productDetailDialogOpen(): void {
		this.dialog.open(ProductDetailDialogComponent, {
			maxWidth: '80vw',
			width: 'auto',
			maxHeight: '80vh',
			height: 'auto',
			autoFocus: false,
			panelClass: 'custom-dialog',
			data: {
				product: this.product,
				showActions: true,
			},
		});
	}

	public editProduct(event: Event): void {
		event.stopPropagation();
		if (this.product) {
			this.router.navigate([
				'/edit-product',
				this.product.restaurantId,
				this.product.id,
			]);
		}
	}
}
