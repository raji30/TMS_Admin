import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { Broker } from '../_models/broker';
import { AppSettings } from '../_constants/appsettings';

@Injectable({
  providedIn: 'root'
})
export class BrokerService {  
constructor(private http:HttpClient) {}
  
public getbrokers()
{ 
   return this.http.get<Broker[]>(AppSettings._BaseURL + 'GetBrokers');
}

}
