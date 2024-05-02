import { Product } from '../product.model';

export type Order = {
	id: number;
	ordererName: string;
	ordererAddress: string;
	ordererPhoneNumber: string;
	isDone: boolean;
	restaurantId: number;
	orderItems: OrderItem[];
};

export type OrderItem = {
	product: Product;
	quantity: number;
};
