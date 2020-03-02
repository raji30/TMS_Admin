import { Component, OnInit, Input } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Router, ActivatedRoute, Routes } from "@angular/router";
import { RoutesService } from "../../_services/routes.service";
import { Tms_routes } from "../../_models/tms_routes";
import { DriverService } from "./../../_services/driver.service";
import { Driver } from "../../_models/driver";
import { DispatchDeliveryService } from "../../_services/dispatchDelivery.service";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { ToastrService } from "ngx-toastr";
import { Dispatch } from "../../_models/dispatch";
import { DispatchupdateComponent } from "../dispatchupdate/dispatchupdate.component";
import { NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-dispathdelivery",
  templateUrl: "./dispathdelivery.component.html",
  styleUrls: ["./dispathdelivery.component.scss"]
})
export class DispathdeliveryComponent implements OnInit {
  //bsConfig: Partial<BsDatepickerConfig>;
  @Input() orderKeyinput: string;
  @Input() public ContainerDetails: Array<Order_details> = [];
  @Input() isContainerAttributeVisible: boolean = false;

  HeaderData: DeliveryOrderHeader;
  dataSaved = false;
  message = null;
  collapsesign: string;
  routes: Tms_routes;
  driverList: Driver[];
  selectedKey: string;
  dataShow: boolean;
  tempOrderDetailKey: string;
  public DetailsData: Order_details;
  showDispatch = false;
  showImage = true;
  public SearchRecentContainer: string;

  dispatchList: Array<any> = [];
  dispatchData: any;

  dispatchDetails: Array<any> = [];
  routekey: string = "";
  OrderDetailKey: string = "";
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
  showdispatchitemlist: boolean = true;
  showaddupdatedispatchitems: boolean = false;
  searchText: string;
  modalRef: NgbModalRef;
  constructor(
    private _NgbModal: NgbModal,
    private orderService: DeliveryOrderService,
    private dispatchDeliveryService: DispatchDeliveryService,
    private routeservice: RoutesService,
    private driverservice: DriverService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.loadDrivers();
    this.GetOrderstoDispatchDelivery();
    this.loadDispatchItemsList();
    this.showdispatchitemlist = true;
  }
  loadDrivers() {
    this.driverservice.getDrivers().subscribe(
      data => (this.driverList = data),
      error => console.log(error),
      () => console.log("Get driverList", this.driverList)
    );
  }
  GetOrderstoDispatchDelivery() {
    this.dispatchDeliveryService.GetOrderstoDispatchDelivery().subscribe(
      data => (this.ContainerDetails = data),
      error => console.log(error),
      () => console.log("Get OrderDetail", this.ContainerDetails)
    );
  }
  loadDispatchItemsList() {
    this.dispatchDeliveryService.GetDispatchItemsList().subscribe(
      data => (this.dispatchList = data),
      error => console.log(error),
      () => console.log("Get dispatchList", this.dispatchList)
    );
  }

  update() {
    if (
      this.Chassis == null ||
      this.legno == null ||
      this.actualarrival == null ||
      this.actualdeparture == null
    ) {
      this.showError("Enter the missing fields.", "Delivery");
      return;
    }
    var routesData = new Tms_routes();

    routesData.OrderKey = this.DetailsData.OrderKey;
    routesData.OrderDetailKey = this.DetailsData.OrderDetailKey;
    routesData.legtype = 0;
    routesData.Chassis = this.Chassis;
    routesData.legno = this.legno;
    routesData.actualarrival = this.actualarrival;
    routesData.actualdeparture = this.actualdeparture;

    // this.dispatchDeliveryService
    //   .UpdateDispatchDeliveryData(routesData)
    //   .subscribe(() => {
    //     this.dataSaved = true;
    //     this.message = "Success";

    //     this.showSuccess("Container sent to Invoice!!", "Dispatch");
    //     this.selectedKey = null;
    //     this.showImage = true;
    //     this.showDispatch = false;
    //     this.clear();
    //   });
  }

  ngOnChanges() {}

