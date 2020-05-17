import { Component, OnInit, Input } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { Tms_routes } from "../../_models/tms_routes";
import { MasterService } from "../../_services/master.service";
import { Item } from "../../_models/item";
import { Order_details } from "../../_models/order_details";
import { AccountingoptionsService } from "../../_services/accountingoptions.service";
import { AccountingOptions } from "../../_models/accountingOptions";
import { DatePipe } from "@angular/common";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { RoutesService } from "../../_services/routes.service";
import { ToastrService } from "ngx-toastr";
import { SchedulerService } from "../../_services/scheduler.service";
import { Scheduler } from "../../_models/scheduler";

@Component({
  selector: "app-scheduler-update",
  templateUrl: "./scheduler-update.component.html",
  styleUrls: ["./scheduler-update.component.scss"]
})
export class SchedulerUpdateComponent implements OnInit {
  @Input() OrderDetailKey: string;
  @Input() containerId: string;
  @Input() OrderDetail: Order_details;

  itemlist: Item[] = [];
  orderdetail: Order_details;
  public AppDateFrom: Date;
  public AppDateTo: Date;
  public PickupDateTime: Date;
  public DropOffDateTime: Date;
  public Legtype: number;
  public LastFreeDay: Date;
  public DriverNotes: string;
  public SchedulerNotes: string;
  public status: string;
  public tmsroutes: Tms_routes;
  optionsChecked = [];
  acccountingOptions: Array<AccountingOptions> = [];
  public schedulerData: Scheduler;

  //update purpose
  public temp_AppDateFrom: Date;
  public temp_AppDateTo: Date;
  public temp_PickupDateTime: Date;
  public temp_DropOffDateTime: Date;
  public temp_Legtype: number;
  public temp_LastFreeDay: Date;
  public temp_DriverNotes: string;
  public temp_SchedulerNotes: string;
  public temp_AccOptionsChecked = Array<AccountingOptions>();

  public isUpdated:boolean;
  constructor(
    public datepipe: DatePipe,
    private _NgbActiveModal: NgbActiveModal,
    private toastr: ToastrService,
    private master: MasterService,
    private accountingoption: AccountingoptionsService,
    private orderService: DeliveryOrderService,
    private routesService: RoutesService,
    private schedulerService: SchedulerService
  ) {}

  ngOnInit() {
    this.schedulerService.GetScheduledContainer(this.OrderDetailKey).subscribe(
      data => {
        this.schedulerData = data;
        this.temp_AppDateFrom = this.AppDateFrom = new Date(
          this.schedulerData.AppDateFrom
        );
        this.temp_AppDateTo = this.AppDateTo = new Date(
          this.schedulerData.AppDateTo
        );
        this.temp_LastFreeDay = this.LastFreeDay = new Date(
          this.schedulerData.LastFreeDay
        );
        this.temp_SchedulerNotes = this.SchedulerNotes = this.schedulerData.SchedulerNotes;
        this.temp_Legtype = this.Legtype = this.schedulerData.LegType;
        this.temp_PickupDateTime = this.PickupDateTime = this.schedulerData.ScheduleArrival;
        this.temp_DropOffDateTime = this.DropOffDateTime = this.schedulerData.ScheduleDeparture;
        this.temp_DriverNotes = this.DriverNotes = this.schedulerData.DriverNotes;
        this.temp_AccOptionsChecked = this.acccountingOptions = this.schedulerData.accountingBO;
      },
      error => console.log(error),
      () => console.log("Get scheduler Data", this.schedulerData)
    );

    this.master.getItemList(1).subscribe(
      data => {
        this.itemlist = data;
        for (var i = 0; i < this.schedulerData.accountingBO.length; i++) {
          for (var j = 0; j < this.itemlist.length; j++) {
            if (
              this.acccountingOptions[i].itemkey == this.itemlist[j].itemkey
            ) {
              this.itemlist[j].isChecked = true;
            }
          }
        }
      },
      error => console.log(error),
      () => console.log("Get itemlist", this.itemlist)
    );
    // this.accountingoption
    //   .GetAccountingOptionsbyKey(this.OrderDetailKey)
    //   .subscribe(
    //     data => {
    //       this.acccountingOptions = data;
    //     },
    //     error => console.log(error),
    //     () => console.log("Get acccounting Options", this.acccountingOptions)
    //   );
  }

