import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { baseUrl } from '../../global';

@Injectable({
	providedIn: 'root',
})
export class PriceCategoryService {
	constructor(private readonly httpClient: HttpClient) {}

	public getPriceCategories(): Observable<number[]> {
		return this.httpClient.get<number[]>(`${baseUrl}/priceCategory`);
	}
}
