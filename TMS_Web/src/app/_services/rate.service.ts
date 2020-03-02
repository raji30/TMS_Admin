import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AppSettings } from "../_constants/appsettings";
import { Item } from "../_models/item";
import { Ratesheet } from "./../_models/ratesheet";
import { Baserate } from "./../_models/baserate";

@Injectable({
  providedIn: "root"
})
export class RateService {
  constructor(private http: HttpClient) {}

  //Item rate
  public GetRates() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Ratesheet[]>(AppSettings._BaseURL + "GetRates");
  }

  AddRate(rate: Ratesheet) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    rate.userkey = token.userId;
    rate.ratekey = null;
    rate.createdate = null;
    rate.lastupdatedate = null;

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.post<Ratesheet>(
      AppSettings._BaseURL + "AddRate",
      rate      
    );
  }

  UpdateRate(rate: Ratesheet[]) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    for (var item of rate) {
      item.userkey = token.userId;
    }
    return this.http.put<Ratesheet[]>(
      AppSettings._BaseURL + "UpdateRate",
      rate      
    );
  }

  GetRateByCustomer(customerkey: string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    var url = AppSettings._BaseURL + "GetRateByCustomer/" + customerkey;
    return this.http.get<Ratesheet[]>(url);
  }

  //BASE RATE
  public GetBaseRates() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Baserate[]>(AppSettings._BaseURL + "GetBaseRates");
  }

  AddBaseRate(rate: Baserate[]) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    for (var data in rate) {
      rate[data].userkey = token.userId;
    }

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.post<Baserate[]>(
      AppSettings._BaseURL + "AddBaseRate",
      rate      
    );
  }

  UpdateBaseRate(rate: Baserate[]) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    for (var item of rate) {
      item.userkey = token.userId;
    }
    return this.http.put<Baserate[]>(
      AppSettings._BaseURL + "UpdateBaseRate",
      rate      
    );
  }

  GetBaseRateByCustomer(customerkey: string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    var url = AppSettings._BaseURL + "GetBaseRateByCustomer/" + customerkey;
    return this.http.get<Baserate[]>(url);
  }
}
