import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AppSettings } from '../_constants/appsettings';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {   
    baseUrl:string; 
    constructor(private http: HttpClient) {
        this.baseUrl=AppSettings._BaseURL;
     }

    login(username: string, password: string,company:string) {
        return this.http.post<any>(this.baseUrl+'Token' , { username:username, password:password,company:company })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }                
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}