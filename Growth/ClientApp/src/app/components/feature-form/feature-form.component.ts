import { Feature } from '../feature.model';
import { Component, OnInit } from '@angular/core';
import { FeatureService } from '../../services/feature.service';
import { NgForm } from '@angular/forms'


@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.css']
})
export class FeatureFormComponent implements OnInit {

  constructor(private featureService: FeatureService) { }

  ngOnInit() {

  }

  onSubmit(form :NgForm) {
    console.log(form.value);
    this.featureService.createFeature(form.value)
    .subscribe(response => console.log(response));
  }
}
