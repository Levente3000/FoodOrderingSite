import { Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import {
	MatError,
	MatFormField,
	MatInput,
	MatLabel,
} from '@angular/material/input';
import { MatButton, MatMiniFabButton } from '@angular/material/button';
import { FileUploadComponent } from '../shared/file-upload/file-upload.component';
import { NgForOf } from '@angular/common';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { RestaurantService } from '../services/restaurant.service';
import { MatDialogContent } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditRestaurant } from '../model/restaurant/edit-restaurant.model';

@Component({
	selector: 'app-create-restaurant',
	standalone: true,
	imports: [
		ReactiveFormsModule,
		MatInput,
		MatLabel,
		MatFormField,
		MatMiniFabButton,
		FileUploadComponent,
		NgForOf,
		NgxMaterialTimepickerModule,
		MatButton,
		MatDialogContent,
		MatError,
	],
	templateUrl: './create-edit-restaurant.component.html',
	styleUrl: './create-edit-restaurant.component.scss',
})
export class CreateEditRestaurantComponent implements OnInit {
	protected restaurantForm: FormGroup;
	protected restaurantLogo: File | null = null;
	protected restaurantBanner: File | null = null;
	protected isEditMode: boolean = false;
	protected title: string = 'Create restaurant';
	days = [
		'Monday',
		'Tuesday',
		'Wednesday',
		'Thursday',
		'Friday',
		'Saturday',
		'Sunday',
	];

	constructor(
		private fb: FormBuilder,
		private restaurantService: RestaurantService,
		private _activatedRoute: ActivatedRoute,
		private router: Router,
		private snackBar: MatSnackBar
	) {
		this.restaurantForm = this.fb.group({
			id: [null],
			name: ['', Validators.required],
			description: ['', Validators.required],
			address: ['', Validators.required],
			phoneNumber: [
				'',
				[Validators.required, Validators.pattern(/^\+36[1-9][0-9]{8}$/)],
			],
			logo: [null],
			banner: [null],
		});
		this.days.forEach(day => {
			this.restaurantForm.addControl(day + 'Opening', this.fb.control(null));
			this.restaurantForm.addControl(day + 'Closing', this.fb.control(null));
		});
	}

	public ngOnInit() {
		this._activatedRoute.params.subscribe(params => {
			if (params['id']) {
				this.restaurantService
					.isAuthorized(params['id'])
					.subscribe(isAuthorized => {
						if (!isAuthorized) {
							this.router.navigate(['/not-found']);
						}
					});

				this.isEditMode = true;
				this.title = 'Edit restaurant';
				this.restaurantService
					.getRestaurantById(params['id'])
					.subscribe(restaurantData => {
						if (restaurantData === null) {
							this.router.navigate(['/create-restaurant']);
						} else {
							this.populateForm(restaurantData);
						}
					});
			}
		});
	}

	public submitForm(): void {
		if (
			(!this.restaurantForm.valid ||
				!this.restaurantLogo ||
				!this.restaurantBanner) &&
			!this.isEditMode
		) {
			if (!this.restaurantForm.valid) {
				this.snackBarMessage('The form is invalid!');
			} else {
				this.snackBarMessage('The logo or banner is missing!');
			}
			return;
		}

		if (!this.restaurantForm.valid && this.isEditMode) {
			this.snackBarMessage('The form is invalid!');
			return;
		}

		const formData = new FormData();
		formData.append('name', this.restaurantForm.value.name);
		formData.append('description', this.restaurantForm.value.description);
		formData.append('address', this.restaurantForm.value.address);
		formData.append('phoneNumber', this.restaurantForm.value.phoneNumber);

		if (this.restaurantLogo) {
			formData.append('logo', this.restaurantLogo);
		}
		if (this.restaurantBanner) {
			formData.append('banner', this.restaurantBanner);
		}

		this.days.forEach(day => {
			formData.append(
				'OpeningHours.' + day,
				this.restaurantForm.value[day + 'Opening']
			);
			formData.append(
				'ClosingHours.' + day,
				this.restaurantForm.value[day + 'Closing']
			);
		});
		if (this.isEditMode) {
			formData.append('id', this.restaurantForm.value.id);
			this.restaurantService.editRestaurant(formData).subscribe(result => {
				if (result) {
					this.router.navigate(['/restaurants/details', result]);
				} else {
					this.router.navigate(['/home']);
				}
			});
		} else {
			this.restaurantService.createRestaurant(formData).subscribe(result => {
				if (result) {
					this.router.navigate(['/restaurants/details', result]);
				} else {
					this.router.navigate(['/home']);
				}
			});
		}
	}

	private snackBarMessage(message: string): void {
		this.snackBar.open(message, 'Ok', {
			duration: 5000,
		});
	}

	private populateForm(details: EditRestaurant): void {
		this.restaurantForm.patchValue({
			id: details.id,
			name: details.name,
			description: details.description,
			address: details.address,
			phoneNumber: details.phoneNumber,
		});
		this.days.forEach(day => {
			this.restaurantForm
				.get(day + 'Opening')
				?.patchValue(details.openingHours[day.toLowerCase()]);
			this.restaurantForm
				.get(day + 'Closing')
				?.patchValue(details.closingHours[day.toLowerCase()]);
		});
	}
}
