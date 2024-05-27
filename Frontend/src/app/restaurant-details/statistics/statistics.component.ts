import { Component, OnInit } from '@angular/core';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { RestaurantService } from '../../services/restaurant.service';
import { OrderWithDate } from '../../model/order/order.model';
import { MatButton } from '@angular/material/button';

@Component({
	selector: 'app-statistics',
	standalone: true,
	imports: [CanvasJSAngularChartsModule, MatButton],
	templateUrl: './statistics.component.html',
	styleUrl: './statistics.component.scss',
})
export class StatisticsComponent implements OnInit {
	protected restaurantId?: number;
	protected data: OrderWithDate[] = [];
	protected months = [
		{ name: 'January', number: 0 },
		{ name: 'February', number: 1 },
		{ name: 'March', number: 2 },
		{ name: 'April', number: 3 },
		{ name: 'May', number: 4 },
		{ name: 'June', number: 5 },
		{ name: 'July', number: 6 },
		{ name: 'August', number: 7 },
		{ name: 'September', number: 8 },
		{ name: 'October', number: 9 },
		{ name: 'November', number: 10 },
		{ name: 'December', number: 11 },
	];

	public numberOfOrdersChartOptions = {
		title: {
			text: 'Number of orders this year',
		},
		theme: 'dark1',
		animationEnabled: true,
		exportEnabled: true,
		axisY: {
			includeZero: true,
		},
		data: [
			{
				type: 'column',
				color: '#01b8aa',
				dataPoints: [
					{ label: 'January', y: 0 },
					{ label: 'February', y: 0 },
					{ label: 'March', y: 0 },
					{ label: 'April', y: 0 },
					{ label: 'May', y: 0 },
					{ label: 'June', y: 0 },
					{ label: 'July', y: 0 },
					{ label: 'August', y: 0 },
					{ label: 'September', y: 0 },
					{ label: 'October', y: 0 },
					{ label: 'November', y: 0 },
					{ label: 'December', y: 0 },
				],
			},
		],
	};

	public monthlyTotalChartOptions = {
		title: {
			text: 'Monthly total',
		},
		theme: 'dark1',
		animationEnabled: true,
		exportEnabled: true,
		axisY: {
			includeZero: true,
			valueFormatString: '#,##0 Ft',
		},
		data: [
			{
				type: 'column',
				yValueFormatString: '#,##0 Ft',
				color: '#01b8aa',
				dataPoints: [
					{ label: 'January', y: 0 },
					{ label: 'February', y: 0 },
					{ label: 'March', y: 0 },
					{ label: 'April', y: 0 },
					{ label: 'May', y: 0 },
					{ label: 'June', y: 0 },
					{ label: 'July', y: 0 },
					{ label: 'August', y: 0 },
					{ label: 'September', y: 0 },
					{ label: 'October', y: 0 },
					{ label: 'November', y: 0 },
					{ label: 'December', y: 0 },
				],
			},
		],
	};

	public quantityOfProductsChartOptions = {
		title: {
			text: 'Quantity of sold products',
		},
		theme: 'dark1',
		exportEnabled: true,
		data: [
			{
				type: 'pie',
				startAngle: 0,
				indexLabel: '{name}: {y}',
				indexLabelPlacement: 'auto',
				yValueFormatString: "#,### 'piece'",
				dataPoints: [{ y: 0, name: 'nothing' }],
			},
		],
	};

	constructor(
		private _activatedRoute: ActivatedRoute,
		private orderService: OrderService,
		private restaurantService: RestaurantService,
		private router: Router
	) {}

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

				this.orderService
					.getOrdersByRestaurantId(params['id'])
					.subscribe(orders => {
						this.data = orders;
						this.makeChartData();
					});
				this.restaurantId = params['id'];
			} else {
				this.router.navigate(['/home']);
			}
		});
	}

	public makeChartData(): void {
		const numberOfOrders: number[] = new Array(12).fill(0);
		const monthlyTotal: number[] = new Array(12).fill(0);
		this.data.map(order => {
			const monthIndex = order.createdAt.getMonth();

			numberOfOrders[monthIndex]++;

			order.orderItems.forEach(item => {
				const itemTotal = item.product.price * item.quantity; // Multiply price by quantity
				monthlyTotal[monthIndex] += itemTotal; // Add to the total for the month
			});
		});

		const newChartOptions = {
			...this.numberOfOrdersChartOptions,
			data: [
				{
					type: 'column',
					color: '#01b8aa',
					dataPoints: this.months.map((month, index) => ({
						label: month.name,
						y: numberOfOrders[index],
					})),
				},
			],
		};

		this.numberOfOrdersChartOptions = newChartOptions;

		const newMonthlyTotalChartOption = {
			...this.monthlyTotalChartOptions,
			data: [
				{
					type: 'column',
					yValueFormatString: '#,##0 Ft',
					color: '#01b8aa',
					dataPoints: this.months.map((month, index) => ({
						label: month.name,
						y: monthlyTotal[index],
					})),
				},
			],
		};

		this.monthlyTotalChartOptions = newMonthlyTotalChartOption;

		const newQuantityOfProductsChartOptionsChartOption = {
			...this.quantityOfProductsChartOptions,
			data: [
				{
					type: 'pie',
					startAngle: 0,
					indexLabel: '{name}: {y}',
					indexLabelPlacement: 'auto',
					yValueFormatString: "#,### 'piece'",
					dataPoints: this.getProductQuantities(),
				},
			],
		};

		this.quantityOfProductsChartOptions =
			newQuantityOfProductsChartOptionsChartOption;
	}

	public getProductQuantities(): {
		name: string;
		y: number;
	}[] {
		const productQuantities: { [key: string]: number } = {};

		this.data.forEach(order => {
			order.orderItems.forEach(item => {
				const productName = item.product.name;
				if (!productQuantities[productName]) {
					productQuantities[productName] = 0;
				}
				productQuantities[productName] += item.quantity;
			});
		});

		return Object.keys(productQuantities).map(key => ({
			name: key,
			y: productQuantities[key],
		}));
	}

	public backToRestaurant(): void {
		this.router.navigate(['/restaurants/details', this.restaurantId]);
	}
}
