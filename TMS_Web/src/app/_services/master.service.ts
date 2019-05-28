import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Containersize, Priority, OrderType } from '../common/master';
@Injectable({
  providedIn: 'root'
})
export class MasterService {

constructor(private http:HttpClient) { }

public getContainerSizeList() {
  return this.http.get<Containersize[]>(AppSettings._BaseURL + 'GetContainerSizes');  
}
public getPriorityList() {
  return this.http.get<Priority[]>(AppSettings._BaseURL + 'GetallPriority');  
}
public getOrderTypeList() {
  return this.http.get<OrderType[]>(AppSettings._BaseURL + 'GetOrderType');  
}
public getStatusList() {
  return this.http.get<OrderType[]>(AppSettings._BaseURL + 'GetAllDOStatus');  
}
public getHoldReasonList() {
  return this.http.get<OrderType[]>(AppSettings._BaseURL + 'GetHoldReason');  
}

}
