import { PaginationComponent } from './../shared/pagination/pagination.component';
import { OrderService } from './../../services/order.service';
import { Plant } from './../plant.model';
import { Component, OnInit } from '@angular/core';
import { PlantService } from './../../services/plant.service';
import { SpeciesService } from './../../services/species.service';
import { FeatureService } from './../../services/feature.service';
import { collectExternalReferences } from '@angular/compiler';

interface query{plants: Plant[]; totalCount: number;}

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
  totalCount={};
  filter: any = {
    selectedFeatures: []
  };
  query: any = {
    pageSize: 4
  }
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
    this.populatePlants(this.query);    
  }
  populatePlants(query){
    this.plantService.getPlants(query).subscribe((result:query) => {
      this.displayedPlants = this.allPlants = result.plants;
      this.totalCount = result.totalCount;    
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

  onPageChanged(page){
    this.query.page=page;
    this.populatePlants(this.query);
  }

  sortBy(columnHeader){
    if(this.query.sortBy === columnHeader){
      if(this.query.isAscending == true)
        this.query.isAscending = false;
      else 
        this.query.isAscending = true;
    }

    else{
      this.query.sortBy = columnHeader;
      this.query.isAscending = true;
    }
    console.log(this.query)
    this.populatePlants(this.query);
  }
  resetFilter(){
    this.filter={selectedFeatures: []};
    this.onFilterChange();
  }

}
