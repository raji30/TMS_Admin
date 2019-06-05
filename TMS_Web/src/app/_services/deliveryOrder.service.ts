import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { AppSettings } from './../_constants/appsettings';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Order_details } from './../_models/order_details';
import { now } from 'moment';
import { getDate } from 'ngx-bootstrap/chronos/utils/date-getters';

@Injectable({
  providedIn: 'root'
})
export class DeliveryOrderService {
 constructor(private http:HttpClient) {}
  
public saveDOHeader(OrderHeader:DeliveryOrderHeader)
{  
  // OrderHeader.OrderKey= "00000000-0000-0000-0000-000000000000";
  // OrderHeader.OrderNo ="";
  // OrderHeader.CustKey= "00000000-0000-0000-0000-000000000000";
  // OrderHeader.OrderDate= "";
  // OrderHeader.BillToAddress= "00000000-0000-0000-0000-000000000000",
  // OrderHeader.SourceAddress="00000000-0000-0000-0000-000000000000",
  // OrderHeader.DestinationAddress= "00000000-0000-0000-0000-000000000000",
  // OrderHeader.ReturnAddress= "00000000-0000-0000-0000-000000000000",
  // OrderHeader.Source="2";
  // OrderHeader.OrderType= "";
  // OrderHeader.Status= "";
  // OrderHeader.StatusDate="";
  // OrderHeader.HoldReason=0,
  // OrderHeader.HoldDate="";
  // OrderHeader.BrokerName="";
  // OrderHeader.BrokerId="";
  // OrderHeader.Brokerkey= "00000000-0000-0000-0000-000000000000";
  // OrderHeader.BrokerRefNo="";
  // OrderHeader.PortofOriginKey= "00000000-0000-0000-0000-000000000000";
  // OrderHeader.CarrierKey="00000000-0000-0000-0000-000000000000";
  // OrderHeader.VesselName="";
  // OrderHeader.BillofLading="";
  // OrderHeader.BookingNo="";
  // OrderHeader.CutOffDate="";
  // OrderHeader.Priority= 0;
  // OrderHeader.IsHazardous= true;
  // OrderHeader.CreatedBy="00000000-0000-0000-0000-000000000000";
  // OrderHeader.CreatedDate="";
  // OrderHeader.ordertypedescription="";
  // OrderHeader.statusdescription="";
  OrderHeader.orderdetails=[];
  console.log(OrderHeader);
  return this.http.post<any>( AppSettings._BaseURL + 'DeliveryOrderHeader/', OrderHeader);
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

 public updateOrderDetails(Orderdetails:Order_details)
{
  return this.http.put<Order_details>( AppSettings._BaseURL + 'UpdateDeliveryOrderDetails/',Orderdetails);
}  
}
