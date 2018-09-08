import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()

export class OrderService {

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'responseType' : 'xhr'
    })
  };

  getOrders() {
    return this.http.get('/api/orders');
  }
  
  createOrder(order: string) {
    var body = JSON.stringify(order);
    return this.http.post('/api/orders', body, this.httpOptions);
  }

  getOrder(id: number) {
    return this.http.get('/api/orders/'+id);
  }

  updateOrder(order: string, id:number){
    return this.http.put('/api/orders/'+id, order, this.httpOptions);
  }

  deleteOrder(id: number) {
    return this.http.delete('/api/orders/'+id,this.httpOptions);
  }
}
