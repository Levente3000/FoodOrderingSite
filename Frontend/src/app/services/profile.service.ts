import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { baseUrl } from '../../global';
import { UserData } from '../model/user-data.model';

@Injectable({
	providedIn: 'root',
})
export class ProfileService {
	private controllerUrl = 'user';

	constructor(private readonly httpClient: HttpClient) {}

	public getProfileInfo(): Observable<UserData> {
		return this.httpClient.get<UserData>(
			`${baseUrl}/${this.controllerUrl}/user-data`
		);
	}

	public getCanPlaceOrder(): Observable<boolean> {
		return this.httpClient.get<boolean>(
			`${baseUrl}/${this.controllerUrl}/user-has-data`
		);
	}

	public UpdateProfileInfo(formData: FormData): Observable<void> {
		return this.httpClient.post<void>(
			`${baseUrl}/${this.controllerUrl}/update-user-data`,
			formData
		);
	}
}
