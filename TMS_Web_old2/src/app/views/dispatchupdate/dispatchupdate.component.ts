import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DriverService } from '../../_services/driver.service';
import { Driver } from '../../_models/driver';
import { DispatchDeliveryService } from '../../_services/dispatchDelivery.service';
import { ToastrService } from 'ngx-toastr';
import { Dispatch } from '../../_models/dispatch';
import { DeliveryOrderHeader } from '../../_models/DeliveryOrderHeader';

@Component({
  selector: 'app-dispatchupdate',
  templateUrl: './dispatchupdate.component.html',
  styleUrls: ['./dispatchupdate.component.scss']
})
export class DispatchupdateComponent implements OnInit {
  @Input() public dispatchdata: DeliveryOrderHeader;

  @Input() public OrderDetailKey: string ;

  dispatchList: DeliveryOrderHeader;
  driverList: Driver[];
  dispatchDetails: Array<Dispatch> = [];
  routekey: string = "";
  //OrderDetailKey: string = "";
  driverkey: string;
  drivernotes: string;
  appointmentNo: string;
  PortWaitingTimeFrom: Date;
  PortWaitingTimeTo: Date;
  CustomerWaitingTimeFrom: Date;
  CustomerWaitingTimeTo: Date;
  Chassis: string;
  legno: string;
  legtype: number;
  actualarrival: Date;
  actualdeparture: Date;
  rowindex: number = -1;
  lbladdupdate: string;  
  showaddupdateEntryDiv: boolean = false;

  constructor( private _NgbActiveModal: NgbActiveModal,  private driverservice: DriverService,
    private dispatchDeliveryService: DispatchDeliveryService,  
    private toastr: ToastrService) { }

  ngOnInit() {    
    this.dispatchDeliveryService.GetDispatch_OrderandDetails(this.OrderDetailKey).subscribe(
      data => (this.dispatchList = data),
      error => console.log(error),
      () => console.log("Get dispatchList", this.dispatchList)
    );

    console.log("Dispatch update :", this.dispatchList);
    this.driverservice.getDrivers().subscribe(
      data => (this.driverList = data),
      error => console.log(error),
      () => console.log("Get driverList", this.driverList)
    );

    this.dispatchDeliveryService.GetDispatchItems(this.OrderDetailKey).subscribe(
      data => (this.dispatchDetails = data),
      error => console.log(error),
      () => console.log("Get dispatchDetails", this.dispatchDetails)
    );

  }

  Submit() {
    this.dispatchDeliveryService
      .UpdateDispatchDeliveryData(this.dispatchDetails)
      .subscribe(
        result => {
          this.showSuccess("Container update done!!", "Dispatch");      
          this.clear();
          this. closeModal();
        },
        error => {
          console.log(error);
          this.showError("An Unexpected Error Occured.", "Dispatch");
          return;
        }
      );    
  }

  addupdateDispatch() {  

    if (this.routekey != "" || this.rowindex >= 0) {
      this.dispatchDetails[this.rowindex].appointmentno = this.appointmentNo;
      this.dispatchDetails[this.rowindex].driverkey = this.driverkey;
      this.dispatchDetails[this.rowindex].drivernotes = this.drivernotes;
      this.dispatchDetails[this.rowindex].chassis = this.Chassis;
      this.dispatchDetails[this.rowindex].legno = this.legno;
      this.dispatchDetails[this.rowindex].legtype = this.legtype;
      this.dispatchDetails[
        this.rowindex
      ].portwaitingtimefrom = this.PortWaitingTimeFrom;
      this.dispatchDetails[
        this.rowindex
      ].portwaitingtimeto = this.PortWaitingTimeTo;
      this.dispatchDetails[
        this.rowindex
      ].customerwaitingtimefrom = this.CustomerWaitingTimeFrom;
      this.dispatchDetails[
        this.rowindex
      ].customerwaitingtimeto = this.CustomerWaitingTimeTo;
      this.dispatchDetails[this.rowindex].actualarrival = this.actualarrival;
      this.dispatchDetails[
        this.rowindex
      ].actualdeparture = this.actualdeparture;

      return;
    }

    var newRow: any = {};
    newRow.OrderDetailKey =  this.OrderDetailKey;
    newRow.appointmentno = this.appointmentNo;
    newRow.driverkey = this.driverkey;
    newRow.drivernotes = this.drivernotes;
    newRow.chassis = this.Chassis;
    newRow.legno = this.legno;
    newRow.legtype = this.legtype;   
    newRow.portwaitingtimefrom = this.PortWaitingTimeFrom;
    newRow.portwaitingtimeto = this.PortWaitingTimeTo;
    newRow.customerwaitingtimefrom = this.CustomerWaitingTimeFrom;
    newRow.customerwaitingtimeto = this.CustomerWaitingTimeTo;
    newRow.actualarrival = this.actualarrival;
    newRow.actualdeparture = this.actualdeparture;
    this.dispatchDetails.push(newRow);
    console.log("New Row", newRow);
    this.clear();
    this.showaddupdateEntryDiv = false;   
  }

  editRow(details: Dispatch, index: number) {
    this.rowindex = index;
    this.OrderDetailKey = details.OrderDetailKey;
    this.routekey = details.Routekey;
    this.appointmentNo = details.appointmentno;
    this.driverkey = details.driverkey;
    this.drivernotes = details.drivernotes;
    this.Chassis = details.chassis;
    this.legno = details.legno;
    this.legtype = details.legtype;
    this.PortWaitingTimeFrom = details.portwaitingtimefrom;
    this.PortWaitingTimeTo = details.portwaitingtimeto;
    this.CustomerWaitingTimeFrom = details.customerwaitingtimefrom;
    this.CustomerWaitingTimeTo = details.customerwaitingtimeto;
    this.actualarrival = details.actualarrival;
    this.actualdeparture = details.actualdeparture;  
    this.lbladdupdate = "Update";
    this.showaddupdateEntryDiv = true;
  }

  deleteRow(details: Dispatch, index: number) {
    if (details.Routekey != "" && details.Routekey != undefined) {
      this.showWarning("", "Selected row cannot be deleted!");
      return;
    }
    this.dispatchDetails.splice(index, 1);
  }

  clear() {
    this.Chassis = undefined;
    this.legno = undefined;
    this.legtype = undefined;
    this.actualarrival = undefined;
    this.actualdeparture = undefined;
    this.appointmentNo = undefined;
    this.driverkey = undefined;
    this.drivernotes = undefined;
    this.Chassis = undefined;
    this.legno = undefined;
    this.legtype = undefined;
    this.PortWaitingTimeFrom = undefined;
    this.PortWaitingTimeTo = undefined;
    this.CustomerWaitingTimeFrom = undefined;
    this.CustomerWaitingTimeTo = undefined;
    this.actualarrival = undefined;
    this.actualdeparture = undefined;
    this.rowindex = -1;
  }
  showaddupdateEntry() {
    this.showaddupdateEntryDiv = true;
    this.lbladdupdate = "Add";
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 2000, closeButton: true });
  }

  
  get activeModal() {
    return this._NgbActiveModal;
  }

  closeModal() { this._NgbActiveModal.close('success'); }
}
