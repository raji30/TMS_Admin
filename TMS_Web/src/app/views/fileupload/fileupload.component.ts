import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { FileUploadService } from "../../_services/fileupload.service";
import {
  HttpClient,
  HttpErrorResponse,
  HttpEventType,
  HttpHeaders,
  HttpResponse
} from "@angular/common/http";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-fileupload",
  templateUrl: "./fileupload.component.html",
  styleUrls: ["./fileupload.component.scss"]
})
export class FileuploadComponent implements OnInit {
  myFiles: string[] = [];
  error: string;
  fileUpload = { status: "", message: "", filePath: "" };
  fileUploadcount: number;

  constructor(
    private toastr: ToastrService,
    private fileUploadService: FileUploadService,
    private http: HttpClient
  ) {}

  ngOnInit() {}

  onSelectedFile(e) {
    this.myFiles = [];
    this.fileUploadcount = 0;
    for (var i = 0; i < e.target.files.length; i++) {
      this.myFiles.push(e.target.files[i]);
    }
  }

  // onSubmit() {
  //   const frmData = new FormData();
  //   for (var i = 0; i < this.myFiles.length; i++) {
  //     frmData.append("fileUpload", this.myFiles[i]);

  //     this.http.post('http://localhost:51902/FileUpload', frmData, {reportProgress: true, observe: 'events'})
  //     .subscribe(event => {
  //       if (event.type === HttpEventType.UploadProgress) {
  //         this.percentDone = Math.round(100 * event.loaded / event.total);
  //       } else if (event instanceof HttpResponse) {
  //         this.uploadSuccess = true;
  //       }
  //   });
  //   }
  // }

  onSubmit() {
    if (this.myFiles.length === 0) {
      return this.showWarning("No File(s) selected", "Upload");
    }
    for (var i = 0; i < this.myFiles.length; i++) {
      const frmData = new FormData();
      frmData.append("fileUpload", this.myFiles[i]);
      this.fileUploadService.upload(frmData).subscribe(
        res => {
          (
            this.fileUpload.status = res.toString())
            this.showSuccess("File(s) uploaded successfully", "Upload");
           (this.fileUploadcount = this.fileUploadcount + 1);
          this.myFiles = [];   
        },
        err => (this.error = err)
      );
    }
  }
  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 4000, closeButton: true });
  }

  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 4000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }

  // onSubmit() {
  //   const frmData = new FormData();
  //   for (var i = 0; i < this.myFiles.length; i++) {
  //     frmData.append("fileUpload", this.myFiles[i]);
  //     this.http
  //       .post("http://localhost:51902/FileUpload", frmData)
  //       .subscribe(event => {

  //           console.log(event);

  //       });
  //   }
  // }
}
