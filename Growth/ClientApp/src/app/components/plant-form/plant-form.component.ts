import { ToastrService } from 'ngx-toastr';
import { ImageService } from './../../services/image.service';
import { PlantService } from './../../services/plant.service';
import { FeatureService } from './../../services/feature.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Plant } from '../plant.model';
import {Location} from '@angular/common';
import { Route, Router } from '@angular/router';



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
  plant: Plant;
  isFileSelected: boolean;

  constructor(
    private orderService : OrderService,
    private featureService: FeatureService,
    private plantService: PlantService,
    private toastr: ToastrService,
    private imageService: ImageService,
    private location: Location,
    private router: Router
    
  ) {
    this.plant ={} as Plant;
    this.plant.features=[];
    this.orderService.getOrders().subscribe(orders => this.orders = orders);
    this.featureService.getFeatures().subscribe(features => this.features = features);
  }

  ngOnInit() {

  }
  onOrderChange() {
    var selectedOrder = this.orders.find(o => o.id == this.plant.orderId);
    this.species = selectedOrder ? selectedOrder.species : [];
  }
  onSpeciesChange(){
    if(this.plant.name==null)
      this.plant.name=this.species.find(s=>s.id==this.plant.speciesId).name;
  }

  onChangeFeature(featureId,$event){
    if($event.target.checked)
      this.plant.features.push(featureId);
    else{
      var index = this.plant.features.indexOf(featureId);
      this.plant.features.splice(index,1);
    }
  }
  onFileUploadChange(){
    this.isFileSelected=true;
  }
  onSubmit() {
      this.plantService.createPlant(this.plant)
      .subscribe(
        (data: Plant) => {
            this.plantId = data.id;
            var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
            this.imageService.uploadImage(this.plantId,nativeElement.files[0]).subscribe(data => console.log(data));
            this.toastr.success("Utworzono nową roślinę o id "+ this.plantId,"Sukces");
            this.router.navigate(["/plants"]);  
            this.location.go("/plants");
        },
        error => {
            this.toastr.error(error.message,"Błąd! "+error.name);
        });
      
    
      }
}
