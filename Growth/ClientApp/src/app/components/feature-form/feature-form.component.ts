import { Feature } from '../feature.model';
import { Component, OnInit, NgModule } from '@angular/core';
import { FeatureService } from '../../services/feature.service';
import { NgForm, NgModel, FormBuilder, FormsModule } from '@angular/forms'
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.css']
})
export class FeatureFormComponent implements OnInit {
  f = {};
  id:number;
  constructor(
    private featureService: FeatureService,
    private route: ActivatedRoute    
  ) 
  {
    this.route.paramMap.subscribe(params => this.id = +params.get('id'));
    if(this.id != 0)
      this.featureService.getFeature(this.id).subscribe(feature => this.f = feature);
   }


  ngOnInit() {
    console.log(this.id);
  }

  onSubmit(form) {
    console.log(this.f.name );
    console.log(this.id);
    if(this.id === 0){ //empty form
      this.featureService.createFeature(form.value)
      .subscribe(response => console.log(response));
    }
    else{ //edit form
      this.featureService.updateFeature(form.value, this.id)
      .subscribe(response => console.log(response));
  }  
  }
}
