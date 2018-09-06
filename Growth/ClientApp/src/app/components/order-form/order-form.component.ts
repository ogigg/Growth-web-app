import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
  f = {};
  id:number;
  isNewForm: boolean = true;

  constructor(    
    private orderService: OrderService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

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


  }
}
