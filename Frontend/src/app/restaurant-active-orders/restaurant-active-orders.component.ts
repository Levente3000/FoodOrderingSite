import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import {
	MatCell,
	MatCellDef,
	MatColumnDef,
	MatHeaderCell,
	MatHeaderCellDef,
	MatHeaderRow,
	MatHeaderRowDef,
	MatRow,
	MatRowDef,
	MatTable,
	MatTableDataSource,
} from '@angular/material/table';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { OrderService } from '../services/order.service';
import { Order } from '../model/order/order.model';
import { MatPaginator } from '@angular/material/paginator';
import { RestaurantService } from '../services/restaurant.service';

@Component({
	selector: 'app-restaurant-orders',
	standalone: true,
	animations: [
		trigger('detailExpand', [
			state('collapsed,void', style({ height: '0px', minHeight: '0' })),
			state('expanded', style({ height: '*' })),
			transition(
				'expanded <=> collapsed',
				animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
			),
		]),
	],
	imports: [
		MatHeaderRow,
		MatRow,
		MatCell,
		MatColumnDef,
		MatIconButton,
		MatIcon,
		MatHeaderCell,
		MatTable,
		MatHeaderRowDef,
		MatRowDef,
		MatCellDef,
		MatHeaderCellDef,
		MatPaginator,
		RouterLink,
		MatButton,
	],
	templateUrl: './restaurant-active-orders.component.html',
	styleUrl: './restaurant-active-orders.component.scss',
})
export class RestaurantActiveOrdersComponent implements OnInit, AfterViewInit {
	protected dataSource = new MatTableDataSource<Order>();
	protected columnsToDisplay = [
		'ordererName',
		'ordererAddress',
		'ordererPhoneNumber',
	];
	protected columnsToDisplayWithExpand = [
		...this.columnsToDisplay,
		'isDone',
		'expand',
	];
	protected expandedElement: Order | null = null;
	protected restaurantId?: number;

	@ViewChild(MatPaginator) paginator!: MatPaginator;

	public orderTotal(order: Order): number {
		return order.orderItems.reduce((pastValue, item) => {
			return pastValue + item.quantity * item.product.price;
		}, 0);
	}

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
					.getActiveOrderByRestaurantId(params['id'])
					.subscribe(orders => {
						this.dataSource.data = orders;
					});
				this.restaurantId = params['id'];
			} else {
				this.router.navigate(['/home']);
			}
		});
	}

	public ngAfterViewInit(): void {
		this.dataSource.paginator = this.paginator;
	}

	public routeToInactiveOrders(): void {
		this.router.navigate(['/restaurant-orders/inactive', this.restaurantId]);
	}

	public doneClick(event: Event, order: Order): void {
		event.stopPropagation();
		this.dataSource.data = this.dataSource.data.filter(data => data !== order);
		this.orderService.updateOrder(order.id).subscribe();
	}

	public backToRestaurant(): void {
		this.router.navigate(['/restaurants/details', this.restaurantId]);
	}
}
