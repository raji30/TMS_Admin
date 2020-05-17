
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { Address } from '../_models/address';
import { AppSettings } from './../_constants/appsettings';

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private baseUrl : string
constructor(private http:HttpClient) {
 this.baseUrl= AppSettings._BaseURL;//'http://localhost:51902/';
 //http://localhost:51902/api/address/GetAllByType/1
 }
  
public getAddress(addressType:number)
{   
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.get<Address[]>(this.baseUrl + 'GetAllByType/' + addressType);
}
}
