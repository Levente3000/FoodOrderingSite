import { Component, OnInit } from '@angular/core';
import { ShoppingCartItem } from '../model/shopping-cart.model';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
	selector: 'app-shopping-cart',
	standalone: true,
	imports: [],
	templateUrl: './shopping-cart.component.html',
	styleUrl: './shopping-cart.component.scss',
})
export class ShoppingCartComponent implements OnInit {
	protected shoppingCart: ShoppingCartItem[] = [];

	public constructor(
		private dialog: MatDialog,
		private shoppingCartService: ShoppingCartService
	) {}

	public ngOnInit() {
		this.shoppingCartService
			.getShoppingCartWithProductPicture()
			.subscribe(shoppingCartItems => {
				this.shoppingCart = shoppingCartItems;
			});
	}
}
