import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from '../../global';
import { forkJoin, map, mergeMap, Observable } from 'rxjs';
import { ShoppingCartItem } from '../model/shopping-cart.model';
import { AssetsService } from './assets.service';

@Injectable({
	providedIn: 'root',
})
export class ShoppingCartService {
	private controllerUrl = 'shopping-cart';

	constructor(
		private readonly httpClient: HttpClient,
		private readonly assetsService: AssetsService
	) {}

	public getShoppingCartWithProductPicture(): Observable<ShoppingCartItem[]> {
		return this.httpClient
			.get<ShoppingCartItem[]>(`${baseUrl}/${this.controllerUrl}`)
			.pipe(
				mergeMap(shoppingCartItems =>
					forkJoin(
						shoppingCartItems.map(shoppingCartItem => {
							return this.assetsService
								.getAssetForProduct(shoppingCartItem.product.pictureName)
								.pipe(
									map(asset => {
										shoppingCartItem.product.picture = asset;
										return shoppingCartItem;
									})
								);
						})
					)
				)
			);
	}

	public addProduct(productId: number, quantity: number) {
		const bodyParams = {
			productId: productId,
			quantity: quantity,
		};

		console.log(bodyParams);

		return this.httpClient.post(
			`${baseUrl}/${this.controllerUrl}/add-product`,
			bodyParams
		);
	}

	public updateQuantity(shoppingCartItemId: number, quantity: number) {
		const bodyParams = {
			shoppingCartItemId: shoppingCartItemId,
			quantity: quantity,
		};

		return this.httpClient.patch(
			`${baseUrl}/${this.controllerUrl}/update-quantity`,
			bodyParams
		);
	}

	public removeItem(itemId: number) {
		return this.httpClient.delete(
			`${baseUrl}/${this.controllerUrl}/remove-item/${itemId}`
		);
	}

	public clearCart() {
		return this.httpClient.delete(
			`${baseUrl}/${this.controllerUrl}/clear-cart`
		);
	}
}
