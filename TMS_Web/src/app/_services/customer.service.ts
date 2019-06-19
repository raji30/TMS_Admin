import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Customer } from '../_models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

constructor(private http:HttpClient) { }

public getCustomers() {
  return this.http.get<Customer[]>(AppSettings._BaseURL + 'GetCustomers');  
}
}