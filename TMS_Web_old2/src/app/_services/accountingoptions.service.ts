import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { AccountingOptions } from '../_models/accountingOptions';

@Injectable({
  providedIn: 'root'
})
export class AccountingoptionsService {

constructor(private http:HttpClient) { }

public insertAccountingoptions(options:AccountingOptions[])
{
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

 // var AccOptionsChecked = Array<AccountingOptions>();
  for (let option of options) {
   option.createuserkey = token.userId;  
 }

  return this.http.post<AccountingOptions[]>( AppSettings._BaseURL + 'AddAccountingOptions/',options);
}  

public GetAccountingOptionsbyKey(orderdetailKey:string) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  return this.http.get<AccountingOptions[]>(
    AppSettings._BaseURL + "GetAccountingOptionsbyKey/"+ orderdetailKey
  );
}

public UpdateAccountingOptions(orderdetailKey:string) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  return this.http.put<any>(
    AppSettings._BaseURL + "UpdateAccountingOptions/"+ orderdetailKey,httpOptions
  );
}
}
