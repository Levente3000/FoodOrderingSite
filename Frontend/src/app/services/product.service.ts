import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { baseUrl } from '../../global';
import { EditProduct } from '../model/edit-product.model';

@Injectable({
	providedIn: 'root',
})
export class ProductService {
	private controllerUrl = 'product';

	constructor(private readonly httpClient: HttpClient) {}

	public getProductForEdit(productId: number): Observable<EditProduct> {
		return this.httpClient.get<EditProduct>(
			`${baseUrl}/${this.controllerUrl}/${productId}`
		);
	}

	public createProduct(formData: FormData): Observable<number | null> {
		return this.httpClient.post<number | null>(
			`${baseUrl}/${this.controllerUrl}/create-product`,
			formData
		);
	}

	public editProduct(formData: FormData): Observable<number | null> {
		return this.httpClient.post<number | null>(
			`${baseUrl}/${this.controllerUrl}/edit-product`,
			formData
		);
	}
}
