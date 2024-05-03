import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, mergeMap, Observable, of } from 'rxjs';
import { baseUrl } from '../../global';
import { AssetsService } from './assets.service';
import { Restaurant } from '../model/restaurant/restaurant.model';
import {
	CategoriesWithProducts,
	RestaurantDetail,
} from '../model/restaurant/restaurant-detail.model';
import { EditRestaurant } from '../model/restaurant/edit-restaurant.model';

@Injectable({
	providedIn: 'root',
})
export class RestaurantService {
	private restaurantControllerUrl = 'restaurant';
	private restaurantPermissionControllerUrl = 'restaurant-permission';

	constructor(
		private readonly httpClient: HttpClient,
		private readonly assetsService: AssetsService
	) {}

	public getRestaurantsWithLogo(): Observable<Restaurant[]> {
		return this.httpClient
			.get<Restaurant[]>(`${baseUrl}/${this.restaurantControllerUrl}`)
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

	public getLatestRestaurantsWithLogo(): Observable<Restaurant[]> {
		return this.httpClient
			.get<Restaurant[]>(`${baseUrl}/${this.restaurantControllerUrl}/latest`)
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

	public getRestaurantsWithTheMostOrdersWithLogo(): Observable<Restaurant[]> {
		return this.httpClient
			.get<
				Restaurant[]
			>(`${baseUrl}/${this.restaurantControllerUrl}/most-orders`)
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

	public getRestaurantsByCategoryWithLogo(
		category: string
	): Observable<Restaurant[]> {
		return this.httpClient
			.get<
				Restaurant[]
			>(`${baseUrl}/${this.restaurantControllerUrl}/by-category/${category}`)
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
			.get<RestaurantDetail>(
				`${baseUrl}/${this.restaurantControllerUrl}/details/${id}`
			)
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

	public getRestaurantById(id: number): Observable<EditRestaurant> {
		return this.httpClient.get<EditRestaurant>(
			`${baseUrl}/${this.restaurantControllerUrl}/edit-details/${id}`
		);
	}

	public createRestaurant(formData: FormData): Observable<number | null> {
		return this.httpClient.post<number | null>(
			`${baseUrl}/${this.restaurantControllerUrl}/create-restaurant`,
			formData
		);
	}

	public editRestaurant(formData: FormData): Observable<number | null> {
		return this.httpClient.post<number | null>(
			`${baseUrl}/${this.restaurantControllerUrl}/edit-restaurant`,
			formData
		);
	}

	public isAuthorized(restaurantId: number): Observable<boolean> {
		return this.httpClient.get<boolean>(
			`${baseUrl}/${this.restaurantPermissionControllerUrl}/${restaurantId}`
		);
	}

	private fetchProductPicturesForCategories(
		categories: CategoriesWithProducts[]
	): Observable<CategoriesWithProducts[]> {
		if (categories.length === 0) {
			return of([]);
		}

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
