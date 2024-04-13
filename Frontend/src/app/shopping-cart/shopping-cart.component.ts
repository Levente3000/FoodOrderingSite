import { Component, OnInit } from '@angular/core';
import { ShoppingCartItem } from '../model/shopping-cart.model';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { QuantityComponent } from '../shared/quantity/quantity.component';
import { MatTooltip } from '@angular/material/tooltip';
import { MatButton } from '@angular/material/button';
import { MatYearView } from '@angular/material/datepicker';
import { FormsModule } from '@angular/forms';

@Component({
	selector: 'app-shopping-cart',
	standalone: true,
	imports: [QuantityComponent, MatTooltip, MatButton, MatYearView, FormsModule],
	templateUrl: './shopping-cart.component.html',
	styleUrl: './shopping-cart.component.scss',
})
export class ShoppingCartComponent implements OnInit {
	protected shoppingCart: ShoppingCartItem[] = [];
	protected promoCode = '';

	protected get totalCost(): number {
		return this.shoppingCart.reduce((pastValue, item) => {
			return pastValue + item.quantity * item.product.price;
		}, 0);
	}

	public constructor(private shoppingCartService: ShoppingCartService) {}

	public ngOnInit() {
		this.shoppingCartService
			.getShoppingCartWithProductPicture()
			.subscribe(shoppingCartItems => {
				this.shoppingCart = shoppingCartItems;
			});
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

	protected applyPromoCode(): void {}
}
