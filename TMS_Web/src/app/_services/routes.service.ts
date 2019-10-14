import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Tms_routes } from '../_models/tms_routes';

@Injectable({
  providedIn: 'root'
})
export class RoutesService {

constructor(private http:HttpClient) { }

public insertRoutesDetails(routedetails:Tms_routes)
{
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

  return this.http.post<Tms_routes>( AppSettings._BaseURL + 'AddRoutes/',routedetails);
}  

public updateRoutesDetails(routedetails:Tms_routes)
{
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.put<Tms_routes>( AppSettings._BaseURL + 'UpdateRoutes/',routedetails);
}  
}
