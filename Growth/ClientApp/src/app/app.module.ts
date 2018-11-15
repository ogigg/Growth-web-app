import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { AlertComponent } from './components/alert/alert.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PlantFormComponent } from './components/plant-form/plant-form.component';
import { OrderService } from './services/order.service';
import { FeatureService } from './services/feature.service';
import { FeatureFormComponent } from './components/feature-form/feature-form.component';
import { OrderFormComponent } from './components/order-form/order-form.component';
import { SpeciesFormComponent } from './components/species-form/species-form.component';
import { PlantListComponent } from './components/plant-list/plant-list.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { PlantDetailsComponent } from './components/plant-details/plant-details.component';
import { TypeaheadModule } from 'ngx-type-ahead';
import { NgSelectModule } from '@ng-select/ng-select';
import { AuthGuard } from './services/auth.guard';
import { AlertService } from './services/alert.service';
import { AuthenticationService } from './services/authentication.service';
import { JwtInterceptor } from './services/jwt.interceptor';
import { ErrorInterceptor } from './services/error.interceptor';
import { UserService } from './services/user.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PlantFormComponent,
    FeatureFormComponent,
    OrderFormComponent,
    SpeciesFormComponent,
    PlantListComponent,
    PaginationComponent,
    PlantDetailsComponent,
    AlertComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgSelectModule,
    TypeaheadModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'plant/new', component: PlantFormComponent },
      { path: 'plants', component: PlantListComponent },
      { path: 'orders', component: OrderFormComponent },
      { path: 'orders/:id', component: OrderFormComponent },
      { path: 'species', component: SpeciesFormComponent },
      { path: 'species/:id', component: SpeciesFormComponent },
      { path: 'features', component: FeatureFormComponent },
      { path: 'features/:id', component: FeatureFormComponent },
      { path: 'plant/:id', component: PlantDetailsComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },  
      { path: '**', component: HomeComponent }
    ])
  ],
  providers: [
    OrderService,
    FeatureService,
    AuthGuard,
    UserService,
    AlertService,
    AuthenticationService,
    //{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
