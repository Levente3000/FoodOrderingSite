export type EditProduct = {
	id: number;
	name: string;
	description: string;
	price: number;
	isEnabled: boolean;
	categoryNames: string[];
	restaurantId: number;
};
