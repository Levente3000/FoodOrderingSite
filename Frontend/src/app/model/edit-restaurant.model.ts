import { OpeningHours } from './opening-hours.model';

export type EditRestaurant = {
	id: number;
	name: string;
	description: string;
	address: string;
	phoneNumber: string;
	openingHours: OpeningHours;
	closingHours: OpeningHours;
};
