import { ImageService } from './../../services/image.service';
import { PlantService } from './../../services/plant.service';
import { FeatureService } from './../../services/feature.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Plant } from '../plant.model';




@Component({
  selector: 'app-plant-form',
  templateUrl: './plant-form.component.html',
  styleUrls: ['./plant-form.component.css']
})
export class PlantFormComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  plantId :number;
  orders: any;
  species: any;
  features: any;
  plant: any = {
    features: []
  };
  constructor(
    private orderService : OrderService,
    private featureService: FeatureService,
    private plantService: PlantService,
    private imageService: ImageService
    
  ) { this.orderService.getOrders().subscribe(orders => this.orders = orders);
    this.featureService.getFeatures().subscribe(features => this.features = features);
  }

  ngOnInit() {

  }
  onOrderChange() {
    var selectedOrder = this.orders.find(o => o.id == this.plant.orderId);
    this.species = selectedOrder ? selectedOrder.species : [];
  }

  onChangeFeature(featureId,$event){
    if($event.target.checked)
      this.plant.features.push(featureId);
    else{
      var index = this.plant.features.indexOf(featureId);
      this.plant.features.splice(index,1);
    }
  }

  onSubmit() {
    console.log(this.plant)
      this.plantService.createPlant(this.plant)
      .subscribe(
        (data: Plant) => {
            this.plantId = data.id;
            var nativeElement: HTMLInputElement =  this.fileInput.nativeElement;
            this.imageService.uploadImage(this.plantId,nativeElement.files[0]).subscribe(data => console.log(data));
            console.log("Nadana nowa wartosc id: " + this.plantId)
        },
        error => {
            console.log("Error", error);

        });
      
    
      }
}
