import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { User } from '../_models/User';
import { AppSettings } from '../_constants/appsettings';

@Injectable({ providedIn: 'root' })
export class UserService {
    //private baseUrl : string
    
    constructor(private http:HttpClient) { 
        //this.baseUrl= 'http://localhost:51902/Token/';
    }
    getAll() {   
        var token = JSON.parse(localStorage.getItem("currentUser"));

        const httpOptions = {
          headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: "Bearer " + token.token
          })
        }; 
        
        return this.http.get<User[]>(AppSettings._BaseURL + 'GetAllUser');  
    }
    getById(username: string,passord:string,company:string) {

        const params = new HttpParams()
        .set('username', username)
        .set('password', passord)
        .set('companyName', company);          

       // return this.http.get(this.baseUrl ,{params});
    }    
    getUserById(userkey: string) {   
      var token = JSON.parse(localStorage.getItem("currentUser"));

      const httpOptions = {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: "Bearer " + token.token
        })
      };       
      return this.http.get<User>(AppSettings._BaseURL + "getUserById"+ "/" + userkey);  
  }

  CreateUser(user: User) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<User>(
      AppSettings._BaseURL + "CreateUser",
      user,
      httpOptions
    );
  }

  UpdateUser(user: User) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<User>(
      AppSettings._BaseURL + "UpdateUser",
      user
    );
  }
  
}

