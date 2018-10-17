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
  plants: Plant[];

  constructor(private plantService: PlantService,private speciesService: SpeciesService) {
    
   }

  ngOnInit() {
    this.plantService.getPlants().subscribe((plants: Plant[]) => {
      this.plants = plants;
      console.log(this.plants);
    });
    
  }

}
