import {
  Component,
  OnInit,
  Input,
  OnDestroy,
  SimpleChanges,
  OnChanges,
  ViewChild
} from "@angular/core";
import { ModalModule } from "ngx-bootstrap/modal";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { Order_type } from "../../_models/order_type.enum";
import { NgForm } from "@angular/forms";
import { Broker } from "../../_models/broker";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Params, Router, ActivatedRoute } from "@angular/router";
import "rxjs/add/operator/filter";
import "rxjs/add/operator/switchMap";
import { v } from "@angular/core/src/render3";
import { switchMap } from "rxjs/operators";
import { Order_details } from "./../../_models/order_details";
import { DatePipe } from "@angular/common";
import * as moment from "moment";
import { BsDatepickerConfig } from "ngx-bootstrap/datepicker";
import { Subscription } from "rxjs";
import { HttpClient, HttpEventType, HttpResponse } from "@angular/common/http";
import {
  FileSelectDirective,
  FileDropDirective,
  FileUploader
} from "ng2-file-upload";
import {
  OrderType,
  Priority,
  Containersize,
  Status,
  HoldReason,
  Source,
  Carrier,
  LoadDischargePort
} from "../../common/master";
import { MasterService } from "../../_services/master.service";
import { ToastrService } from "ngx-toastr";
import { AppSettings } from "./../../_constants/appsettings";
import { FileUploadService } from "../../_services/fileupload.service";

const URL = "https://evening-anchorage-3159.herokuapp.com/api/";
//const URL = 'http://localhost:4200/api';

@Component({
  selector: "app-dointake",
  templateUrl: "./dointake.component.html",
  styleUrls: ["./dointake.component.scss"]
})
export class DOIntakeComponent implements OnInit, OnChanges, OnDestroy {
  subscription: Subscription;
  bsConfig: Partial<BsDatepickerConfig>;
  @Input() orderKeyinput: string;
  // broker: Broker[];
  // brokerName: string = "Select Broker";

  Orderlist: Array<DeliveryOrderHeader> = [];

  ordertypelist: OrderType[];
  prioritylist: Priority[];
  containersizelist: Containersize[];
  statuslist: Status[];
  holdreasonlist: HoldReason[];
  sourcelist: Source[];
  carrierlist: Carrier[];
  LoadDischargePortList: LoadDischargePort[];

  public doHeader: DeliveryOrderHeader;
  public orderinfo: Order_details[];

  isContainerAttributeVisible: boolean = true;
  isNewDeliveryOrder: boolean = false;
  btnShowcreateNewOrder: boolean = true;
  editmode = false;
  showDO = false;
  showImage = true;

  selectedKey: string;

  orderNo: string;
  errorMessage: string;
  orderKey: string;
  selectedBillToKey = "";
  HolddropdownVisible = false;
  showordernodate:boolean= false;

  myFiles: string[] = [];
  error: string;
  fileUpload = { status: "", message: "", filePath: "" };
  fileUploadcount: number;

  uploader = new FileUploader({ url: AppSettings._BaseURL + "FileUpload" });

  public hasBaseDropZoneOver: boolean = false;
  public hasAnotherDropZoneOver: boolean = false;

  private ContainerDetails: Array<any> = [];
  private newAttribute: any = {};

  constructor(
    private toastr: ToastrService,
    private http: HttpClient,
    private service: DeliveryOrderService,
    private master: MasterService,
    private router: Router,
    private route: ActivatedRoute,
    private fileUploadService: FileUploadService
  ) {
    this.doHeader = null;
    // this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  onSelectedCustKeyAddress(addressKey: string) {
    this.doHeader.CustKey = addressKey;
  }
  onSelectedBilltoAddress(addressKey: string) {
    this.doHeader.BillToAddress = addressKey;
  }
  onSelectedPickupAddress(addressKey: string) {
    this.doHeader.SourceAddress = addressKey;
  }
  onSelectedConsigneeAddress(addressKey: string) {
    this.doHeader.DestinationAddress = addressKey;
  }
  onSelectedReturnAddress(addressKey: string) {
    this.doHeader.ReturnAddress = addressKey;
  }
  onSelectedBroker(brokerkey: string) {
    this.doHeader.Brokerkey = brokerkey;
  }

  onSelectedContainerDetails(Order_details: any[]) {
    this.doHeader.orderdetails = Order_details;
  }
  onOrdernoGenerated(newOrderno: string) {
    this.showordernodate = true;
    this.doHeader.OrderNo = newOrderno;
    this.doHeader.OrderDate = new Date(); //.toLocaleDateString();
  }

