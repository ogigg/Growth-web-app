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
}
