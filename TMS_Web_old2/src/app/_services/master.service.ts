import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Containersize, Priority, OrderType ,Source, HoldReason, Status, Carrier, LoadDischargePort} from '../common/master';
import { Item } from '../_models/item';

@Injectable({
  providedIn: 'root'
})
export class MasterService {

constructor(private http:HttpClient) { }

public getContainerSizeList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Containersize[]>(AppSettings._BaseURL + 'GetContainerSizes');  
}
public getPriorityList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Priority[]>(AppSettings._BaseURL + 'GetallPriority');  
}
public getOrderTypeList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<OrderType[]>(AppSettings._BaseURL + 'GetOrderType');  
}
public getStatusList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Status[]>(AppSettings._BaseURL + 'GetAllDOStatus');  
}
public getHoldReasonList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<HoldReason[]>(AppSettings._BaseURL + 'GetHoldReason');  
}
public getSourceList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Source[]>(AppSettings._BaseURL + 'GetSource');  
}
public getLoadDischargePortList(addressType:number) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<LoadDischargePort[]>(AppSettings._BaseURL + 'GetAllByType/' + addressType);  
}
public getCarrierList() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.get<Carrier[]>(AppSettings._BaseURL + 'GetCarrier');  
}

public getMaxcount_Customer(custname:any) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<number>(AppSettings._BaseURL + 'GetCustomerMaxcount/'+ custname);  
}
public getItemList(itemType:number) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  return this.http.get<Item[]>(AppSettings._BaseURL + 'GetItemsbyType/'+ itemType);  
}

}
