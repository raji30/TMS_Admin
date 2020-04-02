import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { User } from '../_models/User';
import { AppSettings } from '../_constants/appsettings';
import { UserPermissions } from '../_models/UserPermissions';

@Injectable({ providedIn: 'root' })
export class UserpermissionService {
    private baseUrl : string
    
    constructor(private http:HttpClient) { 
        this.baseUrl= 'http://localhost:51902/Token/';
    }  
     
    getpermissionsByuserkey(userkey: string) {   
      var token = JSON.parse(localStorage.getItem("currentUser"));

      const httpOptions = {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: "Bearer " + token.token
        })
      };       
      return this.http.get<UserPermissions[]>(AppSettings._BaseURL + "getUserPermissionsByUserkey"+ "/" + userkey);  
  } 

  getMenus() {   
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };       
    return this.http.get<UserPermissions[]>(AppSettings._BaseURL + "getMenus");  
} 


public AddUserPermissions(UserPermission: UserPermissions[]) {

  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

 
  return this.http.post<UserPermissions[]>(
    AppSettings._BaseURL + "AddUserPermissions/",
    UserPermission
  );
}


public UpdateUserPermissions(UserPermission: UserPermissions[]) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

 
  return this.http.put<UserPermissions[]>(
    AppSettings._BaseURL + "UpdateUserPermissions",
    UserPermission
  );
}


}

