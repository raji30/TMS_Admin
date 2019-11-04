import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { BsDatepickerConfig } from "ngx-bootstrap";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Router, ActivatedRoute, Routes } from "@angular/router";
import { RoutesService } from "../../_services/routes.service";
import { Tms_routes } from "../../_models/tms_routes";
import { DriverService } from "./../../_services/driver.service";
import { Driver } from "../../_models/driver";
import { DispatchDeliveryService } from "../../_services/dispatchDelivery.service";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { ToastrService } from "ngx-toastr";
@Component({
  selector: "app-dispathdelivery",
  templateUrl: "./dispathdelivery.component.html",
  styleUrls: ["./dispathdelivery.component.scss"]
})
export class DispathdeliveryComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
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

  Chassis: string;
  legno: string;
  legtype: string;
  actualarrival: string;
  actualdeparture: string;

  constructor(
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
    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" }
    );

    this.dispatchDeliveryService
      .GetOrderstoDispatchDelivery()
      .subscribe(
        data => (this.ContainerDetails = data),
        error => console.log(error),
        () => console.log("Get OrderDetail", this.ContainerDetails)
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
    routesData.actualarrival =this.actualarrival;
    routesData.actualdeparture =this.actualdeparture;

    this.dispatchDeliveryService
      .UpdateDispatchDeliveryData(routesData)
      .subscribe(() => {
        this.dataSaved = true;
        this.message = "Success";
     //   alert(this.message);
        this.showSuccess("Container sent to Invoice!!", "Dispatch");
        this.selectedKey  =  null;
        this.showImage = true;
        this.showDispatch = false;
        this.clear();
      });
  }

  ngOnChanges() {}

  rowclickEvent(value: Order_details) {
    if (this.tempOrderDetailKey == value.OrderDetailKey) {
      this.dataShow = false;
      this.tempOrderDetailKey = null;
      this.selectedKey = null;
      return;
    } else {
      this.dataShow = true;
    }

    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
        this.DetailsData = value;
        this.dataShow = true;
        this.showImage = false;
        this.showDispatch = true;
        this.tempOrderDetailKey = value.OrderDetailKey;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );
    this.selectedKey = value.OrderDetailKey;
  }
  clear() {
    this.Chassis = undefined;
    this.legno = undefined;
    this.legtype = undefined;
    this.actualarrival = undefined;
    this.actualdeparture = undefined;
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }
}
