import { OpeningHours } from './opening-hours.model';

export type Restaurant = {
	id: number;
	name: string;
	description: string;
	address: string;
	phoneNumber: string;
	logoName: string;
	logo: string | null;
	openingHours: OpeningHours;
	closingHours: OpeningHours;
};
