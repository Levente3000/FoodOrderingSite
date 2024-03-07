import { Component, Input } from '@angular/core';
import { Category } from '../model/category.model';
import { RouterLink } from '@angular/router';

@Component({
	selector: 'app-category-card',
	standalone: true,
	imports: [RouterLink],
	templateUrl: './category-card.component.html',
	styleUrl: './category-card.component.scss',
})
export class CategoryCardComponent {
	@Input() category?: Category;
}
