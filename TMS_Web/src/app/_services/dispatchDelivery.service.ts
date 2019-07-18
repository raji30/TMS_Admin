import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { HttpClientModule } from "@angular/common/http";
import { of } from "rxjs";
import { AppSettings } from "./../_constants/appsettings";
import { DeliveryOrderHeader } from "../_models/DeliveryOrderHeader";
import { Order_details } from "./../_models/order_details";
import { now } from "moment";
import { getDate } from "ngx-bootstrap/chronos/utils/date-getters";
import { Tms_routes } from "../_models/tms_routes";

@Injectable({
  providedIn: "root"
})
export class DispatchDeliveryService {
  constructor(private http: HttpClient) {}

  /**
   * name
   */
  public GetOrderstoDispatchDelivery() {
    return this.http.get<Order_details[]>(
      AppSettings._BaseURL + "GetOrderstoDispatchDelivery"
    );
  }

  public UpdateDispatchDeliveryData(routedetails: Tms_routes) {
    return this.http.put<Tms_routes>(
      AppSettings._BaseURL + "UpdateDispatchDeliveryData/",
      routedetails
    );
  }
}
