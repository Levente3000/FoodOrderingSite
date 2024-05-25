import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { baseUrl } from '../../global';
import { PromoCode } from '../model/promo-code.model';
import { Order, OrderWithDate } from '../model/order/order.model';

@Injectable({
	providedIn: 'root',
})
export class OrderService {
	private controllerUrl = 'order';

	constructor(private readonly httpClient: HttpClient) {}

	public placeOrder(promo: PromoCode | null): Observable<void> {
		return this.httpClient.post<void>(
			`${baseUrl}/${this.controllerUrl}/place-order`,
			promo
		);
	}

	public getOrdersByRestaurantId(
		restaurantId: number
	): Observable<OrderWithDate[]> {
		return this.httpClient
			.get<OrderWithDate[]>(`${baseUrl}/${this.controllerUrl}/${restaurantId}`)
			.pipe(
				map(orders => {
					return orders.map(order => {
						order.createdAt = new Date(order.createdAt);
						return order;
					});
				})
			);
	}

	public getActiveOrderByRestaurantId(
		restaurantId: number
	): Observable<Order[]> {
		return this.httpClient.get<Order[]>(
			`${baseUrl}/${this.controllerUrl}/active/${restaurantId}`
		);
	}

	public getDoneOrderByRestaurantId(restaurantId: number): Observable<Order[]> {
		return this.httpClient.get<Order[]>(
			`${baseUrl}/${this.controllerUrl}/done/${restaurantId}`
		);
	}

	public getCanPlaceOrder(): Observable<boolean> {
		return this.httpClient.get<boolean>(
			`${baseUrl}/${this.controllerUrl}/user-has-data`
		);
	}

	public updateOrder(orderId: number): Observable<void> {
		return this.httpClient.post<void>(
			`${baseUrl}/${this.controllerUrl}/update-order`,
			orderId
		);
	}
}
