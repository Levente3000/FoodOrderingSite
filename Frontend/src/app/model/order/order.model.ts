import { Product } from '../product.model';

export type Order = {
	id: number;
	ordererName: string;
	ordererAddress: string;
	ordererPhoneNumber: string;
	isDone: boolean;
	restaurantId: number;
	createdAt: string;
	orderItems: OrderItem[];
};

export type OrderWithDate = {
	id: number;
	ordererName: string;
	ordererAddress: string;
	ordererPhoneNumber: string;
	isDone: boolean;
	restaurantId: number;
	createdAt: Date;
	orderItems: OrderItem[];
};

export type OrderItem = {
	product: Product;
	quantity: number;
};