  rowclickEvent(value: Order_details) {
    if (this.tempOrderDetailKey == value.OrderDetailKey) {
      this.tempOrderDetailKey = null;
      this.selectedKey = null;
      this.showImage = false;
      this.showdispatchitemlist = false;
      this.showaddupdatedispatchitems = true;
      return;
    } else {
      this.dataShow = true;
    }

    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
        this.DetailsData = value;
        this.showImage = false;
        this.showdispatchitemlist = false;
        this.showaddupdatedispatchitems = true;
        this.tempOrderDetailKey = value.OrderDetailKey;
        console.log("order Order-Header Data ", this.HeaderData);
        console.log("order Order-Details Data ", this.DetailsData);
      },
      error => {
        console.log(error);
        this.showImage = true;
      },
      () => console.log("order Header Data ", this.HeaderData)
    );
    this.selectedKey = value.OrderDetailKey;
  }

  Submit() {
    this.dispatchDeliveryService
      .UpdateDispatchDeliveryData(this.dispatchDetails)
      .subscribe(
        () => {
          var dispatch = new Dispatch();
          dispatch.OrderDetailKey = this.DetailsData.OrderDetailKey;
          dispatch.status = 7;
          this.dispatchDeliveryService.UpdateStatus(dispatch).subscribe(
            data => {
              this.showSuccess("Dispatch data saved!", "Dispatch");
              this.GetOrderstoDispatchDelivery();
              this.loadDispatchItemsList();
              this.showdispatchitemlist = true;
              this.showaddupdatedispatchitems = false;
              this.selectedKey = null;
              this.showImage = true;
              this.showDispatch = false;
              this.clear();
            },
            error => console.log(error),
            () => console.log("Get dispatchList", this.dispatchList)
          );
        },
        error => {
          console.log(error);
          this.showError("An Unexpected Error Occured.", "Dispatch");
          return;
        }
      );
  }

  hold_Dispatch() {
    var dispatch = new Dispatch();
    dispatch.OrderDetailKey = this.OrderDetailKey;
    dispatch.status = 8;
    this.dispatchDeliveryService.UpdateStatus(dispatch).subscribe(
      data => {
        this.showSuccess("Dispatch data saved!", "Dispatch");
        this.GetOrderstoDispatchDelivery();
        this.loadDispatchItemsList();
        this.showdispatchitemlist = true;
        this.showaddupdatedispatchitems = false;
        this.selectedKey = null;
        this.showImage = true;
        this.showDispatch = false;
        this.clear();
      },
      error => console.log(error),
      () => console.log("Get dispatchList", this.dispatchList)
    );
  }

  Complete_Dispatch() {
    var dispatch = new Dispatch();
    dispatch.OrderDetailKey = this.OrderDetailKey;
    dispatch.status = 9;
    this.dispatchDeliveryService.UpdateStatus(dispatch).subscribe(
      data => {
        this.showSuccess("Dispatch data saved!", "Dispatch");
        this.GetOrderstoDispatchDelivery();
        this.loadDispatchItemsList();
        this.showdispatchitemlist = true;
        this.showaddupdatedispatchitems = false;
        this.selectedKey = null;
        this.showImage = true;
        this.showDispatch = false;
        this.clear();
      },
      error => console.log(error),
      () => console.log("Get dispatchList", this.dispatchList)
    );
  }

  Cancel() {
    this.showdispatchitemlist = true;
    this.showaddupdatedispatchitems = false;
    this.selectedKey = "";
  }

  addupdateDispatch() {
    // if (
    //   this.appointmentNo != undefined &&
    //    this.driverkey != undefined && this.drivernotes != undefined && this.Chassis != undefined &&
    //    this.legno != undefined && this.legtype != undefined &&
    //   this.PortWaitingTimeFrom != undefined &&
    //   this.PortWaitingTimeTo != undefined &&
    //   this.CustomerWaitingTimeFrom != undefined &&
    //   this.CustomerWaitingTimeTo != undefined &&
    //   this.actualarrival != undefined &&
    //   this.actualdeparture != undefined
    // ) {
    //   this.showWarning("", "Empty row cannot be added!");
    //   return;
    // }

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
    newRow.OrderDetailKey = this.DetailsData.OrderDetailKey;
    newRow.appointmentno = this.appointmentNo;
    newRow.driverkey = this.driverkey;
    newRow.drivernotes = this.drivernotes;
    newRow.chassis = this.Chassis;
    newRow.legno = this.legno;
    newRow.legtype = this.legtype;
    if (
      this.PortWaitingTimeFrom == null ||
      this.PortWaitingTimeFrom == undefined
    ) {
      this.PortWaitingTimeFrom = null;
    }
    //newRow.portwaitingtimefrom = this.PortWaitingTimeFrom;
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
    this.showaddupdateEntryDiv = true;
    this.lbladdupdate = "Update";
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

  updateclick(data: any) {
    this.dispatchData = this.dispatchDetails.find(
      x => x.OrderDetailKey == data.OrderDetailKey
    );
  }

  load_DispatchForEdit(data: any) {
    this.showaddupdatedispatchitems = true;
    this.showdispatchitemlist = false;
  
    this.HeaderData = new DeliveryOrderHeader();
    this.HeaderData.orderdetails = new Array<Order_details>();

    this.dispatchDeliveryService.GetDispatch_OrderandDetails(data.OrderDetails.OrderDetailKey).subscribe(
      result => {this.HeaderData =result;       
      },
      error => console.log(error),
      () => console.log("Get dispatchList", this.dispatchList)
    );

    this.dispatchDetails =  new  Array<Dispatch>(); 
    this.dispatchDetails = data.dispatchDetails;
    this.DetailsData = data.OrderDetails;

    // this.HeaderData = new DeliveryOrderHeader();
    //  this.HeaderData.OrderNo =data.OrderNo;
    //  this.HeaderData.OrderDate =data.OrderDate;
    //  this.HeaderData.ordertypedescription =data.ordertypedescription;
   
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


}
