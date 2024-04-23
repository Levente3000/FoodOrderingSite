import { Product } from './product.model';

export type ShoppingCartItem = {
	shoppingCartItemId: number;
	userId: string;
	quantity: number;
	product: Product;
};
