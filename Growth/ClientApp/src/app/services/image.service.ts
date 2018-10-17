import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) { }

  uploadImage(plantId, file){
    var formData = new FormData ();
    formData.append('file',file);
    return this.http.post(`/api/plants/${plantId}/images`, formData);
  }
}
