
import {Observable} from 'rxjs/Rx';
import { AppSettings } from "./../_constants/appsettings";
import { DeliveryOrderHeader } from "../_models/DeliveryOrderHeader";
import { Order_details } from "./../_models/order_details";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: "root"
})
export class DeliveryOrderService {
  constructor(private http: HttpClient) {}

  public saveDOHeader(OrderHeader: DeliveryOrderHeader) {
    OrderHeader.orderdetails = []; 
    OrderHeader.CreatedBy = "";

    var token = JSON.parse(localStorage.getItem("currentUser"));
    OrderHeader.CreatedBy = token.userId;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.post<any>(
      AppSettings._BaseURL + "DeliveryOrderHeader/",
      OrderHeader
    );
  }

  public saveOrderDetails(Orderdetails: Order_details[]) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    for(var data in Orderdetails)
    {
      Orderdetails[data].CreatedBy = token.userId;
    }
    return this.http.post<Order_details[]>(
      AppSettings._BaseURL + "DeliveryOrderDetails/",
      Orderdetails
    );
  }

  public updateDOHeader(OrderHeader: DeliveryOrderHeader) {
    OrderHeader.orderdetails = [];
    OrderHeader.CreatedBy = "";

    var token = JSON.parse(localStorage.getItem("currentUser"));
    OrderHeader.CreatedBy = token.userId;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<any>(
      AppSettings._BaseURL + "UpdateOrder/",
      OrderHeader
    );
  }

  public updateDeliveryOrderDetails(Orderdetails: Order_details[]) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    for(var data in Orderdetails)
    {
      Orderdetails[data].CreatedBy = token.userId;
    }

    return this.http.put<Order_details[]>(
      AppSettings._BaseURL + "UpdateDeliveryOrderDetails/",
      Orderdetails
    );
  }

  /**
   * name
   */

  public getOrderlist(): Observable<DeliveryOrderHeader[]> {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<DeliveryOrderHeader[]>(
      AppSettings._BaseURL + "GetOrders"
    );
  }

  public GetbyKey(OrderKey: any) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<DeliveryOrderHeader>(
      AppSettings._BaseURL + "GetbyKey/" + OrderKey
    );
    //return this.http.get<DeliveryOrderHeader>( 'http://localhost:51902/GetbyKey?OrderKey='+ OrderKey);
  }
  public GetOrderDetailsbyKey(OrderKey: any) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Order_details[]>(
      AppSettings._BaseURL + "GetDeliveryOrderDetailByKey/" + OrderKey
    );
  }

  public GetOrderDetails() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Order_details[]>(
      AppSettings._BaseURL + "GetDeliveryOrderDetails"
    );
  }

  public updateOrderDetails(Orderdetails: Order_details) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.put<Order_details>(
      AppSettings._BaseURL + "UpdateScheduler/",
      Orderdetails
    );
  }
 

  public UpdateDOdetailStatus(Orderdetails: Order_details) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.put<Order_details>(
      AppSettings._BaseURL + "UpdateDOdetailStatus/",
      Orderdetails
    );
  }

  public GetOrderHeaderandDetails() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.get<DeliveryOrderHeader[]>(
      AppSettings._BaseURL + "GetAllDOHeaderandDetails"
    );
  }
}
