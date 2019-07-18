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

@Component({
  selector: "app-schedulerlist",
  templateUrl: "./schedulerlist.component.html",
  styleUrls: ["./schedulerlist.component.scss"]
})
export class SchedulerlistComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  HeaderData: DeliveryOrderHeader;
  DetailsData: Array<Order_details> = [];
  statuslist: Status[];
  dataSaved = false;
  message = null;
  public searchText: string;

  registerForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private schedulerService: SchedulerService,
    private orderService: DeliveryOrderService,
    private master: MasterService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    // this.bsConfig = Object.assign(
    //   {},
    //   { containerClass: "theme-orange" },
    //   { dateInputFormat: "MM/DD/YYYY" }
    // );
  //   this.registerForm = this.formBuilder.group({
  //     AppDateFrom: ['', Validators.required],
  //     AppDateTo: ['', Validators.required],
  //     Pickupdatetime: ['', [Validators.required]],
  //     DropOffdatetime: ['', [Validators.required]]
  // });
    this.master
      .getStatusList()
      .subscribe(
        data => (this.statuslist = data),
        error => console.log(error),
        () => console.log("Get statuslist", this.statuslist)
      );
    // this.service.GetOrderHeaderandDetails().subscribe(
    //   data => {
    //     this.schedulerlist = data;
    //   },
    //   error => console.log(error),
    //   () => console.log("Scheduler list ", this.schedulerlist)
    // );

 this.loaddata();
  }
  onSubmit(field: Order_details) {   
    
    if(field.AppDateFrom==null||field.AppDateTo==null || field.status==null || field.PickupDateTime == null|| field.DropOffDateTime==null)
    {
      this.showError("Enter the missing fields.", "Scheduler");
      return;
    }

    this.orderService.updateOrderDetails(field).subscribe(() => {      
      this.dataSaved = true;
      this.message = "Scheduled Successfully";
      this.loaddata();
    },
    error => console.log(error),
    () => console.log("Scheduler  ", this.message));    
  }

  ngOnChanges() {}

  rowclickEvent(value:Order_details)
  {
        this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 4000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 4000, closeButton: true });
  }

  loaddata()
  {

    this.schedulerService.GetOrderstoSchedule().subscribe(
      data => {
        this.DetailsData = data;        
      },
      error => console.log(error),
      () => console.log("Get OrderDetail", this.DetailsData)
    );
  }
}
