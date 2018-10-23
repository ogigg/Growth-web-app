import { OrderService } from './../../services/order.service';
import { Plant } from './../plant.model';
import { Component, OnInit } from '@angular/core';
import { PlantService } from './../../services/plant.service';
import { SpeciesService } from './../../services/species.service';
import { FeatureService } from './../../services/feature.service';



@Component({
  selector: 'app-plant-list',
  templateUrl: './plant-list.component.html',
  styleUrls: ['./plant-list.component.css']
})
export class PlantListComponent implements OnInit {
  displayedPlants: Plant[];
  allPlants
  orders: any[];
  species: any[];
  features: any[];
  filter: any = {
    selectedFeatures: []
  };

  constructor(private plantService: PlantService,
    private speciesService: SpeciesService, 
    private orderService: OrderService,
    private featureService: FeatureService
    ) {
    
   }

  ngOnInit() {
    this.orderService.getOrders().subscribe((orders: any[]) => this.orders = orders);
    this.speciesService.getSpecies().subscribe((species: any[]) => this.species = species);
    this.featureService.getFeatures().subscribe((features: any[]) => this.features = features);
    this.plantService.getPlants().subscribe((plants: Plant[]) => {
      this.displayedPlants = this.allPlants = plants;
    });
    
  }
  onFilterChange(){
    var plants = this.allPlants;
    if(this.filter.orderId)
    {
      plants=plants.filter(p=>p.orderId==this.filter.orderId);
    }
    if(this.filter.speciesId)
    {
      plants=plants.filter(p=>p.speciesId==this.filter.speciesId);
    }
    if(this.filter.selectedFeatures)
    {
      this.filter.selectedFeatures.forEach((feature, id) => {
        if(feature)
         plants=plants.filter(p=>p.features.some(f=>f==id));
      });
    }
    this.displayedPlants=plants;
  }

  resetFilter(){
    this.filter={selectedFeatures: []};
    this.onFilterChange();
  }

}
