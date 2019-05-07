import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { Broker } from '../_models/broker';

@Injectable({
  providedIn: 'root'
})
export class BrokerService {
  private baseUrl : string;
  broker = [{ "BrokerName": "Maersk Line",
  "BrokerId": "ML0023",
  "BrokerKey":"ae24e3ba-5aad-11e9-94fc-332aa5298740",
  "Status":"1"}];
constructor(private http:HttpClient) {
 this.baseUrl= 'http://localhost:51902/';
 }
  
public getAddress(brokerKey:string)
{   
  return this.broker;
  //  this.http.get<Broker[]>(this.baseUrl + 'GetAllByType/' + addressType);
}
}
