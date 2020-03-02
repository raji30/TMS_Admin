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
import {
  NgbActiveModal,
  NgbModal,
  NgbModalRef
} from "@ng-bootstrap/ng-bootstrap";
import { SchedulerUpdateComponent } from "../scheduler-update/scheduler-update.component";
import { Scheduler } from "../../_models/scheduler";

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
  scheduledContainerList: Array<Order_details> = [];
  AccountingOptions: Array<Item> = [];
  acccountingOptions: Array<AccountingOptions> = [];
  statuslist: Status[] = [];
  itemlist: Item[] = [];
  AccOpts: number;
  statuslistFiltered: Status[] = [];
  dataSaved: boolean = false;
  routesdataSaved: boolean = false;
  accountingoptionsSaved: boolean = false;
  alldataSaved: boolean = false;
  message = null;
  public searchText: string;
  public SearchRecentContainer: string;

  registerForm: FormGroup;
  submitted = false;
  selectedKey: string;
  dataShow: boolean;
  tempOrderDetailKey: string;
  OrderDetailKey: string;
  customerKey: string;

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
  public schedulerData: Scheduler;

  showScheduler = false;
  showImage = true;
  showSchedulerDiv = true;
  showScheduledContainerList: boolean;

  optionsChecked = [];
  modalRef: NgbModalRef;

  isDesc: boolean = false;
  column: string = "containerid";
