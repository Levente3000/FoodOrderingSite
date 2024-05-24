import { Component, OnInit } from '@angular/core';
import { ShoppingCartItem } from '../model/shopping-cart.model';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { QuantityComponent } from '../shared/quantity/quantity.component';
import { MatTooltip } from '@angular/material/tooltip';
import { MatButton } from '@angular/material/button';
import { MatYearView } from '@angular/material/datepicker';
import { FormsModule } from '@angular/forms';
import { PromoCode } from '../model/promo-code.model';
import { PromoCodeService } from '../services/promo-code.service';
import { NgIf } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductDetailDialogComponent } from '../shared/product-detail-dialog/product-detail-dialog.component';
import { Product } from '../model/product.model';
import { MatDialog } from '@angular/material/dialog';
import { ProfileService } from '../services/profile.service';
import { MatError } from '@angular/material/form-field';
import { Router } from '@angular/router';
import { OrderService } from '../services/order.service';
import { map } from 'rxjs';

@Component({
	selector: 'app-shopping-cart',
	standalone: true,
	imports: [
		QuantityComponent,
		MatTooltip,
		MatButton,
		MatYearView,
		FormsModule,
		NgIf,
		MatError,
	],
	templateUrl: './shopping-cart.component.html',
	styleUrl: './shopping-cart.component.scss',
})
export class ShoppingCartComponent implements OnInit {
	protected shoppingCart: ShoppingCartItem[] = [];
	protected promoCodes: PromoCode[] = [];
	protected promoCode = '';
	protected appliedPromoCode: PromoCode | null = null;
	protected canPlaceOrder: boolean = false;

	protected get totalCost(): number {
		return this.shoppingCart.reduce((pastValue, item) => {
			return pastValue + item.quantity * item.product.price;
		}, 0);
	}

	protected get totalCostWithPromo(): number {
		const baseTotal = this.totalCost;

		if (this.appliedPromoCode) {
			return baseTotal - baseTotal * this.appliedPromoCode.percentage;
		}

		return baseTotal;
	}

	public constructor(
		private shoppingCartService: ShoppingCartService,
		private promoCodeService: PromoCodeService,
		private profileService: ProfileService,
		private orderService: OrderService,
		private snackBar: MatSnackBar,
		private dialog: MatDialog,
		private router: Router
	) {}

	public ngOnInit() {
		this.shoppingCartService
			.getShoppingCartWithProductPicture()
			.pipe(
				map(shoppingCartItems =>
					shoppingCartItems.map(shoppingCartItem => {
						shoppingCartItem.product.name = `(${shoppingCartItem.restaurantName}) ${shoppingCartItem.product.name}`;
						return shoppingCartItem;
					})
				)
			)
			.subscribe(shoppingCartItems => {
				this.shoppingCart = shoppingCartItems;
			});

		this.promoCodeService.getPromoCodes().subscribe(codes => {
			this.promoCodes = codes;
		});

		this.profileService
			.getCanPlaceOrder()
			.subscribe(canPlaceOrder => (this.canPlaceOrder = canPlaceOrder));
	}

	public onQuantityChange(item: ShoppingCartItem): void {
		this.shoppingCartService
			.updateQuantity(item.shoppingCartItemId, item.quantity)
			.subscribe();
	}

	public removeItem(shoppingCartItemId: number): void {
		this.shoppingCartService.removeItem(shoppingCartItemId).subscribe(() => {
			this.shoppingCart = this.shoppingCart.filter(
				item => item.shoppingCartItemId !== shoppingCartItemId
			);
		});
	}

	public clearCart(): void {
		this.shoppingCartService.clearCart().subscribe(() => {
			this.snackBar.open('Cart successfully cleared!', 'Ok', {
				duration: 5000,
			});

			this.shoppingCart = [];
			this.removePromoCode();
		});
	}

	protected applyPromoCode(): void {
		const code = this.promoCodes.find(
			code =>
				code.code.trim().toLowerCase() == this.promoCode.trim().toLowerCase()
		);

		if (code) {
			this.appliedPromoCode = code;
		}
	}

	protected removePromoCode(): void {
		this.appliedPromoCode = null;
		this.promoCode = '';
	}

	protected placeOrder(): void {
		this.orderService.placeOrder(this.appliedPromoCode).subscribe(() => {
			this.snackBar.open('Order successfully placed', 'Ok', {
				duration: 10000,
			});

			this.router.navigate(['/home']);
		});
	}

	protected productDetailDialogOpen(product: Product): void {
		this.dialog.open(ProductDetailDialogComponent, {
			maxWidth: '80vw',
			width: 'auto',
			maxHeight: '80vh',
			height: 'auto',
			autoFocus: false,
			panelClass: 'custom-dialog',
			data: {
				product: product,
				showActions: false,
			},
		});
	}
}
