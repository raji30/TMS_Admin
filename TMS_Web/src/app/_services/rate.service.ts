import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AppSettings } from "../_constants/appsettings";
import { Item } from "../_models/item";
import { Ratesheet } from "./../_models/ratesheet";

@Injectable({
  providedIn: "root"
})
export class RateService {
  constructor(private http: HttpClient) {}

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
      rate,
      httpOptions
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
      rate,
      httpOptions
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
var url =  AppSettings._BaseURL + "GetRateByCustomer/" + customerkey;
    return this.http.get<Ratesheet[]>(
      url,
      httpOptions
    );
  }
}
