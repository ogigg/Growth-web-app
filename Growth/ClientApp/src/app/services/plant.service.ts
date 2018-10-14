import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PlantService {

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'responseType' : 'xhr'
    })
  };

  getPlants() {
    return this.http.get('/api/plants');
  }
  
  createPlant(plant: string) {
    var body = JSON.stringify(plant);
    return this.http.post('/api/plants', body, this.httpOptions);
  }

  getPlant(id: number) {
    return this.http.get('/api/plants/'+id);
  }

  updatePlant(plant: string, id:number){
    return this.http.put('/api/plants/'+id, plant, this.httpOptions);
  }

  deletePlant(id: number) {
    return this.http.delete('/api/plants/'+id,this.httpOptions);
  }
}
