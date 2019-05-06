import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { AppSettings } from './../_constants/appsettings';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Order_details } from './../_models/order_details';

@Injectable({
  providedIn: 'root'
})
export class DeliveryOrderService {
  
constructor(private http:HttpClient) {}
  
public saveDOHeader(OrderHeader:DeliveryOrderHeader)
{    
  OrderHeader.Source ="2";
  return this.http.post<any>( AppSettings._BaseURL + 'DeliveryOrderHeader/',OrderHeader);
}

public saveOrderDetails(Orderdetails:Order_details[])
{
  return this.http.post<Order_details[]>( AppSettings._BaseURL + 'DeliveryOrderDetails/',Orderdetails);
}

/**
 * name
 */
public getOrderlist() {
  return this.http.get<DeliveryOrderHeader[]>(AppSettings._BaseURL + 'GetOrders');  
}

public GetbyKey(OrderKey:any) {
 return this.http.get<DeliveryOrderHeader>(AppSettings._BaseURL + 'GetbyKey/'+OrderKey);   
 //return this.http.get<DeliveryOrderHeader>( 'http://localhost:51902/GetbyKey?OrderKey='+ OrderKey); 
}
public GetOrderDetailsbyKey(OrderKey:any) {
  return this.http.get<Order_details[]>(AppSettings._BaseURL + 'GetDeliveryOrderDetail/'+OrderKey);
 }
}
