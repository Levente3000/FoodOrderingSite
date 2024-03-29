import { OpeningHours } from './opening-hours.model';
import { Product } from './product.model';

export type Restaurant = {
	id: number;
	name: string;
	description: string;
	address: string;
	phoneNumber: string;
	logoName: string;
	logo?: string;
	bannerName: string;
	banner?: string;
	priceCategory: number;
	openingHours: OpeningHours;
	closingHours: OpeningHours;
	products: Product[];
};
