import { Component, OnInit } from '@angular/core';
import { SpeciesService } from '../../services/species.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {Location} from '@angular/common';
import { OrderService } from '../../services/order.service';


@Component({
  selector: 'app-species-form',
  templateUrl: './species-form.component.html',
  styleUrls: ['./species-form.component.css']
})
export class SpeciesFormComponent implements OnInit {
  s = {};
  id:number;
  orders: any;
  isNewForm: boolean = true;
  constructor(    
    private speciesService: SpeciesService,
    private orderService : OrderService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private location: Location
  ) {
    this.route.paramMap.subscribe(params => this.id = +params.get('id'));
    if(this.id != 0)
    {
      this.speciesService.getOneSpecies(this.id).subscribe(species => this.s = species);
      this.isNewForm = false;
    }
    else{
      this.orderService.getOrders().subscribe(orders => this.orders = orders)
    }
   }

  ngOnInit() {
  }

  onSubmit(form) {
    if(this.isNewForm){ 
      this.speciesService.createSpecies(form.value)
      .subscribe(
        data => {
            console.log(data);
            this.toastr.success("Dodano gatunek","Sukces");
        },
        error => {
            console.log("Error", error);
            this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
        });
    }
    else{ 
      this.speciesService.updateSpecies(form.value, this.id)
      .subscribe(
        data => {
            console.log(data);
            this.toastr.success("Edytowano gatunek","Sukces");
        },
        error => {
            console.log("Error", error);
            this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
        }
    );  
  }  
  this.location.back();
  }

  onDelete() {
    this.speciesService.deleteSpecies(this.id)      
    .subscribe(
      data => {
          console.log(data);
          this.toastr.success("Usunięto gatunek","Sukces");
      },
      error => {
          console.log("Error", error);
          this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
      });
      this.location.back();
    }

}
