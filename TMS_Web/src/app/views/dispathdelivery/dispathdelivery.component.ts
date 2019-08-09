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
  tmpRoutes: Tms_routes;
  driverList: Driver[];
  selectedKey: string;
  dataShow: boolean;
  tempOrderDetailKey :string;
  constructor(
    private orderService: DeliveryOrderService,
    private dispatchDeliveryService : DispatchDeliveryService ,
    private routeservice: RoutesService,
    private driverservice: DriverService,
    private router: Router,
    private route: ActivatedRoute
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
  update(fld: Tms_routes) {
    this.dispatchDeliveryService.UpdateDispatchDeliveryData(fld).subscribe(() => {
      this.dataSaved = true;
      this.message = "Success";
      alert(this.message);
    });
  }

  ngOnChanges() {
  }

  rowclickEvent(value: Order_details) {
    if (this.tempOrderDetailKey == value.OrderDetailKey )
    {
      this.dataShow = false;       
      this.tempOrderDetailKey = null;
      this.selectedKey = null;
      return;
    }
    else
    {
      this.dataShow = true;  
    }

    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
        this.dataShow = true; 
        this.tempOrderDetailKey = value.OrderDetailKey;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );
    this.selectedKey = value.OrderDetailKey;
  }
}
