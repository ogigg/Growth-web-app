import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse } from '@angular/common/http';
import { Feature } from '../components/feature.model';
import { HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class FeatureService {

  constructor(private http: HttpClient) {  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };
  getFeatures() {
    return this.http.get('/api/features');
  }

  createFeature(feature: Feature) {
    var body = JSON.stringify(feature);
    return this.http.post('/api/features', body,this.httpOptions);
  }
}
