import { Component, Inject } from '@angular/core';
import {
	MAT_DIALOG_DATA,
	MatDialogActions,
	MatDialogContent,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatButton } from '@angular/material/button';
import { MatFormField } from '@angular/material/form-field';
import { MatOption } from '@angular/material/autocomplete';
import { MatSelect } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { OpeningHours } from '../../model/opening-hours.model';
import { DatePipe, NgIf } from '@angular/common';
import { RestaurantMoreInfo } from '../../model/restaurant/restaurant-more-info.model';

class OpeningHoursInDate {
	monday?: Date;
	tuesday?: Date;
	thursday?: Date;
	wednesday?: Date;
	friday?: Date;
	saturday?: Date;
	sunday?: Date;

	constructor(openingHoursInString: OpeningHours) {
		this.monday =
			openingHoursInString.monday === null
				? undefined
				: new Date(openingHoursInString.monday);
		this.tuesday =
			openingHoursInString.tuesday === null
				? undefined
				: new Date(openingHoursInString.tuesday);
		this.thursday =
			openingHoursInString.thursday === null
				? undefined
				: new Date(openingHoursInString.thursday);
		this.wednesday =
			openingHoursInString.wednesday === null
				? undefined
				: new Date(openingHoursInString.wednesday);
		this.friday =
			openingHoursInString.friday === null
				? undefined
				: new Date(openingHoursInString.friday);
		this.saturday =
			openingHoursInString.saturday === null
				? undefined
				: new Date(openingHoursInString.saturday);
		this.sunday =
			openingHoursInString.sunday === null
				? undefined
				: new Date(openingHoursInString.sunday);
	}
}

@Component({
	selector: 'app-restaurant-more-info-dialog',
	standalone: true,
	imports: [
		MatDialogContent,
		MatButton,
		MatDialogActions,
		MatFormField,
		MatOption,
		MatSelect,
		ReactiveFormsModule,
		DatePipe,
		DatePipe,
		NgIf,
	],
	templateUrl: './restaurant-more-info-dialog.component.html',
	styleUrl: './restaurant-more-info-dialog.component.scss',
})
export class RestaurantMoreInfoDialogComponent {
	protected openingHours: OpeningHoursInDate;
	protected closingHours: OpeningHoursInDate;

	constructor(
		public dialogRef: MatDialogRef<RestaurantMoreInfoDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: RestaurantMoreInfo
	) {
		this.openingHours = new OpeningHoursInDate(data.openingHours);
		this.closingHours = new OpeningHoursInDate(data.closingHours);
		this.dialogRef.backdropClick().subscribe(() => this.dialogRef.close());
	}

	public onCancelClick(): void {
		this.dialogRef.close();
	}
}
