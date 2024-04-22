import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgClass } from '@angular/common';

@Component({
	selector: 'app-quantity',
	standalone: true,
	imports: [NgClass],
	templateUrl: './quantity.component.html',
	styleUrl: './quantity.component.scss',
})
export class QuantityComponent {
	@Input() quantity!: number;
	@Output() quantityChange = new EventEmitter<number>();
	@Input() isEnabled?: boolean;
	@Input() isResponsive: boolean = false;

	protected get isQuantityOne(): boolean {
		return this.quantity === 1;
	}

	public addProduct(): void {
		this.changeQuantity(+1);
	}

	public subtractProduct(): void {
		if (this.quantity > 1) {
			this.changeQuantity(-1);
		}
	}

	public changeQuantity(number: number) {
		this.quantity += number;
		this.quantityChange.emit(this.quantity);
	}
}
