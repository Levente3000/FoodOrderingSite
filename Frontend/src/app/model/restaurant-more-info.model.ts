import { OpeningHours } from './opening-hours.model';

export type RestaurantMoreInfo = {
	restaurantName: string;
	address: string;
	phoneNumber: string;
	openingHours: OpeningHours;
	closingHours: OpeningHours;
};
