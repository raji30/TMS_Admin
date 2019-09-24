import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Observable } from 'rxjs';
import { AppSettings } from '../_constants/appsettings';
import { Invoicemodel } from '../_models/invoicemodel';

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

}
