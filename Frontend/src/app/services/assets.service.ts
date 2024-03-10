import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { baseUrl } from '../../global';

@Injectable({
	providedIn: 'root',
})
export class AssetsService {
	private AssetPath = 'assets';

	constructor(private readonly httpClient: HttpClient) {}

	public getAssetForRestaurant(fileName: string): Observable<string> {
		return this.httpClient
			.get(`${baseUrl}/${this.AssetPath}/restaurant?assetName=${fileName}`, {
				responseType: 'blob',
			})
			.pipe(map(asset => URL.createObjectURL(asset)));
	}

	public getAssetForCategory(fileName: string): Observable<string> {
		return this.httpClient
			.get(`${baseUrl}/${this.AssetPath}/category?assetName=${fileName}`, {
				responseType: 'blob',
			})
			.pipe(map(asset => URL.createObjectURL(asset)));
	}
}
