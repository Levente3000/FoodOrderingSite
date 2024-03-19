export type Product = {
	id: number;
	name: string;
	description: string;
	price: number;
	pictureName: string;
	picture: string | null;
	isEnabled: boolean;
	categoryNames: string[];
	restaurantId: number;
};