  ngOnInit(): void {
    this.doHeader = null;
    this.doHeader = new DeliveryOrderHeader();
    this.doHeader.orderdetails = new Array<Order_details>();
    //this.orderNo = this.route.snapshot.paramMap.get("order");
    this.orderNo = this.orderKeyinput;

    this.showDO = false;
    this.showImage = true;

    // this.master
    //   .getContainerSizeList()
    //   .subscribe(
    //     data => (this.containersizelist = data),
    //     error => console.log(error),
    //     () => console.log("Get containersizelist", this.containersizelist)
    //   );

    this.master
      .getPriorityList()
      .subscribe(
        data => (this.prioritylist = data),
        error => console.log(error),
        () => console.log("Get prioritylist", this.prioritylist)
      );

    this.master
      .getOrderTypeList()
      .subscribe(
        data => (this.ordertypelist = data),
        error => console.log(error),
        () => console.log("Get ordertypelist", this.ordertypelist)
      );

    this.master
      .getHoldReasonList()
      .subscribe(
        data => (this.holdreasonlist = data),
        error => console.log(error),
        () => console.log("Get holdreasonlist", this.holdreasonlist)
      );

    this.master
      .getStatusList()
      .subscribe(
        data => (this.statuslist = data),
        error => console.log(error),
        () => console.log("Get statuslist", this.statuslist)
      );

    this.master
      .getSourceList()
      .subscribe(
        data => (this.sourcelist = data),
        error => console.log(error),
        () => console.log("Get sourcelist", this.sourcelist)
      );

    this.master
      .getLoadDischargePortList(3)
      .subscribe(
        data => (this.LoadDischargePortList = data),
        error => console.log(error),
        () =>
          console.log("Get LoadDischargePortList", this.LoadDischargePortList)
      );

    this.master
      .getCarrierList()
      .subscribe(
        data => (this.carrierlist = data),
        error => console.log(error),
        () => console.log("Get carrierlist", this.carrierlist)
      );

    this.service
      .getOrderlist()
      .subscribe(
        data => (this.Orderlist = data),
        error => console.log(error),
        () => console.log("Get OrderList complete", this.Orderlist)
      );

    if (this.orderNo != undefined) {
      this.isContainerAttributeVisible = false;
      this.isNewDeliveryOrder = false;

      //getting order info from the DB..
      this.service.GetbyKey(this.orderNo).subscribe(data => {
        ((this.doHeader = data),
        this.service
          .GetOrderDetailsbyKey(this.orderNo)
          .subscribe(
            data => (this.doHeader.orderdetails = data),
            error => console.log(error),
            () => console.log("Get OrderDetail", this.doHeader.orderdetails)
          )),
          error => console.log(error),
          () =>
            console.log(
              "Get Order Detail BillToAddress",
              this.doHeader.BillToAddress
            );
      });
    }
  }

  OnSubmit(form) {
    if (this.doHeader.CustKey == null || this.doHeader.CustKey == undefined||this.doHeader.CustKey=="") {
      this.showError("Customer is required", "Header");
      return;
    }
    if (this.doHeader.BillToAddress == null || this.doHeader.BillToAddress == undefined||this.doHeader.BillToAddress=="") {
      this.showError("Consignee is required", "Header");
      return;
    }
    if (this.doHeader.SourceAddress == null || this.doHeader.SourceAddress == undefined||this.doHeader.SourceAddress=="") {
      this.showError("Pickup is required", "Header");
      return;
    }
    if (this.doHeader.DestinationAddress == null || this.doHeader.DestinationAddress == undefined||this.doHeader.DestinationAddress=="") {
      this.showError("Delivery is required", "Header");
      return;
    }
    if (this.isNewDeliveryOrder) {
      if (
        this.doHeader.orderdetails == null ||
        this.doHeader.orderdetails.length == 0
      ) {
        this.showError("Container data is missing!!", "Container");
        return;
      }

      // for (let i = 0; i < this.doHeader.orderdetails.length; i++) {
      //   if (
      //     this.doHeader.orderdetails[i].containerNo == null ||
      //     this.doHeader.orderdetails[i].containerSize == null ||
      //     this.doHeader.orderdetails[i].sealNo == null ||
      //     this.doHeader.orderdetails[i].weight == null
      //   ) {
      //     console.log(this.doHeader.orderdetails[i]);
      //     this.showError("Container data is missing!!", "Container");
      //     return;
      //   }
      // }
      // this.doHeader.orderdetails = this.ContainerDetails;

      console.log("Container Details", this.doHeader.orderdetails);
      this.service.saveDOHeader(form.value).subscribe(
        result => {
          this.orderKey = result;
          if (this.orderKey != undefined && this.orderKey != "") {
            this.saveDeliveryDetails();
          }
        },
        error => {
          this.errorMessage = error;
          this.showError(this.errorMessage, "New-Order");
        }
      );
      //this.showSuccess("Order Created successfully", "New-Order");
      //applying orderkey to order details
    } else {
    }
  }

  private saveDeliveryDetails() {
    for (let order of this.doHeader.orderdetails) {
      order.OrderKey = this.orderKey;
    }

    this.service.saveOrderDetails(this.doHeader.orderdetails).subscribe(
      results => {
        this.showSuccess("Order Created successfully", "New-Order");
        this.service
          .getOrderlist()
          .subscribe(
            data => (this.Orderlist = data),
            error => console.log(error),
            () => console.log("Get OrderList complete", this.Orderlist)
          );
        this.createNewOrder();
        this.getOrderInfo(this.orderKey);
      },
      error => (this.errorMessage = error)
    );
  }

