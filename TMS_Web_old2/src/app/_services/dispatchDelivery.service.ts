import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { HttpClientModule } from "@angular/common/http";
import { of } from "rxjs";
import { AppSettings } from "./../_constants/appsettings";
import { DeliveryOrderHeader } from "../_models/DeliveryOrderHeader";
import { Order_details } from "./../_models/order_details";
import { now } from "moment";
import { Tms_routes } from "../_models/tms_routes";
import { Dispatch } from './../_models/dispatch';

@Injectable({
  providedIn: "root"
})
export class DispatchDeliveryService {
  constructor(private http: HttpClient) {}

  /**
   * name
   */
  public GetOrderstoDispatchDelivery() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Order_details[]>(
      AppSettings._BaseURL + "GetOrderstoDispatchDelivery"
    );
  }

  public UpdateDispatchDeliveryData(routedetails: Dispatch[]) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.put<Dispatch[]>(
      AppSettings._BaseURL + "UpdateDispatchDeliveryData/",
      routedetails
    );
  }

  public GetDispatchItemsList() {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.get<any[]>(
      AppSettings._BaseURL + "GetDispatchItemsList");
  }

  public GetDispatch_OrderandDetails(orderdetailkey:string) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.get<any>(
      AppSettings._BaseURL + "GetDispatch_OrderandDetails/"+orderdetailkey,httpOptions);
  }

  public GetDispatchItems(orderdetailkey:string ) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.get<Dispatch[]>(
      AppSettings._BaseURL + "GetDispatchItems/"+ orderdetailkey,httpOptions);
  }

  UpdateStatus(dispatch: Dispatch) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<Dispatch>(
      AppSettings._BaseURL + "UpdateStatus",
      dispatch,
      
    );
  }
}
