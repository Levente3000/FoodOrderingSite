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
								.getAssetForRestaurant(shoppingCartItem.product.pictureName)
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

	public addProduct(productId: number, quantity: number): void {
		const bodyParams = {
			productId: productId,
			quantity: quantity,
		};

		this.httpClient.post(
			`${baseUrl}/${this.controllerUrl}/add-product`,
			bodyParams
		);
	}
}