  clear() {
    this.doHeader.BrokerRefNo = undefined;
    this.doHeader.OrderKey = undefined;
    this.doHeader.OrderNo = undefined;
    //this.doHeader. CustKey:string ;
    this.doHeader.OrderDate = undefined;
    //BillToAddress: string ;
    //SourceAddress: string ;
    // DestinationAddress: string ;
    //ReturnAddress: string ;
    //Source:string ;
    this.doHeader.Source = undefined;
    this.doHeader.OrderType = undefined;
    this.doHeader.Status = undefined;
    this.doHeader.StatusDate = undefined;
    this.doHeader.HoldReason = undefined;
    this.doHeader.HoldDate = undefined;
    this.doHeader.BrokerName = "Select Broker";
    //BrokerId:string ;
    //Brokerkey: string;
    // PortofOriginKey=undefined;
    // CarrierKey=undefined;
    this.doHeader.VesselName = undefined;
    this.doHeader.BillofLading = undefined;
    this.doHeader.BookingNo = undefined;
    this.doHeader.CutOffDate = undefined;
    this.doHeader.Priority = undefined;
    this.doHeader.IsHazardous = undefined;
    this.doHeader.CreatedBy = undefined;
    this.doHeader.CreatedDate = undefined;

    this.doHeader.orderdetails = null;
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  ngOnChanges() {}

  ngOnDestroy() {
    this.subscription && this.subscription.unsubscribe();
  }

  StatusDropDownChanged(val: number) {
    if (val.toString() === "10") {
      //OnHold
      this.HolddropdownVisible = true;
    } else {
      this.HolddropdownVisible = false;
    }
  }

  onUploadSubmit() {
    if (this.myFiles.length === 0) {
      return this.showWarning("No File(s) selected", "Upload");
    }
    for (var i = 0; i < this.myFiles.length; i++) {
      const frmData = new FormData();
      frmData.append("fileUpload", this.myFiles[i]);
      frmData.append("DO", this.doHeader.OrderNo);
      frmData.append("CreatedBy", this.doHeader.CreatedBy);

      this.fileUploadService.upload(frmData).subscribe(
        res => {
          this.fileUpload.status = res.toString();
          console.log("testt", res);
          this.fileUploadcount = this.fileUploadcount + 1;
          this.myFiles = [];
        },
        err => {
          this.error = err;
          this.showError(this.error, "Upload Error");
        }
      );
    }
    this.showSuccess("File(s) uploaded successfully", "Upload");
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }

  showError(message: string, title: string) {
    this.toastr.error(message, title, { timeOut: 2000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 2000, closeButton: true });
  }

  showToast(position: any = "top-left") {
    this.toastr.info("This is a toast.", "Toast", { positionClass: position });
  }

  onSelectedFile(e) {
    this.myFiles = [];
    this.fileUploadcount = 0;
    for (var i = 0; i < e.target.files.length; i++) {
      this.myFiles.push(e.target.files[i]);
    }
  }

  getOrderInfo(inputKey: string) {
    this.doHeader = null;
    this.doHeader = new DeliveryOrderHeader();
    this.doHeader.orderdetails = new Array<Order_details>();
    this.showDO = true;
    this.showImage = false;
    this.isNewDeliveryOrder = false;
    this.btnShowcreateNewOrder = true;
    this.selectedKey = null;

    this.service.GetbyKey(inputKey).subscribe(data => {
      (this.doHeader = data), console.log("testing Model----", this.doHeader);
      this.service
        .GetOrderDetailsbyKey(inputKey)
        .subscribe(
          data => (this.doHeader.orderdetails = data),
          error => console.log(error),
          () => console.log("Get OrderDetail", this.doHeader.orderdetails)
        ),
        error => console.log(error);
    });

    this.isContainerAttributeVisible = true;
    this.isNewDeliveryOrder = false;
    this.editmode = true;

    this.selectedKey = inputKey;
  }

  createNewOrder() {
    this.isNewDeliveryOrder = true;
    this.btnShowcreateNewOrder = false;
    this.showDO = false;
    this.showImage = false;
    this.editmode = false;

    this.doHeader = null;
    this.doHeader = new DeliveryOrderHeader();
    this.doHeader.orderdetails = new Array<Order_details>();
    this.doHeader.CustKey = "";
    this.doHeader.BillToAddress = "";
    this.doHeader.SourceAddress = "";
    this.doHeader.DestinationAddress = "";
    this.doHeader.ReturnAddress = "";
    this.doHeader.Brokerkey = "";
    this.doHeader.Comment = "";
    this.ContainerDetails = new Array<Order_details>();
  }

  addFieldValue() {
    this.ContainerDetails.push(this.newAttribute);
    this.newAttribute = {};
  }

  deleteFieldValue(index) {
    this.ContainerDetails.splice(index, 1);
  }
}
