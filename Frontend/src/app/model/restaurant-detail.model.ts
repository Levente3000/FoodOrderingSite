import { OpeningHours } from './opening-hours.model';
import { Product } from './product.model';

export type RestaurantDetail = {
	name: string;
	description: string;
	address: string;
	phoneNumber: string;
	logoName: string;
	logo?: string;
	bannerName: string;
	banner?: string;
	priceCategory: number;
	categoriesWithProducts: CategoriesWithProducts[];
	openingHours: OpeningHours;
	closingHours: OpeningHours;
};

export type CategoriesWithProducts = {
	name: string;
	products: Product[];
};
