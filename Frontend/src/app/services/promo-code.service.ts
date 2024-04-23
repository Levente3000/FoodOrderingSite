import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { baseUrl } from '../../global';
import { HttpClient } from '@angular/common/http';
import { PromoCode } from '../model/promo-code.model';

@Injectable({
	providedIn: 'root',
})
export class PromoCodeService {
	constructor(private readonly httpClient: HttpClient) {}

	public getPromoCodes(): Observable<PromoCode[]> {
		return this.httpClient.get<PromoCode[]>(`${baseUrl}/promo-code`);
	}
}
