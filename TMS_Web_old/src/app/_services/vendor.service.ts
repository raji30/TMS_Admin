import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Driver } from '../_models/driver';
import { Vendor } from '../_models/vendor';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

constructor(private http:HttpClient) { }

public getVendors() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Vendor[]>(AppSettings._BaseURL + 'GetVendors');  
}

  createVendor(vendor: Vendor) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Vendor>(
      AppSettings._BaseURL + "CreateVendor",
      vendor,
      httpOptions
    );
  }

  updateVendor(vendor: Vendor) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<Vendor>(
      AppSettings._BaseURL + "UpdateVendor",
      vendor,
      httpOptions
    );
  }
  GetVendorByID(id: string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.get<Vendor>(
      AppSettings._BaseURL + "GetVendorByID" + "/" + id
    );
  }
}
