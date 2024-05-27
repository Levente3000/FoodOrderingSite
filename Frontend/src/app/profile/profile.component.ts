import { Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatButton } from '@angular/material/button';
import { MatInput } from '@angular/material/input';
import { ProfileService } from '../services/profile.service';
import { UserData } from '../model/user-data.model';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-profile',
	standalone: true,
	imports: [
		MatLabel,
		MatFormField,
		ReactiveFormsModule,
		MatButton,
		MatInput,
		MatError,
	],
	templateUrl: './profile.component.html',
	styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
	protected formGroup: FormGroup;

	constructor(
		private fb: FormBuilder,
		private profileService: ProfileService,
		private router: Router,
		private snackBar: MatSnackBar
	) {
		this.formGroup = this.fb.group({
			name: [''],
			email: [''],
			address: ['', Validators.required],
			phone: [
				'',
				[Validators.required, Validators.pattern(/^\+36[1-9][0-9]{8}$/)],
			],
		});
	}

	ngOnInit() {
		this.profileService.getProfileInfo().subscribe(userData => {
			if (userData) {
				this.populateData(userData);
			}
		});
	}

	public onSubmit(): void {
		if (this.formGroup.valid) {
			const formData = new FormData();
			formData.append('name', this.formGroup.value.name);
			formData.append('email', this.formGroup.value.email);
			formData.append('address', this.formGroup.value.address);
			formData.append('phone', this.formGroup.value.phone);

			this.profileService.UpdateProfileInfo(formData).subscribe(() => {
				this.router.navigate(['/shopping-cart']);
				this.snackBar.open('Profile successfully updated!', 'Ok', {
					duration: 5000,
				});
			});
		}
	}

	public populateData(data: UserData): void {
		this.formGroup.patchValue({
			name: data.name,
			email: data.email,
			address: data.address,
			phone: data.phone,
		});
	}
}
