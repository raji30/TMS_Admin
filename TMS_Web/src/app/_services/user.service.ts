import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { of } from 'rxjs';
import { User } from '../_models/User';

@Injectable({ providedIn: 'root' })
export class UserService {
    private baseUrl : string
    
    constructor(private http:HttpClient) { 
        this.baseUrl= 'http://localhost:51902/Token/';
    }
    getAll() {   
        var token = JSON.parse(localStorage.getItem("currentUser"));

        const httpOptions = {
          headers: new HttpHeaders({
            "Content-Type": "application/json",
            Authorization: "Bearer " + token.token
          })
        };
 
        return this.http.get<User[]>(this.baseUrl);
    }
    getById(username: string,passord:string,company:string) {

        const params = new HttpParams()
        .set('username', username)
        .set('password', passord)
        .set('companyName', company);          

       // return this.http.get(this.baseUrl ,{params});
    }
}