  onSubmit() {  
    //Order Details
    var detail = new Order_details();
    detail.OrderDetailKey = this.OrderDetailKey;
    detail.AppDateFrom = this.AppDateFrom;
    detail.AppDateTo = this.AppDateTo;
    detail.LastFreeDay = this.LastFreeDay;
    detail.SchedulerNotes = this.SchedulerNotes;

    //TMS route details
    var tmsroutes = new Tms_routes();
    tmsroutes.OrderDetailKey = this.OrderDetailKey;
    tmsroutes.scheduledarrival = this.PickupDateTime;
    tmsroutes.scheduleddeparture = this.DropOffDateTime;
    tmsroutes.legtype = this.Legtype;
    tmsroutes.drivernotes = this.DriverNotes;

    //accounting options
    var AccOptionsChecked = Array<AccountingOptions>();
    AccOptionsChecked = this.acccountingOptions;
    var updateAccOptions = false;

    //check whether to call update method or not for -Order Details-
    if (
      this.AppDateFrom != this.temp_AppDateFrom ||
      this.temp_AppDateTo != this.temp_AppDateTo ||
      this.LastFreeDay != this.temp_LastFreeDay ||
      this.SchedulerNotes != this.temp_SchedulerNotes
    ) {
      this.orderService.updateOrderDetails(detail).subscribe(
        result => {
          this.isUpdated = true;
        },
        error => {
          console.log(error);
          this.showError("An Unexpected Error Occured.", "Scheduler-Update");
          return;
        }
      );
    }
    //check whether to call update method or not for -route details-
    if (
      this.PickupDateTime != this.temp_PickupDateTime ||
      this.DropOffDateTime != this.temp_DropOffDateTime ||
      this.Legtype != this.temp_Legtype ||
      this.DriverNotes != this.temp_DriverNotes
    ) {
      this.routesService.insertRoutesDetails(tmsroutes).subscribe(
        result => {
          this.isUpdated = true;
        },
        error => {
          console.log(error);
          this.showError("An Unexpected Error Occured.", "Scheduler-Update");
          return;
        }
      );
    }

    //check whether to call update method or not for -AccountingOptions-

    // if (
    //   this.schedulerData.accountingBO.length > 0 &&
    //   AccOptionsChecked.length > 0
    // ) {
    //   for (var j = 0; j < AccOptionsChecked.length; j++) {
    //     if (
    //       AccOptionsChecked[j].itemkey != this.temp_AccOptionsChecked[j].itemkey
    //     ) {
    //       updateAccOptions = true;
    //     }
    //   }
    // } 
    
    

//deleting existing data before adding
    this.accountingoption
        .UpdateAccountingOptions(this.OrderDetailKey)
        .subscribe(
          result => {
            this.isUpdated = true;
            //alert(result);
          },
          error => {
            console.log(error);
            this.showError("An Unexpected Error Occured.", "Scheduler-Update");
            return;
          }
        );

    if (AccOptionsChecked.length > 0) {
      this.accountingoption
        .insertAccountingoptions(AccOptionsChecked)
        .subscribe(
          result => {
            this.isUpdated = true;
            //alert(result);
          },
          error => {
            console.log(error);
            this.showError("An Unexpected Error Occured.", "Scheduler-Update");
            return;
          }
        );
    }
    //if ( this.isUpdated) {
      this.showSuccess("Updated successfully", "Scheduler-Update");
      //closing the modal window
      //this.activeModal.dismiss("Cross click");
      this. closeModal();
   // }
  }

  onCheckboxChange(option, event) {
    if (event.target.checked) {
      option.orderdetailkey = this.OrderDetailKey;
      this.acccountingOptions.push(option);
    } else {
      for (var i = 0; i < this.itemlist.length; i++) {
        if (this.acccountingOptions[i].itemkey == option.itemkey) {
          this.acccountingOptions.splice(i, 1);
        }
      }
    }
    console.log(this.acccountingOptions);
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }

  get activeModal() {
    return this._NgbActiveModal;
  }

  closeModal() { this._NgbActiveModal.close('success'); }

}
