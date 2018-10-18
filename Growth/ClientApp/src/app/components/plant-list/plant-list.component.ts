import { OrderService } from './../../services/order.service';
import { Plant } from './../plant.model';
import { Component, OnInit } from '@angular/core';
import { PlantService } from './../../services/plant.service';
import { SpeciesService } from './../../services/species.service';



@Component({
  selector: 'app-plant-list',
  templateUrl: './plant-list.component.html',
  styleUrls: ['./plant-list.component.css']
})
export class PlantListComponent implements OnInit {
  displayedPlants: Plant[];
  allPlants
  orders: any[];
  filter: any = {};

  constructor(private plantService: PlantService,private speciesService: SpeciesService, private orderService: OrderService) {
    
   }

  ngOnInit() {
    this.orderService.getOrders().subscribe((orders: any[]) => this.orders = orders);
    this.plantService.getPlants().subscribe((plants: Plant[]) => {
      this.displayedPlants = this.allPlants = plants;
      console.log(this.displayedPlants);
    });
    
  }
  onFilterChange(){
    var plants = this.allPlants;
    if(this.filter.orderId)
    {
      plants=plants.filter(p=>p.orderId==this.filter.orderId);
    }
    this.displayedPlants=plants;
  }
  resetFilter(){
    this.filter={};
    this.onFilterChange();
  }

}
