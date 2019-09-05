
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Carrier } from '../common/master';

@Injectable({
  providedIn: 'root'
})
export class CarrierService {

constructor(private http:HttpClient) { }

public GetCarriers() {
  return this.http.get<Carrier[]>(AppSettings._BaseURL + 'GetCarrier');  
}

  CreateCarrier(carrier: Carrier) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Carrier >(
      AppSettings._BaseURL + "CreateCarrier",
      carrier,
      httpOptions
    );
  }

  UpdateCarrier(carrier: Carrier ) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<Carrier>(
      AppSettings._BaseURL + "UpdateCarrier",
      carrier,
      httpOptions
    );
  }
  GetCarrierByID(id: string) {
    return this.http.get<Carrier>(
      AppSettings._BaseURL + "GetCarrierByID" + "/" + id
    );
  }
}
