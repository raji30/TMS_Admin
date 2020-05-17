import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Router, ActivatedRoute, Routes } from "@angular/router";
import { RoutesService } from "../../_services/routes.service";
import { Tms_routes } from "../../_models/tms_routes";
import { DriverService } from "./../../_services/driver.service";
import { Driver } from "../../_models/driver";
import { DispatchAssignmentService } from "../../_services/dispatchAssignment.service";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { ToastrService } from "ngx-toastr";
import { findLast } from "@angular/compiler/src/directive_resolver";
@Component({
  selector: "app-dispatchAssignment",
  templateUrl: "./dispatchAssignment.component.html",
  styleUrls: ["./dispatchAssignment.component.scss"]
})
export class DispatchAssignmentComponent implements OnInit {
  //bsConfig: Partial<BsDatepickerConfig>;
  @Input() orderKeyinput: string;
  @Input() public ContainerDetails: Array<Order_details> = [];
  @Input() isContainerAttributeVisible: boolean = false;
  showDispatch = false;
  showImage = true;

  HeaderData: DeliveryOrderHeader;
  public DetailsData: Array<Order_details> = [];
  dataSaved = false;
  message = null;
  collapsesign: string;
  public routes: Tms_routes;
  driverList: Driver[];
  selectedKey: string;

  driverkey: string;
  drivernotes: string;
  OrderKey: string;
  OrderDetailKey: string;

  constructor(
    private orderService: DeliveryOrderService,
    private dispatchAssignmentservice: DispatchAssignmentService,
    private routeservice: RoutesService,
    private driverservice: DriverService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.collapsesign = "+";

    // this.bsConfig = Object.assign(
    //   {},
    //   { containerClass: "theme-orange" },
    //   { dateInputFormat: "MM/DD/YYYY" }
    // );

    this.loaddata();

    this.driverservice
      .getDrivers()
      .subscribe(
        data => (this.driverList = data),
        error => console.log(error),
        () => console.log("Get driverList", this.driverList)
      );
  }
  update() {
    if (this.driverkey == null || this.drivernotes == null) {
      this.showError("Enter the missing fields.", "Scheduler");
      return;
    }

    var route = new Tms_routes();
    route.OrderKey = this.OrderKey;
    route.OrderDetailKey = this.OrderDetailKey;
    route.driverkey = this.driverkey;
    route.drivernotes = this.drivernotes;

    this.dispatchAssignmentservice
      .AddDispatchAssignmentData(route)
      .subscribe(() => {
        this.dataSaved = true;
        this.message = "Success";
        //alert(this.message);
        this.showSuccess("Container sent to dispatch delivery!!", "Dispatch");
        this.loaddata();
        this.showImage = true;
        this.showDispatch = false;
      });
  }

  ngOnChanges() {}
  collapseSign(tmsRoutes: Tms_routes) {
    if (this.collapsesign === "+") {
      this.collapsesign = "-";
    } else if (this.collapsesign === "-") {
      this.collapsesign = "+";
    }

    //this.temptmsRoutes.OrderKey = tmsRoutes.OrderKey;
    //this.temptmsRoutes.OrderDetailKey = tmsRoutes.OrderDetailKey;
  }

  rowclickEvent(value: any) {
    this.selectedKey = null;

    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
        this.DetailsData = value;
        this.OrderKey = value.OrderKey;
        this.OrderDetailKey = value.OrderDetailKey;
        this.selectedKey = value.OrderDetailKey;
        this.showImage = false;
        this.showDispatch = true;
        this.driverkey = undefined;
        this.drivernotes = undefined;
        console.log("OrderKey ", this.OrderKey);
        console.log("OrderDetailKey ", this.OrderDetailKey);
        console.log("DetailsData ", this.DetailsData);
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );
  }

  loaddata() {
    this.dispatchAssignmentservice
      .GetOrderstoDispatchAssignment()
      .subscribe(
        data => (this.ContainerDetails = data),
        error => console.log(error),
        () => console.log("Get OrderDetail", this.ContainerDetails)
      );
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }
}
