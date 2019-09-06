import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Driver } from '../_models/driver';

@Injectable({
  providedIn: 'root'
})
export class DriverService {

constructor(private http:HttpClient) { }

public getDrivers() {
  return this.http.get<Driver[]>(AppSettings._BaseURL + 'GetDrivers');  
}

  createDriver(driver: Driver) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Driver>(
      AppSettings._BaseURL + "CreateDriver",
      driver,
      httpOptions
    );
  }

  updateDriver(driver: Driver) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<Driver>(
      AppSettings._BaseURL + "UpdateDriver",
      driver,
      httpOptions
    );
  }
  getDriverById(id: string) {
    return this.http.get<Driver>(
      AppSettings._BaseURL + "GetDriverByID" + "/" + id
    );
  }
}
