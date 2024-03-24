import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, mergeMap, Observable } from 'rxjs';
import { Restaurant } from '../model/restaurant.model';
import { baseUrl } from '../../global';
import { AssetsService } from './assets.service';

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

	public getRestaurantByIdWithLogo(id: number): Observable<Restaurant> {
		return this.httpClient
			.get<Restaurant>(`${baseUrl}/${this.controllerUrl}/details/${id}`)
			.pipe(
				mergeMap(restaurant =>
					this.assetsService.getAssetForRestaurant(restaurant.logoName).pipe(
						map(asset => {
							restaurant.logo = asset;
							return restaurant;
						})
					)
				)
			);
	}
}
