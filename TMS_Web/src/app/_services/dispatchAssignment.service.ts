import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { AppSettings } from './../_constants/appsettings';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Order_details } from './../_models/order_details';
import { now } from 'moment';
import { getDate } from 'ngx-bootstrap/chronos/utils/date-getters';
import { Tms_routes } from '../_models/tms_routes';

@Injectable({
  providedIn: 'root'
})
export class DispatchAssignmentService {
 constructor(private http:HttpClient) {}

/**
 * name
 */
public GetOrderstoDispatchAssignment() {
  return this.http.get<Order_details[]>(AppSettings._BaseURL + 'GetOrderstoDispatchAssignment');  
}

public AddDispatchAssignmentData(routedetails:Tms_routes)
{
  return this.http.post<Tms_routes>( AppSettings._BaseURL + 'AddDispatchAssignmentData/',routedetails);
} 
}
