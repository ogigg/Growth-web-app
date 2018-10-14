import { PlantService } from './../../services/plant.service';
import { FeatureService } from './../../services/feature.service';
import { Component, OnInit } from '@angular/core';
import {OrderService} from '../../services/order.service';


@Component({
  selector: 'app-plant-form',
  templateUrl: './plant-form.component.html',
  styleUrls: ['./plant-form.component.css']
})
export class PlantFormComponent implements OnInit {
  orders: any;
  species: any;
  features: any;
  plant:any = {};
  constructor(
    private orderService : OrderService,
    private featureService: FeatureService,
    private plantService: PlantService
    
  ) { this.orderService.getOrders().subscribe(orders => this.orders = orders);
    this.featureService.getFeatures().subscribe(features => this.features = features);
  }

  ngOnInit() {

  }
  onOrderChange() {
    var selectedOrder = this.orders.find(o => o.id == this.plant.order);
    this.species = selectedOrder ? selectedOrder.species : [];
  }

  onSubmit(form) {
    console.log(form.value);
      this.plantService.createPlant(form.value)
      .subscribe(
        data => {
            console.log(data);
        },
        error => {
            console.log("Error", error);

        });
    
      }
}
