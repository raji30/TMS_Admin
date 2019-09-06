import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Containersize, Priority, OrderType ,Source, HoldReason, Status, Carrier, LoadDischargePort} from '../common/master';

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
  return this.http.get<Status[]>(AppSettings._BaseURL + 'GetAllDOStatus');  
}
public getHoldReasonList() {
  return this.http.get<HoldReason[]>(AppSettings._BaseURL + 'GetHoldReason');  
}
public getSourceList() {
  return this.http.get<Source[]>(AppSettings._BaseURL + 'GetSource');  
}
public getLoadDischargePortList(addressType:number) {
  return this.http.get<LoadDischargePort[]>(AppSettings._BaseURL + 'GetAllByType/' + addressType);  
}
public getCarrierList() {
  return this.http.get<Carrier[]>(AppSettings._BaseURL + 'GetCarrier');  
}

public getMaxcount_Customer(custname:any) {
  return this.http.get<number>(AppSettings._BaseURL + 'GetCustomerMaxcount/'+ custname);  
}


}
