import { Injectable } from '@angular/core';
import { Restaurant } from '../model/restaurant.model';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from '../../global';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class HomeService {
	constructor(private readonly httpClient: HttpClient) {}

	public getAllRestaurants(): Observable<Restaurant[]> {
		return this.httpClient.get<Restaurant[]>(`${baseUrl}/restaurant`);
	}
}
