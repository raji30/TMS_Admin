import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { HttpClientModule } from "@angular/common/http";
import { of } from "rxjs";
import { AppSettings } from "./../_constants/appsettings";
import { DeliveryOrderHeader } from "../_models/DeliveryOrderHeader";
import { Order_details } from "./../_models/order_details";
import { now } from "moment";
import { Scheduler } from '../_models/scheduler';

@Injectable({
  providedIn: "root"
})
export class SchedulerService {
  constructor(private http: HttpClient) {}

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

    return this.http.get<Order_details[]>(
      AppSettings._BaseURL + "GetOrderstoSchedule"
    );
  }
  
  public GetOrderDetailsbykey(orderdetailkey:string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Order_details>(
      AppSettings._BaseURL + "GetOrderDetailsbykey/" +orderdetailkey
    );
  }

  public GetScheduledContainers() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Order_details[]>(
      AppSettings._BaseURL + "GetScheduledContainers"
    );
  }

  public GetScheduledContainer(orderdetailkey: any) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    // return this.http.get<any>(
    //   AppSettings._BaseURL + "GetScheduledContainer/" + orderdetailkey,httpOptions
    // );
    var url = AppSettings._BaseURL + "GetScheduledContainer/" + orderdetailkey;
    return this.http.get<any>(url);
  }
}
