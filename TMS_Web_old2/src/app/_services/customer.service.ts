import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AppSettings } from "../_constants/appsettings";
import { Customer } from "../_models/customer";

@Injectable({
  providedIn: "root"
})
export class CustomerService {
  headers: any;
  constructor(private http: HttpClient) {}

  public getCustomers() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Customer[]>(AppSettings._BaseURL + "GetCustomers");
  }

  createCustomer(customer: Customer) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Customer>(
      AppSettings._BaseURL + "CreateCustomer",
      customer,
      httpOptions
    );
  }

  updateCustomer(customer: Customer) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<any>(
      AppSettings._BaseURL + "UpdateCustomer",
      customer      
    );
  }
  getCustomerById(id: string) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.get<Customer>(
      AppSettings._BaseURL + "GetCustomerByID" + "/" + id
    );
  }
}
