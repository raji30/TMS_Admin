import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { BsDatepickerConfig } from "ngx-bootstrap";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-scheduler",
  templateUrl: "./scheduler.component.html",
  styleUrls: ["./scheduler.component.scss"]
})
export class SchedulerComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  @Input() orderKeyinput: string;
  @Input() public ContainerDetails: Array<Order_details> = [];
  @Input() isContainerAttributeVisible: boolean = false;
  private AddContainerDetails: Array<Order_details> = [];
  private newAttribute: any = {};
  dataSaved = false;
  message = null;
  @Output() ContainerDetailsOutput = new EventEmitter<any>();
  collapsesign: string;
  selectedcontainer: Order_details;
  constructor(
    private service: DeliveryOrderService,
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

    this.service
      .GetOrderDetailsbyKey(this.orderKeyinput)
      .subscribe(
        data => (this.ContainerDetails = data),
        error => console.log(error),
        () => console.log("Get OrderDetail", this.ContainerDetails)
      );
  }
  ScheduleFieldValue(field: Order_details) {
    //this.ContainerDetails.splice(field, 1);
    this.service.updateOrderDetails(field).subscribe(() => {
      this.dataSaved = true;
      this.message = "Scheduled Successfully";
    });
    alert(this.message);
  }

  ngOnChanges() {
    // alert('Scheduler Onchange:  '+ this.orderKeyinput);
    this.service
      .GetOrderDetailsbyKey(this.orderKeyinput)
      .subscribe(
        data => (this.ContainerDetails = data),
        error => console.log(error),
        () => console.log("Get OrderDetail", this.ContainerDetails)
      );
  }
  collapseSign() {
    if (this.collapsesign === "+") {
      this.collapsesign = "-";
    }
    else if (this.collapsesign === "-") {
      this.collapsesign = "+";
    }
  }
}