p: number = 1;
 count: number;

  constructor(
    private _NgbModal: NgbModal,
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
    this.loadScheduleddata();
    this.sliceItems();
  }

  sliceItems() {
    var size = 4;
    this.AccOpts = Math.floor(13 / size) + 1;
    console.log("Itemlist : = ", this.itemlist.length);
    console.log("AccOpts  : = ", this.AccOpts);
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
    this.DetailData.AppDateFrom = new Date(this.AppDateFrom); 
    this.DetailData.AppDateTo = new Date(this.AppDateTo); 
    this.DetailData.LastFreeDay = new Date(this.LastFreeDay);
    this.DetailData.SchedulerNotes = this.SchedulerNotes;

    //TMS route details
    var tmsroutes = new Tms_routes();
    tmsroutes.OrderDetailKey = this.DetailData.OrderDetailKey;
    tmsroutes.OrderKey = this.DetailData.OrderKey;
    tmsroutes.scheduledarrival = new Date(this.PickupDateTime);
    tmsroutes.scheduleddeparture = new Date(this.DropOffDateTime);
    tmsroutes.legtype = this.Legtype;
    tmsroutes.drivernotes = this.DriverNotes;

    //accounting options
    var AccOptionsChecked = Array<AccountingOptions>();
    AccOptionsChecked = this.optionsChecked;
    var updateAccOptions = false;

    this.orderService.updateOrderDetails(this.DetailData).subscribe(
      result => {},
      error => {
        console.log(error);
        this.showError("An Unexpected Error Occured.", "Scheduler-Update");
        return;
      }
    );

    this.routesService.insertRoutesDetails(tmsroutes).subscribe(
      result => {},
      error => {
        console.log(error);
        this.showError("An Unexpected Error Occured.", "Scheduler-Update");
        return;
      }
    );

    //deleting existing data before adding
    this.accountingoptionsService
      .UpdateAccountingOptions(this.DetailData.OrderDetailKey)
      .subscribe(
        result => {
          // this.isUpdated = true;
          //alert(result);
        },
        error => {
          console.log(error);
          this.showError("An Unexpected Error Occured.", "Scheduler-Update");
          return;
        }
      );

    if (AccOptionsChecked.length > 0) {
      this.accountingoptionsService
        .insertAccountingoptions(AccOptionsChecked)
        .subscribe(
          result => {},
          error => {
            console.log(error);
            this.showError("An Unexpected Error Occured.", "Scheduler-Update");
            return;
          }
        );
    } 

    var DOdetail = this.DetailData;
    DOdetail.status = "3"; //In progress

    this.orderService.UpdateDOdetailStatus(this.DetailData).subscribe(
      result => {
        this.loaddata();
        this.loadScheduleddata();
        this.showScheduledContainerList = true;
        this.showScheduler  = false;
        this.showSuccess("Container - " + DOdetail.ContainerNo + " Holded!", "Scheduler-Update");
        return;

      },
      error => {
        console.log(error);
        this.showError("An Unexpected Error Occured.", "Scheduler-Update");
        return;
      }
    );

    this.showScheduler = false;
    this.DetailsData = null;
    this.optionsChecked = null;

    for (var j = 0; j < this.itemlist.length; j++) {
      this.itemlist[j].isChecked = false;
    }
    this.loaddata();
    this.loadScheduleddata();
    
    this.showSuccess("Container Scheduled successfully", "Scheduler");
  }

  ngOnChanges() {}

  rowclickEvent(value: Order_details) {
    // if (this.tempOrderDetailKey == value.OrderDetailKey) {
    //   this.dataShow = false;
    //   this.tempOrderDetailKey = null;
    //   this.selectedKey = null;
    //   return;
    // } else {
    //   this.dataShow = true;
    //   this.DetailData = value;
    //   console.log("Testing DetailData ", this.DetailData);
    // }
    this.OrderDetailKey = value.OrderDetailKey;

    this.orderService.GetbyKey(value.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
        this.dataShow = true;
        this.tempOrderDetailKey = value.OrderDetailKey;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );
    this.GetOrderDetailsbykey(this.OrderDetailKey);
    this.Legtype = 0;
    this.showScheduler = true;
    this.showImage = false;
    this.showSchedulerDiv = true;
    this.showScheduledContainerList = false;
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
  loadScheduleddata() {
    this.schedulerService.GetScheduledContainers().subscribe(
      data => {
        this.scheduledContainerList = data;       
      },
      error => console.log(error),
      () => console.log("Get OrderstoSchedule", this.scheduledContainerList)
    );
    this.showScheduledContainerList = true;
  }

  GetOrderDetailsbykey(orderdetailkey: string) {
    this.schedulerService.GetOrderDetailsbykey(orderdetailkey).subscribe(
      data => {
        this.DetailData = data;
      },
      error => console.log(error)
    );
    this.showScheduledContainerList = true;
  }

  //onCheckboxChange
  onCheckboxChange(option, event) {
    if (event.target.checked) {
      option.orderdetailkey = this.OrderDetailKey;
      //   option.customerkey = this.DetailsData[0].OrderKey;
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

  openModal(data: Order_details) {
    this.modalRef = this._NgbModal.open(SchedulerUpdateComponent, {
      backdrop: "static",
      size: "lg",
      keyboard: true,
      centered: true
    });
    this.modalRef.componentInstance.OrderDetailKey = data.OrderDetailKey;
    this.modalRef.componentInstance.containerId = data.containerid;
    this.modalRef.componentInstance.OrderDetail = data;

    this.modalRef.result.then(
      result => {
        if (result === "success") {
          this.loadScheduleddata(); // Refresh Data in table grid
          console.log("Refresh Data ", "success");
        }
      },
      reason => {}
    );
  }

  onCancel() {
    this.showScheduledContainerList = true;
    this.showScheduler = false;

    for (var j = 0; j < this.itemlist.length; j++) {
      this.itemlist[j].isChecked = false;
    }
  }

  loaddata_forEdit(data: Order_details) {
    this.OrderDetailKey = data.OrderDetailKey;

    this.orderService.GetbyKey(data.OrderKey).subscribe(
      data => {
        this.HeaderData = data;
      },
      error => console.log(error),
      () => console.log("order Header Data ", this.HeaderData)
    );

    this.schedulerService.GetOrderDetailsbykey(data.OrderDetailKey).subscribe(
      data => {
        this.DetailData = data;
        console.log(" this.DetailData for edit", this.DetailData);
      },
      error => console.log(error)
    );

    this.schedulerService.GetScheduledContainer(data.OrderDetailKey).subscribe(
      data => {
        this.schedulerData = data;

        this.AppDateFrom = new Date(this.schedulerData.AppDateFrom);
        this.AppDateTo = new Date(this.schedulerData.AppDateTo);
        this.LastFreeDay = new Date(this.schedulerData.LastFreeDay);
        this.SchedulerNotes = this.schedulerData.SchedulerNotes;
        this.Legtype = this.schedulerData.LegType;
        this.PickupDateTime = this.schedulerData.ScheduleArrival;
        this.DropOffDateTime = this.schedulerData.ScheduleDeparture;
        this.DriverNotes = this.schedulerData.DriverNotes;
        this.optionsChecked = this.acccountingOptions = this.schedulerData.accountingBO;
        this.showScheduledContainerList = false;
        this.showScheduler = true;
      },
      error => console.log(error),
      () => console.log("Get scheduler Data", this.schedulerData)
    );

    if (this.schedulerData.accountingBO.length > 0) {
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
    }

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

  hold_Schedule()
  {
      var DOdetail = this.DetailData;
    DOdetail.status = "4"; //4- Hold

    this.orderService.UpdateDOdetailStatus(this.DetailData).subscribe(
      result => {
        this.loaddata();
        this.loadScheduleddata();
        this.showScheduledContainerList = true;
        this.showScheduler  = false;
        this.showSuccess("Container - " + DOdetail.ContainerNo + " Holded!", "Scheduler-Update");
        return;

      },
      error => {
        console.log(error);
        this.showError("An Unexpected Error Occured.", "Scheduler-Update");
        return;
      }
    );
  }
  complete_Schedule()
  {
  var DOdetail = this.DetailData;
    DOdetail.status = "5"; //4- complete schedule

    this.orderService.UpdateDOdetailStatus(this.DetailData).subscribe(
      result => {
        this.loaddata();
        this.loadScheduleddata();
        this.showScheduledContainerList = true;
        this.showScheduler  = false;
        this.showSuccess("Schedule Completed for the Container :" +this.DetailData.ContainerNo ! , "Scheduler-Update");
        return;

      },
      error => {
        console.log(error);
        this.showError("An Unexpected Error Occured.", "Scheduler-Update");
        return;
      }
    );
  }


  
  sort(column) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = column;
    let direction = this.isDesc ? 1 : -1;    

    this.scheduledContainerList = [...this.scheduledContainerList].sort((n1, n2) => {
      if ((this.column == "containerid")) {
        if (n1.containerid > n2.containerid) {
          return 1* direction;
        } else if (n1.containerid < n2.containerid) {
          return -1* direction;
        } else return 0;
      }

      if ((this.column == "ContainerNo")) {
        if (n1.ContainerNo > n2.ContainerNo) {
          return 1* direction;
        } else if (n1.ContainerNo < n2.ContainerNo) {
          return -1* direction;
        } else return 0;
      }
      if ((this.column == "ContainerSizeDesc")) {
        if (n1.ContainerSizeDesc > n2.ContainerSizeDesc) {
          return 1* direction;
        } else if (n1.ContainerSizeDesc < n2.ContainerSizeDesc) {
          return -1* direction;
        } else return 0;
      }
      if ((this.column == "LastFreeDay")) {
        if (n1.LastFreeDay > n2.LastFreeDay) {
          return 1* direction;
        } else if (n1.LastFreeDay < n2.LastFreeDay) {
          return -1* direction;
        } else return 0;
      }

      if ((this.column == "PickupDateTime")) {
        if (n1.PickupDateTime > n2.PickupDateTime) {
          return 1* direction;
        } else if (n1.PickupDateTime < n2.PickupDateTime) {
          return -1* direction;
        } else return 0;
      }
      if ((this.column == "DropOffDateTime")) {
        if (n1.DropOffDateTime > n2.DropOffDateTime) {
          return 1* direction;
        } else if (n1.DropOffDateTime < n2.DropOffDateTime) {
          return -1* direction;
        } else return 0;
      }
      if ((this.column == "StatusDesc")) {
        if (n1.StatusDesc > n2.StatusDesc) {
          return 1* direction;
        } else if (n1.StatusDesc < n2.StatusDesc) {
          return -1* direction;
        } else return 0;
      }
    });
  }
}
