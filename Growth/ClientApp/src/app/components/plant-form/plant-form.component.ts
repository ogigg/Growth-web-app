import { Component, OnInit } from '@angular/core';
import {OrderService} from '../../services/order.service';


@Component({
  selector: 'app-plant-form',
  templateUrl: './plant-form.component.html',
  styleUrls: ['./plant-form.component.css']
})
export class PlantFormComponent implements OnInit {
  orders: any[];
  species: any[];
  plant:any = {};
  constructor(private orderService : OrderService) { }

  ngOnInit() {
    this.orderService.getOrders().subscribe(orders => {
      this.orders = orders;
    });
  }
  onOrderChange() {
    var selectedOrder = this.orders.find(o => o.id == this.plant.order);
    this.species = selectedOrder ? selectedOrder.species : [];
  }

}
