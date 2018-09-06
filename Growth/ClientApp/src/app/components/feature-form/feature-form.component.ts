import { Feature } from '../feature.model';
import { Component, OnInit, NgModule } from '@angular/core';
import { FeatureService } from '../../services/feature.service';
import { NgForm, NgModel, FormBuilder, FormsModule } from '@angular/forms'
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {Location} from '@angular/common';

@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.css']
})
export class FeatureFormComponent implements OnInit {
  f = {};
  id:number;
  isNewForm: boolean = true;

  constructor(
    private featureService: FeatureService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private location: Location
  ) 
  {
    this.route.paramMap.subscribe(params => this.id = +params.get('id'));
    if(this.id != 0)
    {
      this.featureService.getFeature(this.id).subscribe(feature => this.f = feature);
      this.isNewForm = false;
    }
      
   }


  ngOnInit() {
  }

  onDelete() {
    this.featureService.deleteFeature(this.id)      
    .subscribe(
      data => {
          console.log(data);
          this.toastr.success("Usunięto cechę","Sukces");
      },
      error => {
          console.log("Error", error);
          this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
      });
      this.location.back();
    }
  onSubmit(form) {
    if(this.isNewForm){ 
      this.featureService.createFeature(form.value)
      .subscribe(
        data => {
            console.log(data);
            this.toastr.success("Dodano cechę","Sukces");
        },
        error => {
            console.log("Error", error);
            this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
        });
    }
    else{ 
      this.featureService.updateFeature(form.value, this.id)
      .subscribe(
        data => {
            console.log(data);
            this.toastr.success("Edytowano cechę","Sukces");
        },
        error => {
            console.log("Error", error);
            this.toastr.error("Wystąpił bład ¯\\_(ツ)_/¯","Błąd");
        }
    );  
  }  
  this.location.back();
  }
}
