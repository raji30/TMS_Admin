import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { Containersize } from '../../../common/master';
import { MasterService } from '../../../_services/master.service';

@Component({
  selector: 'app-containersize',
  templateUrl: './containersize.component.html',
  styleUrls: ['./containersize.component.scss']
})
export class ContainersizeComponent implements OnInit {
  
  drpContainerSize: Containersize[];
  selectedContainersize: Containersize = new Containersize();

@Input() bindContainersize: number;
@Output() ContainersizeSelectedOutput = new EventEmitter<string>();

constructor(private service: MasterService) {}

ngOnInit() {  
  this.service
    .getContainerSizeList()
    .subscribe(
      data => (this.drpContainerSize = data),
      error => console.log(error),
      () => console.log("Get ContainerSize complete")
    );
}

ngOnChanges() {
  this.selectedContainersize.containersize= this.bindContainersize;
}

onSelect(Selected: Containersize): void {
  this.ContainersizeSelectedOutput.emit(Selected.description);
  this.selectedContainersize = Selected;  
}


}



