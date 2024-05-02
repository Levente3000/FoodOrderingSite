import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, mergeMap, Observable } from 'rxjs';
import { baseUrl } from '../../global';
import { Category } from '../model/category.model';
import { AssetsService } from './assets.service';

@Injectable({
	providedIn: 'root',
})
export class CategoryService {
	private controllerUrl = 'category';

	constructor(
		private readonly httpClient: HttpClient,
		private readonly assetsService: AssetsService
	) {}

	public getCategoriesWithLogo(): Observable<Category[]> {
		return this.httpClient
			.get<Category[]>(`${baseUrl}/${this.controllerUrl}`)
			.pipe(
				mergeMap(categories =>
					forkJoin(
						categories.map(category => {
							return this.assetsService
								.getAssetForCategory(category.pictureName)
								.pipe(
									map(asset => {
										category.logo = asset;
										return category;
									})
								);
						})
					)
				)
			);
	}

	public getCategories(): Observable<string[]> {
		return this.httpClient.get<string[]>(
			`${baseUrl}/${this.controllerUrl}/names`
		);
	}
}
