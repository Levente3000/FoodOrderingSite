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
	constructor(
		private readonly httpClient: HttpClient,
		private readonly assetsService: AssetsService
	) {}

	public getAllRestaurants(): Observable<Restaurant[]> {
		return this.httpClient.get<Restaurant[]>(`${baseUrl}/restaurant`);
	}

	public getRestaurantsWithLogo(): Observable<Restaurant[]> {
		return this.httpClient.get<Restaurant[]>(`${baseUrl}/restaurant`).pipe(
			mergeMap(restaurants =>
				forkJoin(
					restaurants.map(restaurant => {
						return this.assetsService.getAsset(restaurant.logoName).pipe(
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
}
