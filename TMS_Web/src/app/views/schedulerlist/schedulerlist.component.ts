import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Order_details } from "../../_models/order_details";
import { Router, ActivatedRoute } from "@angular/router";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { MasterService } from "../../_services/master.service";
import { Status } from "../../common/master";
import { SchedulerService } from "../../_services/scheduler.service";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { NavigationComponent } from "../navigation/navigation.component";
import { Item } from "../../_models/item";
import { RoutesService } from "../../_services/routes.service";
import { Tms_routes } from "../../_models/tms_routes";
import { AccountingOptions } from "../../_models/accountingOptions";
import { AccountingoptionsService } from "../../_services/accountingoptions.service";

@Component({
  providers: [NavigationComponent],
  selector: "app-schedulerlist",
  templateUrl: "./schedulerlist.component.html",
  styleUrls: ["./schedulerlist.component.scss"]
})
export class SchedulerlistComponent implements OnInit {
 // bsConfig: Partial<BsDatepickerConfig>;
  HeaderData: DeliveryOrderHeader;
  DetailData: Order_details;
  DetailsData: Array<Order_details> = [];
  AccountingOptions: Array<Item> = [];
  statuslist: Status[] = [];
  itemlist: Item[] = [];
  statuslistFiltered: Status[] = [];
  dataSaved: boolean = false;
  routesdataSaved: boolean = false;
  accountingoptionsSaved: boolean = false;
  alldataSaved: boolean = false;
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
  public Legtype: number;
  public LastFreeDay: string;
  public DriverNotes: string;
  public SchedulerNotes: string;
  public status: string;
  public tmsroutes: Tms_routes;

  showScheduler = false;
  showImage = true;
  showSchedulerDiv = true;

  optionsChecked = [];

  constructor(
    private NaviComp: NavigationComponent,
    private formBuilder: FormBuilder,
    private schedulerService: SchedulerService,
    private orderService: DeliveryOrderService,
    private master: MasterService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private routesService: RoutesService,
    private accountingoptionsService: AccountingoptionsService
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    // NaviComp.test(6);
  }

  ngOnInit() {
    this.master.getStatusList().subscribe(
      data => {
        this.statuslist = data;
      },
      error => console.log(error),
      () => console.log("Get statuslist", this.statuslist)
    );

    this.master.getItemList(1).subscribe(
      data => {
        this.itemlist = data;
      },
      error => console.log(error),
      () => console.log("Get itemlist", this.itemlist)
    );

    this.loaddata();
  }

  onSubmit() {
    if (
      this.AppDateFrom == null ||
      this.AppDateTo == null ||
      this.PickupDateTime == null ||
      this.DropOffDateTime == null ||
      this.LastFreeDay == null
    ) {
      this.showError("Enter the missing fields.", "Scheduler");
      return;
    }

    //Order Details
    this.DetailData.AppDateFrom = this.AppDateFrom;
    this.DetailData.AppDateTo = this.AppDateTo;
    this.DetailData.PickupDateTime = this.PickupDateTime;
    this.DetailData.DropOffDateTime = this.DropOffDateTime;
    this.DetailData.status = this.status = "4"; //4 - SendtoDispatchAssignment
    this.DetailData.LastFreeDay = this.LastFreeDay;
    this.DetailData.SchedulerNotes = this.SchedulerNotes;

    //TMS route details
    var tmsroutes = new Tms_routes();
    tmsroutes.OrderDetailKey = this.DetailData.OrderDetailKey;
    tmsroutes.OrderKey = this.DetailData.OrderKey;
    tmsroutes.scheduledarrival = this.PickupDateTime;
    tmsroutes.scheduleddeparture = this.DropOffDateTime;
    tmsroutes.legtype = this.Legtype;
    tmsroutes.drivernotes = this.DriverNotes;

    //accounting options
    var AccOptionsChecked = Array<AccountingOptions>();
    AccOptionsChecked = this.optionsChecked;

    this.orderService.updateOrderDetails(this.DetailData).subscribe(
      result => {
        this.dataSaved = true;
        if (this.dataSaved == true) {
          this.dataSaved = false;
          this.routesService.insertRoutesDetails(tmsroutes).subscribe(
            result => {
              this.routesdataSaved = true;
              if (
                this.routesdataSaved == true &&
                AccOptionsChecked.length > 1
              ) {
                this.routesdataSaved = false;
                this.accountingoptionsService
                  .insertAccountingoptions(AccOptionsChecked)
                  .subscribe(
                    result => {
                      this.accountingoptionsSaved = true;
                      var DOdetail = this.DetailData;
                      DOdetail.status = "4"; //4- send to Dispatch Assignment

                      if (this.accountingoptionsSaved == true) {
                        this.accountingoptionsSaved = false;
                        this.orderService
                          .UpdateDOdetailStatus(this.DetailData)
                          .subscribe(
                            result => {
                              this.alldataSaved = true;
                              if (this.alldataSaved == true) {
                                this.alldataSaved = false;
                                this.AccountingOptions = [];
                                this.message = "Scheduled Successfully";
                                this.showSuccess("Container Scheduled successfully", "Scheduler");
                                this.showImage = true;
                                this.showScheduler = false;
                                this.DetailsData = null;
                                this.loaddata();
                              } else {
                                this.showError("An Unexpected Error Occured.", "Scheduler");
                                return;
                              }
                            },
                            error => {console.log(error);
                              this.showError("An Unexpected Error Occured:"+ error, "Scheduler");}
                          );
                      }
                    },
                    error => console.log(error)
                  );
              }
            },
            error => {
              console.log(error);
            }
          );
        }
      },
      error => console.log(error)
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
    this.selectedKey = value.OrderDetailKey;
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
    this.showSchedulerDiv = true;
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
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
  onCheckboxChange(option, event) {
    if (event.target.checked) {
      option.orderdetailkey = this.DetailsData[0].OrderDetailKey;
      option.customerkey = this.DetailsData[0].OrderKey;
      this.optionsChecked.push(option);
    } else {
      for (var i = 0; i < this.itemlist.length; i++) {
        if (this.optionsChecked[i].itemkey == option.itemkey) {
          this.optionsChecked.splice(i, 1);
        }
      }
    }
    console.log(this.optionsChecked);
  }
}
