import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { HttpClientModule } from "@angular/common/http";
import { of, Observable, BehaviorSubject, observable } from "rxjs";
import { AppSettings } from "./../_constants/appsettings";
import { DeliveryOrderHeader } from "../_models/DeliveryOrderHeader";
import { Order_details } from "./../_models/order_details";
import { now } from "moment";
import { getDate } from "ngx-bootstrap/chronos/utils/date-getters";

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

    return this.http.post<Order_details[]>(
      AppSettings._BaseURL + "DeliveryOrderDetails/",
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
      AppSettings._BaseURL + "UpdateDeliveryOrderDetails/",
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
