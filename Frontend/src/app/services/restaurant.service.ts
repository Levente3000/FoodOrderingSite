import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, mergeMap, Observable } from 'rxjs';
import { Restaurant } from '../model/restaurant.model';
import { baseUrl } from '../../global';
import { AssetsService } from './assets.service';
import {
	CategoriesWithProducts,
	RestaurantDetail,
} from '../model/restaurant-detail.model';

@Injectable({
	providedIn: 'root',
})
export class RestaurantService {
	private controllerUrl = 'restaurant';

	constructor(
		private readonly httpClient: HttpClient,
		private readonly assetsService: AssetsService
	) {}

	public getRestaurantsWithLogo(): Observable<Restaurant[]> {
		return this.httpClient
			.get<Restaurant[]>(`${baseUrl}/${this.controllerUrl}`)
			.pipe(
				mergeMap(restaurants =>
					forkJoin(
						restaurants.map(restaurant => {
							return this.assetsService
								.getAssetForRestaurant(restaurant.logoName)
								.pipe(
									map(asset => {
										restaurant.logo = asset;
										return restaurant;
									})
								);
						})
					)
				)
			);
	}

	public getRestaurantByIdWithLogo(id: number): Observable<RestaurantDetail> {
		return this.httpClient
			.get<RestaurantDetail>(`${baseUrl}/${this.controllerUrl}/details/${id}`)
			.pipe(
				mergeMap(restaurant =>
					forkJoin({
						logo: this.assetsService.getAssetForRestaurant(restaurant.logoName),
						banner: this.assetsService.getAssetForRestaurant(
							restaurant.bannerName
						),
						categoriesWithProducts: this.fetchProductPicturesForCategories(
							restaurant.categoriesWithProducts
						),
					}).pipe(
						map(({ logo, banner, categoriesWithProducts }) => {
							restaurant.logo = logo;
							restaurant.banner = banner;
							restaurant.categoriesWithProducts = categoriesWithProducts;
							return restaurant;
						})
					)
				)
			);
	}

	private fetchProductPicturesForCategories(
		categories: CategoriesWithProducts[]
	): Observable<CategoriesWithProducts[]> {
		return forkJoin(
			categories.map(category =>
				forkJoin(
					category.products.map(product =>
						this.assetsService.getAssetForProduct(product.pictureName).pipe(
							map(pictureUrl => {
								return { ...product, picture: pictureUrl };
							})
						)
					)
				).pipe(
					map(productsWithPictures => {
						return { ...category, products: productsWithPictures };
					})
				)
			)
		);
	}
}
