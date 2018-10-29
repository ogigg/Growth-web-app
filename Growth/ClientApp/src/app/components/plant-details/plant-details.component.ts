import { ImageService } from './../../services/image.service';
import { FeatureService } from './../../services/feature.service';
import { ActivatedRoute } from '@angular/router';
import { Plant } from './../plant.model';
import { PlantService } from './../../services/plant.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-plant-details',
  templateUrl: './plant-details.component.html',
  styleUrls: ['./plant-details.component.css']
})
export class PlantDetailsComponent implements OnInit {

  plant: Plant;
  image: any;
  plantFeatures: any[]=[];
  id: number;

  constructor(private route: ActivatedRoute, 
    private plantService: PlantService,
    private featureService: FeatureService,
    private imageService: ImageService
    ) {
    this.route.paramMap.subscribe(params => this.id = +params.get('id'));
    
    this.plantService.getPlant(this.id).subscribe((plant: Plant) => {
      console.log(plant)
      this.plant = plant;
      this.featureService.getFeatures().subscribe((features:any)=>{
        features.forEach(feature => {
          if(this.plant.features.some(f=>f==feature.id))
            this.plantFeatures.push(feature);
        });
      });
    })
    this.imageService.getImages(this.id).subscribe(image=>this.image=image);
   }

  ngOnInit() {
    
  }

}
