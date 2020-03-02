import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Observable } from 'rxjs';
import { AppSettings } from '../_constants/appsettings';
import { Invoicemodel } from '../_models/invoicemodel';
import { Invoice } from '../_models/invoice';
import { Invoicedetails } from '../_models/invoicedetails';
import { Rate } from '../_models/rate';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  public token:any;
   
constructor(private http: HttpClient) { }

public _httpOptions()
{
  this.token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " +  this.token.token
    })
  };
}

public GetOrderstoGenerateInvoice():Observable<Invoicemodel[]> {
  return this.http.get<Invoicemodel[]>(
    AppSettings._BaseURL + "GetOrderstoGenerateInvoice"
  );
}


public getOrderDatabyKey( orderkey:string):Observable<any> {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<any>(
    AppSettings._BaseURL + "getOrderDatabyKey/"+ orderkey ,httpOptions
  ); 
}


public getinvoicedetail( invoicekey:string):Observable<Invoicedetails[]> {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Invoicedetails[]>(
    AppSettings._BaseURL + "getinvoicedetail/"+ invoicekey ,httpOptions
  ); 
}


public getorderratesbykey( orderkey:string): Observable<Rate[]>{
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Rate[]>(
    AppSettings._BaseURL + "getorderratesbykey/"+ orderkey ,httpOptions
  ); 
}

public getInvoiceHeaderList(): Observable<Invoice[]>{
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.get<Invoice[]>(
    AppSettings._BaseURL + "getInvoiceHeaderList" ,httpOptions
  ); 
}

public GetInvoiceMaxcount() {
  return this.http.get<number>(AppSettings._BaseURL + 'GetInvoiceMaxcount');  
}


public CreateInvoiceHeader(invoiceHeader: Invoice) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  return this.http.post<any>(
    AppSettings._BaseURL + "CreateInvoiceHeader/",
    invoiceHeader
  );
}

public UpdateInvoiceHeader(invoiceHeader: Invoice) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  return this.http.put<any>(AppSettings._BaseURL + "UpdateInvoiceHeader/",invoiceHeader,httpOptions);
}

public CreateInvoiceDetail(invoiceDetails: Invoicedetails[]) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.post<Invoicedetails[]>(
    AppSettings._BaseURL + "CreateInvoiceDetail/",
    invoiceDetails
  );
}


public UpdateInvoiceDetail(invoiceDetails: Invoicedetails[]) {

  var token = JSON.parse(localStorage.getItem("currentUser"));
  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };  
  return this.http.put<Invoicedetails[]>(AppSettings._BaseURL + "UpdateInvoiceDetail/",invoiceDetails);
}
}
