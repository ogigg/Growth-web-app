import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PlantFormComponent } from './components/plant-form/plant-form.component';
import { OrderService } from './services/order.service';
import { FeatureService } from './services/feature.service';
import { FeatureFormComponent } from './components/feature-form/feature-form.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PlantFormComponent,
    FeatureFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'plant/new', component: PlantFormComponent },
      { path: 'features', component: FeatureFormComponent },
      { path: 'features/:id', component: FeatureFormComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: '**', component: HomeComponent }
    ])
  ],
  providers: [OrderService,FeatureService],
  bootstrap: [AppComponent]
})
export class AppModule { }
