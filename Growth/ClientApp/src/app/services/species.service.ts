import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SpeciesService {
  
  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'responseType' : 'xhr'
    })
  };
  
  getSpecies() {
    return this.http.get('/api/species');
  }

  getOneSpecies(id: number) {
    return this.http.get('/api/species/'+id);
  }
  
  createSpecies(order: any) {
    var body = JSON.stringify(order);
    console.log(body);
    return this.http.post('/api/species', body, this.httpOptions);
  }

  updateSpecies(order: string, id:number){
    return this.http.put('/api/species/'+id, order, this.httpOptions);
  }
  
  deleteSpecies(id: number) {
    return this.http.delete('/api/species/'+id,this.httpOptions);
  }


}
