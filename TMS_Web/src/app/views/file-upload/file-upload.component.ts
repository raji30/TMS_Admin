
import { Observable } from 'rxjs/Rx';
import { FileQueueObject,FileUploaderService } from '../../_services/file-uploader.service';
import { Output, EventEmitter, OnInit, Input, Component } from '@angular/core';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {

  @Output() onCompleteItem = new EventEmitter();
  @Input() orderno: string;
  @Input() CreatedBy: string;

  //@ViewChild('fileInput',{static: true}) fileInput;
  queue: Observable<FileQueueObject[]>;

  constructor(public uploader: FileUploaderService) { }

  ngOnInit() {
    this.queue = this.uploader.queue;
    this.uploader.onCompleteItem = this.completeItem;
  }

  completeItem = (item: FileQueueObject, response: any) => {
   
    this.onCompleteItem.emit({ item, response });
  }

  addToQueue(e) {
   // const fileBrowser = this.fileInput.nativeElement;
    this.uploader.addToQueue(e.target.files,this.orderno,this.CreatedBy);
    this.queue = this.uploader.queue;
    console.log("this.queue ", this.queue );
   
  }
  upload()
  {
    this.uploader.uploadAll(this.orderno,this.CreatedBy);   

  }
}
