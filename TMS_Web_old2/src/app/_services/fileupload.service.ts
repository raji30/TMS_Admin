import { Injectable } from "@angular/core";
import {
  HttpClient,
  HttpEvent,
  HttpErrorResponse,
  HttpEventType,
  HttpHeaders,
  HttpRequest, HttpResponse, HttpProgressEvent
} from "@angular/common/http";
import { throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { AppSettings } from '../_constants/appsettings';

@Injectable({
  providedIn: "root"
})
export class FileUploadService {
  apiUrl = AppSettings._BaseURL+ "FileUpload";//"http://localhost:51902/FileUpload";

  constructor(private http: HttpClient) {}

  fileupload(formData) {
    return this.http
      .post(this.apiUrl, formData, {
        headers: new HttpHeaders().set(
          "Authorization",
          "Bearer " + localStorage.getItem("token")
        ),
        reportProgress: true,
        observe: "events"
      })
      .pipe(
        map(event => this.getEventMessage(event, formData)),
        catchError(this.handleError)
      );
  }

  public upload(data) {
    return this.http
      .post<any>(this.apiUrl, data, {
        reportProgress: true,
        observe: "events"
      })
      .pipe(
        map(event => {
          switch (event.type) {
            case HttpEventType.UploadProgress:             
              const percentDone = Math.round((100 * event.loaded) / event.total);
              return { status: "progress", message: percentDone, filePath:'test' };
            case HttpEventType.Response:
              return { status: "success", message: 100, filePath:'test' }
            default:
              return `Unhandled event: ${event.type}`;
          }
        }), catchError(this.handleError)
      );
  }

  //   this.http.post('http://localhost:51902/fileupload/', frmData).subscribe(
  //   data => {
  //     // SHOW A MESSAGE RECEIVED FROM THE WEB API.
  //     this.sMsg = data as string;
  //     console.log (this.sMsg);
  //   },
  //   (err: HttpErrorResponse) => {
  //     console.log (err.message);    // Show error, if any.
  //   }
  // );

  private getEventMessage(event: HttpEvent<any>, formData) {
    switch (event.type) {
      case HttpEventType.UploadProgress:
        return this.fileUploadProgress(event);

      case HttpEventType.Response:
        return this.apiResponse(event);

      default:
        return `File "${
          formData.get("profile").name
        }" surprising upload event: ${event.type}.`;
    }
  }

  private fileUploadProgress(event: HttpProgressEvent) {
    const percentDone = Math.round((100 * event.loaded) / event.total);
    return { status: "progress", message: percentDone };
  }

  private apiResponse(event) {
    return event.body;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error("An error occurred:", error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }
    // return an observable with a user-facing error message
    return throwError("Something bad happened. Please try again later.");
  }
}
