import { Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatDialogActions, MatDialogContent } from '@angular/material/dialog';
import { EditProduct } from '../../model/edit-product.model';
import { FoodCategoryChipAutocompleteComponent } from './food-category-chip-autocomplete/food-category-chip-autocomplete.component';
import { CategoryService } from '../../services/category.service';
import { MatError, MatFormField } from '@angular/material/form-field';
import { MatInput, MatLabel } from '@angular/material/input';
import { MatCheckbox } from '@angular/material/checkbox';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductService } from '../../services/product.service';
import { FileUploadComponent } from '../../shared/file-upload/file-upload.component';

@Component({
	selector: 'app-create-edit-product',
	standalone: true,
	imports: [
		FormsModule,
		MatError,
		MatButton,
		MatDialogActions,
		MatDialogContent,
		ReactiveFormsModule,
		FoodCategoryChipAutocompleteComponent,
		MatFormField,
		MatInput,
		MatLabel,
		MatCheckbox,
		FileUploadComponent,
	],
	templateUrl: './create-edit-product.component.html',
	styleUrl: './create-edit-product.component.scss',
})
export class CreateEditProductComponent implements OnInit {
	protected formGroup: FormGroup;
	protected allFoodCategories: string[] = [];
	protected productPicture: File | null = null;
	protected isEditMode: boolean = false;
	protected title = 'Create product';

	constructor(
		public fb: FormBuilder,
		public categoryService: CategoryService,
		public productService: ProductService,
		private _activatedRoute: ActivatedRoute,
		private router: Router,
		private snackBar: MatSnackBar
	) {
		this.formGroup = this.fb.group({
			restaurantId: [null],
			productId: [null],
			name: ['', Validators.required],
			description: ['', Validators.required],
			price: [null, [Validators.required, Validators.min(0)]],
			isEnabled: [true, Validators.required],
			categoryNames: [[], Validators.required],
			picture: [null],
		});
	}

	public ngOnInit(): void {
		this._activatedRoute.params.subscribe(params => {
			if (params['productId']) {
				this.isEditMode = true;
				this.title = 'Edit product';
				this.productService
					.getProductForEdit(params['productId'])
					.subscribe(restaurantData => {
						if (restaurantData === null) {
							this.router.navigate(['/create-product']);
						} else {
							this.populateForm(restaurantData);
						}
					});
			}
		});

		this.categoryService.getCategories().subscribe(categories => {
			this.allFoodCategories = categories;
		});
	}

	public submitForm(): void {
		if ((!this.formGroup.valid || !this.productPicture) && !this.isEditMode) {
			if (!this.formGroup.valid) {
				this.snackBarMessage('The form is invalid!');
			} else {
				this.snackBarMessage('The logo or banner is missing!');
			}
			return;
		}

		if (!this.formGroup.valid && this.isEditMode) {
			this.snackBarMessage('The form is invalid!');
			return;
		}

		const formData = new FormData();
		formData.append('restaurantId', this.formGroup.value.restaurantId);
		formData.append('name', this.formGroup.value.name);
		formData.append('description', this.formGroup.value.description);
		formData.append('price', this.formGroup.value.price);
		formData.append('isEnabled', this.formGroup.value.isEnabled);

		if (this.productPicture) {
			formData.append('picture', this.productPicture);
		}

		if (this.isEditMode) {
			formData.append('id', this.formGroup.value.productId);
			this.productService.editProduct(formData).subscribe(result => {
				if (result) {
					this.router.navigate(['/restaurants/details', result]);
				} else {
					this.router.navigate(['/home']);
				}
			});
		} else {
			this.productService.createProduct(formData).subscribe(result => {
				if (result) {
					this.router.navigate(['/restaurants/details', result]);
				} else {
					this.router.navigate(['/home']);
				}
			});
		}
	}

	private populateForm(details: EditProduct): void {
		this.formGroup.patchValue({
			restaurantId: details.restaurantId,
			productId: details.id,
			name: details.name,
			description: details.description,
			price: details.price,
			isEnabled: details.isEnabled,
			categoryNames: details.categoryNames,
		});
	}

	private snackBarMessage(message: string): void {
		this.snackBar.open(message, 'Ok', {
			duration: 5000,
		});
	}
}
