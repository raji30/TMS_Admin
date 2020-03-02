
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { City } from '../_models/city';

@Injectable({
  providedIn: 'root'
})
export class CityService {

constructor(private http:HttpClient) { }

public GetCity() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.get<City[]>(AppSettings._BaseURL + 'GetCity');  
}



AddCity(city: City) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<City >(
      AppSettings._BaseURL + "AddCity",
      city,
      httpOptions
    );
  }

  UpdateCity(city: City ) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<City>(
      AppSettings._BaseURL + "UpdateCity",
      city,
      httpOptions
    );
  }
 

  GetCityByID(id: string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<City>(
      AppSettings._BaseURL + "GetCityByID" + "/" + id
    );
  }
}

