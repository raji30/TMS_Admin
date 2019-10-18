import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { BsDatepickerConfig } from "ngx-bootstrap";
import { Router, ActivatedRoute } from "@angular/router";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { MasterService } from "../../_services/master.service";
import { Status } from "../../common/master";
import { SchedulerService } from "../../_services/scheduler.service";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { NavigationComponent } from "../navigation/navigation.component";

@Component({
  providers: [NavigationComponent],
  selector: "app-schedulerlist",
  templateUrl: "./schedulerlist.component.html",
  styleUrls: ["./schedulerlist.component.scss"]
})
export class SchedulerlistComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  HeaderData: DeliveryOrderHeader;
  DetailData: Order_details;
  DetailsData: Array<Order_details> = [];
  statuslist: Status[] = [];
  statuslistFiltered: Status[] = [];
  dataSaved = false;
  message = null;
  public searchText: string;

  registerForm: FormGroup;
  submitted = false;
  selectedKey: string;
  dataShow: boolean;
  tempOrderDetailKey: string;

  public AppDateFrom: string;
  public AppDateTo: string;
  public PickupDateTime: string;
  public DropOffDateTime: string;
  public status: string;

  showScheduler = false;
  showImage = true;
  showSchedulerDiv = true;

  constructor(
    private NaviComp: NavigationComponent,
    private formBuilder: FormBuilder,
    private schedulerService: SchedulerService,
    private orderService: DeliveryOrderService,
    private master: MasterService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    // NaviComp.test(6);
  }

  ngOnInit() {
    // this.bsConfig = Object.assign(
    //   {},
    //   { containerClass: "theme-orange" },
    //   { dateInputFormat: "MM/DD/YYYY" }
    // );

    // this.booksByStoreID = this.books.filter(
    //   book => book.store_id === this.store.id );

    this.master.getStatusList().subscribe(
      data => {
        this.statuslist = data;       
      },
      error => console.log(error),
      () => console.log("Get statuslist", this.statuslist)
    );

    // for (let prop of this.statuslistFiltered) {
    //   if (prop.name === "OnHold" || prop.name === "SendtoDispatchAssignment") {
    //     this.statuslist.push(prop);
    //   }
    // }

    this.loaddata();
  }

  onSubmit() {
    if (
      this.AppDateFrom == null ||
      this.AppDateTo == null ||
      this.status == null ||
      this.PickupDateTime == null ||
      this.DropOffDateTime == null
    ) {
      this.showError("Enter the missing fields.", "Scheduler");
      return;
    }

    this.DetailData.AppDateFrom = this.AppDateFrom;
    this.DetailData.AppDateTo = this.AppDateTo;
    this.DetailData.PickupDateTime = this.PickupDateTime;
    this.DetailData.DropOffDateTime = this.DropOffDateTime;
    this.DetailData.status = this.status;

    this.orderService.updateOrderDetails(this.DetailData).subscribe(
      () => {
        this.dataSaved = true;
        this.message = "Scheduled Successfully";
        this.showSuccess("Container Scheduled successfully", "Scheduler");
        this.loaddata();
       
        this.showImage = true;
       this.showScheduler =false;
      },
      error => console.log(error),
      () => console.log("Scheduler  ", this.message)
    );
  }

  // onSubmit(field: Order_details) {
  //   if (
  //     field.AppDateFrom == null ||
  //     field.AppDateTo == null ||
  //     field.status == null ||
  //     field.PickupDateTime == null ||
  //     field.DropOffDateTime == null
  //   ) {
  //     this.showError("Enter the missing fields.", "Scheduler");
  //     return;
  //   }

  //   this.orderService.updateOrderDetails(field).subscribe(
  //     () => {
  //       this.dataSaved = true;
  //       this.message = "Scheduled Successfully";
  //       this.loaddata();
  //     },
  //     error => console.log(error),
  //     () => console.log("Scheduler  ", this.message)
  //   );
  // }

  ngOnChanges() {}

  rowclickEvent(value: Order_details) {
    if (this.tempOrderDetailKey == value.OrderDetailKey) {
      this.dataShow = false;
      this.tempOrderDetailKey = null;
      this.selectedKey = null;
      return;
    } else {
      this.dataShow = true;
      this.DetailData = value;
      console.log("Testing DetailData ", this.DetailData);
    }
    this.selectedKey = value.OrderKey;
    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
        this.dataShow = true;
        this.tempOrderDetailKey = value.OrderDetailKey;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );

    this.showScheduler = true;
    this.showImage = false;
    this.showSchedulerDiv= true;
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 4000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 4000, closeButton: true });
  }

  loaddata() {
    this.schedulerService.GetOrderstoSchedule().subscribe(
      data => {
        this.DetailsData = data;
      },
      error => console.log(error),
      () => console.log("Get OrderDetail", this.DetailsData)
    );
  }
}
