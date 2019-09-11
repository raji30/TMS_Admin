import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeliveryOrderHeader } from '../_models/DeliveryOrderHeader';
import { Observable } from 'rxjs';
import { AppSettings } from '../_constants/appsettings';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

constructor(private http: HttpClient) { }

public GetOrderstoGenerateInvoice():Observable<DeliveryOrderHeader[]> {
  return this.http.get<DeliveryOrderHeader[]>(
    AppSettings._BaseURL + "GetOrderstoGenerateInvoice"
  );
}

}
