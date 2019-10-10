import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Observable } from 'rxjs';
import { AppSettings } from '../_constants/appsettings';
import { Invoicemodel } from '../_models/invoicemodel';
import { Invoice } from '../_models/invoice';
import { Invoicedetails } from '../_models/invoicedetails';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

constructor(private http: HttpClient) { }

public GetOrderstoGenerateInvoice():Observable<Invoicemodel[]> {
  return this.http.get<Invoicemodel[]>(
    AppSettings._BaseURL + "GetOrderstoGenerateInvoice"
  );
}

public GetInvoiceMaxcount() {
  return this.http.get<number>(AppSettings._BaseURL + 'GetInvoiceMaxcount');  
}


public CreateInvoiceHeader(invoiceHeader: Invoice) {
  return this.http.post<any>(
    AppSettings._BaseURL + "CreateInvoiceHeader/",
    invoiceHeader
  );
}

public CreateInvoiceDetail(invoiceDetails: Invoicedetails[]) {
  return this.http.post<Invoicedetails[]>(
    AppSettings._BaseURL + "CreateInvoiceDetail/",
    invoiceDetails
  );
}

}
