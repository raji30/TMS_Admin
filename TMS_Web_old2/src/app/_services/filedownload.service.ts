import { Injectable } from "@angular/core";
import "rxjs/Rx";
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders,HttpResponse } from "@angular/common/http";
import { DomSanitizer } from "@angular/platform-browser";
import { tap } from 'rxjs/operators';
import { AppSettings } from '../_constants/appsettings';

@Injectable({
  providedIn: "root"
})
export class FiledownloadService {
  public url: string =  AppSettings._BaseURL + "GetTestFile/?fileName=";
  constructor(private http: HttpClient) {}

  DownloadFile(orderno:string,    filePath: string,    fileType: string //: Observable<any>
  ) {
    let fileExtension = fileType;
    let filename = filePath;
    var token = JSON.parse(localStorage.getItem("currentUser"));

    // For pass blob in API 
    var filePath = this.url + filename +"&orderno="+orderno;

 return this.http.get(filePath, { headers: new HttpHeaders({
  'Authorization': "bearer " + token.token,
  'Content-Type': 'application/json',}), responseType: 'blob'}).pipe (
tap (
    // Log the result or error
    data => console.log('You received data'),
    error => console.log(error)
 )
);
  }

 
}
