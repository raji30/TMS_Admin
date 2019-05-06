
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { Address } from '../_models/address';

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private baseUrl : string
constructor(private http:HttpClient) {
 this.baseUrl= 'http://localhost:51902/';
 //http://localhost:51902/api/address/GetAllByType/1
 }
  
public getAddress(addressType:number)
{   
  return this.http.get<Address[]>(this.baseUrl + 'GetAllByType/' + addressType);
}
}
