import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { BsDatepickerConfig } from "ngx-bootstrap";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Router, ActivatedRoute, Routes } from "@angular/router";
import { RoutesService } from "../../_services/routes.service";
import { Tms_routes } from "../../_models/tms_routes";
import { DriverService } from './../../_services/driver.service';
import { Driver } from "../../_models/driver";
import { DispatchAssignmentService } from "../../_services/dispatchAssignment.service";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
@Component({
  selector: 'app-dispatchAssignment',
  templateUrl: './dispatchAssignment.component.html',
  styleUrls: ['./dispatchAssignment.component.scss']
})
export class DispatchAssignmentComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  @Input() orderKeyinput: string;
  @Input() public ContainerDetails: Array<Order_details> = [];
  @Input() isContainerAttributeVisible: boolean = false;
 
  HeaderData: DeliveryOrderHeader;
  dataSaved = false;
  message = null;
  collapsesign: string;
  tmpRoutes:Tms_routes;
  driverList:Driver[];
  constructor(
    private orderService: DeliveryOrderService, 
    private dispatchAssignmentservice : DispatchAssignmentService,
    private routeservice: RoutesService,
    private driverservice:DriverService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.collapsesign = "+";

    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" }
    );

   this.loaddata();
   
      this.driverservice.getDrivers()
      .subscribe(
        data => (this.driverList = data),
        error => console.log(error),
        () => console.log("Get driverList", this.driverList)
      );
  }
  update(fld:Tms_routes) {  
    this.dispatchAssignmentservice.AddDispatchAssignmentData(fld).subscribe(() => {
      this.dataSaved = true;
      this.message = "Success";
      alert(this.message);
      this.loaddata();
    });
    
  }

  ngOnChanges() {
    
  }
  collapseSign(tmsRoutes:Tms_routes ) {
    if (this.collapsesign === "+") {
      this.collapsesign = "-";
    }
    else if (this.collapsesign === "-") {
      this.collapsesign = "+";
    }

    //this.temptmsRoutes.OrderKey = tmsRoutes.OrderKey;
    //this.temptmsRoutes.OrderDetailKey = tmsRoutes.OrderDetailKey;
  }

  rowclickEvent(value: Order_details) {
    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );
  }

  loaddata()
  {
    this.dispatchAssignmentservice
    .GetOrderstoDispatchAssignment()
    .subscribe(
      data => (this.ContainerDetails = data),
      error => console.log(error),
      () => console.log("Get OrderDetail", this.ContainerDetails)
    );
  }
}
