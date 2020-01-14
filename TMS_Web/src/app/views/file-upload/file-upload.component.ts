import { Observable } from "rxjs/Rx";
import {
  FileQueueObject,
  FileUploaderService
} from "../../_services/file-uploader.service";
import { Output, EventEmitter, OnInit, Input, Component } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { FiledownloadService } from "../../_services/filedownload.service";
import { saveAs } from "file-saver";
@Component({
  selector: "app-file-upload",
  templateUrl: "./file-upload.component.html",
  styleUrls: ["./file-upload.component.scss"]
})
export class FileUploadComponent implements OnInit {
  @Output() onCompleteItem = new EventEmitter();
  @Input() orderno: string;
  @Input() CreatedBy: string;

  fileUploadcount: number = 0;
  viewPDF: boolean = false;
  display = "none";
  //@ViewChild('fileInput',{static: true}) fileInput;
  @Input() queue: Observable<FileQueueObject[]>;

  constructor(
    public uploader: FileUploaderService,
    private toastr: ToastrService,
    private filedownloadService: FiledownloadService
  ) {}

  ngOnInit() {
    this.uploader.clearQueue();
    this.queue = this.uploader.queue;
    this.uploader.onCompleteItem = this.completeItem;
    console.log("this.queue ", this.queue);
  }

  ngOnChanges() {
    this.uploader.clearQueue();
    this.uploader.addToQueue2(this.queue, this.orderno, this.CreatedBy);
    this.queue = this.uploader.queue;
    console.log("this.queue ", this.queue);
  }

  completeItem = (item: FileQueueObject, response: any) => {
    this.onCompleteItem.emit({ item, response });
  };

  addToQueue(e) {
    this.fileUploadcount = e.target.files.length;

    // const fileBrowser = this.fileInput.nativeElement;
    this.uploader.addToQueue(e.target.files, this.orderno, this.CreatedBy);
    console.log("this.queue ", this.queue);
  }
  upload() {
    if (this.fileUploadcount === 0) {
      return this.showWarning("No File(s) selected", "Upload");
    }
    this.uploader.uploadAll(this.orderno, this.CreatedBy);
    this.fileUploadcount = 0;
  }
  viewFile() {
    this.viewPDF = true;
    this.display = "block";
  }
  clear() {
    this.fileUploadcount = 0;
    this.uploader.clearQueue();
    this.fileUploadcount = 0;
  }
  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }
 
  closeModalDialog() {
    this.display = "none"; //set none css after close dialog
    this.viewPDF = false;
  }
  DownLoadFiles(attachmentFileName: string) {
    let fileName = attachmentFileName;
    //file type extension
    let checkFileType = fileName.split(".").pop();
    var fileType;
    if (checkFileType === "txt") {
      fileType = "text/plain";
    }
    if (checkFileType === "pdf") {
      fileType = "application/pdf";
    }
    if (checkFileType === "doc") {
      fileType = "application/vnd.ms-word";
    }
    if (checkFileType === "docx") {
      fileType = "application/vnd.ms-word";
    }
    if (checkFileType === "xls") {
      fileType = "application/vnd.ms-excel";
    }
    if (checkFileType === "png") {
      fileType = "image/png";
    }
    if (checkFileType === "jpg") {
      fileType = "image/jpeg";
    }
    if (checkFileType === "jpeg") {
      fileType = "image/jpeg";
    }
    if (checkFileType === "gif") {
      fileType = "image/gif";
    }
    if (checkFileType === "csv") {
      fileType = "text/csv";
    }

    this.filedownloadService
      .DownloadFile(this.orderno, fileName, fileType)
      .subscribe(
        success => {
          if (confirm("Are you sure to download the file? ")) {
            saveAs(success, fileName);
          }
        },
        err => {
          console.log("Error Downloads", err);
          alert("Server error while downloading file.");
        }
      );
  }
}
