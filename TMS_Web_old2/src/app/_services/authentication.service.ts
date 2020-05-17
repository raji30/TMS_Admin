import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AppSettings } from '../_constants/appsettings';
import { Loginresult } from '../_models/loginresult';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {   
    private currentUserSubject: BehaviorSubject<Loginresult>;
    public currentUser: Observable<Loginresult>;
    baseUrl:string; 

    constructor(private http: HttpClient) {
        this.baseUrl=AppSettings._BaseURL;
        this.currentUserSubject = new BehaviorSubject<Loginresult>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
     }

    login(username: string, password: string,company:string) {
        return this.http.post<any>(this.baseUrl+'Token' , { username:username, password:password,company:company })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }                
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}