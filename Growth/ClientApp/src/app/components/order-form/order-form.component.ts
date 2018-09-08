import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {Location} from '@angular/common';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
  o = {};
  id:number;
  isNewForm: boolean = true;

  constructor(    
    private orderService: OrderService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private location: Location
  ) {
    this.route.paramMap.subscribe(params => this.id = +params.get('id'));
    if(this.id != 0)
    {
      this.orderService.getOrder(this.id).subscribe(order => this.o = order);
      this.isNewForm = false;
      console.log(this.id);
      console.log(this.o);
    }
   }

  ngOnInit() {
  }

  onSubmit(form) {
    if(this.isNewForm){ 
      this.orderService.createOrder(form.value)
      .subscribe(
        data => {
            console.log(data);
            this.toastr.success("Dodano rząd","Sukces");
        },
        error => {
            console.log("Error", error);
            this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
        });
    }
    else{ 
      this.orderService.updateOrder(form.value, this.id)
      .subscribe(
        data => {
            console.log(data);
            this.toastr.success("Edytowano rząd","Sukces");
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
    this.orderService.deleteOrder(this.id)      
    .subscribe(
      data => {
          console.log(data);
          this.toastr.success("Usunięto rząd","Sukces");
      },
      error => {
          console.log("Error", error);
          this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
      });
      this.location.back();
    }
}
