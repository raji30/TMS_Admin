import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { AppSettings } from './../_constants/appsettings';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Order_details } from './../_models/order_details';
import { now } from 'moment';

@Injectable({
  providedIn: 'root'
})
export class SchedulerService {
 constructor(private http:HttpClient) {}

/**
 * name
 */
public GetOrderstoSchedule() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.get<Order_details[]>(AppSettings._BaseURL + 'GetOrderstoSchedule');  
}
}
