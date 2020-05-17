import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { User } from '../_models/User';
import { AppSettings } from '../_constants/appsettings';
import { UserPermissions } from '../_models/UserPermissions';
import { UserRole } from '../_models/UserRole';

@Injectable({ providedIn: 'root' })
export class UserpermissionService {
    //private baseUrl : string
    
    constructor(private http:HttpClient) { 
        //this.baseUrl= 'http://localhost:51902/Token/';
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


getRoles() {   
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };       
  return this.http.get<UserRole[]>(AppSettings._BaseURL + "getRoles");  
} 


getUserRoleByRolekey(rolekey: string) {   
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };       
  return this.http.get<UserRole>(AppSettings._BaseURL + "getUserRoleByRolekey"+ "/" + rolekey);  
} 

getUserRoleByUserkey(userkey: string) {   
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };       
  return this.http.get<UserRole>(AppSettings._BaseURL + "getUserRoleByUserkey"+ "/" + userkey);  
} 
public AddUserRole(userRole: UserRole) {

  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

 
  return this.http.post<UserRole>(
    AppSettings._BaseURL + "AddUserRole/",
    userRole
  );
}

public UpdateUserRole(userRole: UserRole) {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };

 
  return this.http.put<UserRole>(
    AppSettings._BaseURL + "UpdateUserRole",
    userRole
  );
}

}

