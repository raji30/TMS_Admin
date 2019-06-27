import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}