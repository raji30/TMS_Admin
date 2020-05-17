import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Broker } from '../_models/broker';

@Injectable({
  providedIn: 'root'
})
export class BrokerService {

constructor(private http:HttpClient) { }

public GetBrokers() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  return this.http.get<Broker[]>(AppSettings._BaseURL + 'GetBrokers');  
}

CreateBroker(broker: Broker) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Broker>(
      AppSettings._BaseURL + "CreateBroker",
      broker,
      httpOptions
    );
  }

  UpdateBroker(broker: Broker) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<Broker>(
      AppSettings._BaseURL + "UpdateBroker",
      broker);
  }
  GetBrokerByID(id: string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Broker>(
      AppSettings._BaseURL + "GetBrokerByID" + "/" + id
    );
  }
}

