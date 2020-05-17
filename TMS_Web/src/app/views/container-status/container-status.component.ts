import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SchedulerlistComponent } from '../schedulerlist/schedulerlist.component';
@Component({
  selector: 'app-container-status',
  templateUrl: './container-status.component.html',
  styleUrls: ['./container-status.component.scss']
})
export class ContainerStatusComponent implements OnInit {
 
  constructor(private _NgbModal: NgbModal) {      
   }  

  ngOnInit() {
  }
   // open modal
   openModal() {
    this._NgbModal.open(SchedulerlistComponent, {backdrop: 'static',size: 'lg', keyboard: true, centered: true,
      windowClass: 'modal-job-scrollable'
    });
  
    // upwrap the "app-ng-modal" data to enable the "modal-dialog-scrollable"
    // and make the modal scrollable
    // (() => {
    //   const node: HTMLElement | null = document.querySelector('app-ng-modal');
    //   if (node) {
    //     while (node.firstChild) {
    //       (node.parentNode as HTMLElement).insertBefore(node.firstChild, node);
    //     }
    //   }
    //   // make the modal scrollable by adding the class .modal-dialog-scrollable
    //   // here wait for one second so that we can find the .modal-dialog
    //   setTimeout(() => {
    //     const modalDialog = document.querySelector('.modal-job-scrollable .modal-dialog');
    //     if (modalDialog) {
    //       modalDialog.classList.add('modal-dialog-scrollable');
    //     }
    //   }, 1000)
    // })();
  }
}
