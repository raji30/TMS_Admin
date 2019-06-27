import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Tms_routes } from '../_models/tms_routes';

@Injectable({
  providedIn: 'root'
})
export class RoutesService {

constructor(private http:HttpClient) { }

public insertRoutesDetails(routedetails:Tms_routes)
{
  return this.http.post<Tms_routes>( AppSettings._BaseURL + 'AddRoutes/',routedetails);
}  
}
