import {
  Component,
  OnInit,
  Input,
  OnDestroy,
  SimpleChanges,
  OnChanges,
  ViewChild
} from "@angular/core";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { Order_type } from "../../_models/order_type.enum";
import { NgForm } from "@angular/forms";
import { Broker } from "../../_models/broker";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { Params, Router, ActivatedRoute } from "@angular/router";
import "rxjs/add/operator/filter";
import "rxjs/add/operator/switchMap";
//import { v } from "@angular/core/src/render3";
import { switchMap } from "rxjs/operators";
import { Order_details } from "./../../_models/order_details";
import { DatePipe } from "@angular/common";
import * as moment from "moment";
import { Subscription } from "rxjs";
import { HttpClient, HttpEventType, HttpResponse } from "@angular/common/http";

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
import { findLast } from "@angular/compiler/src/directive_resolver";
import { FileUploaderService } from "../../_services/file-uploader.service";

//const URL = "https://evening-anchorage-3159.herokuapp.com/api/";
//const URL = 'http://localhost:4200/api';

@Component({
  selector: "app-dointake",
  templateUrl: "./dointake.component.html",
  styleUrls: ["./dointake.component.scss"]
})
export class DOIntakeComponent implements OnInit, OnChanges, OnDestroy {
  subscription: Subscription;
 // bsConfig: Partial<BsDatepickerConfig>;
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

  isEditmode: boolean = false;

  public doHeader: DeliveryOrderHeader;
  public orderinfo: Order_details[];
  searchText: string;

  isContainerAttributeVisible: boolean = true;
  CreateOrEditOrder: boolean = false;
  lblCreateOrEdit: string = "";
  ShowOrderList: boolean = true;
  btnShowcreateNewOrder: boolean = true;
  editmode = false;
  showDO = false;
  showImage = false;
  customer_disabled = false;
  selectedKey: string;

  orderNo: string;
  errorMessage: string;
  orderKey: string;
  selectedBillToKey = "";
  HolddropdownVisible = false;
  showordernodate: boolean = false;

  myFiles: string[] = [];
  error: string;
  fileUpload = { status: "", message: "", filePath: "" };
  fileUploadcount: number;

  //uploader = new FileUploader({ url: AppSettings._BaseURL + "FileUpload" });

  display = "none"; //default Variable

  public hasBaseDropZoneOver: boolean = false;
  public hasAnotherDropZoneOver: boolean = false;

  addupdatecontainer: Array<Order_details> = [];
  public ContainerDetails: Array<Order_details> = [];
  private AddContainerDetails: Order_details;
  private newAttribute: any = {};
  rowindex: number;

  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  public OrderDetailKey: string = "";
  public ContainerSize: string = "";
  public ContainerSizeDesc: string = "";
  public ContainerNo: string = "";
  public Chassis: string = "";
  public SealNo: string = "";
  public Weight: number;
  public Comments: string = "";
  public CommentItems = new Array();

  hazard: string;
  trixale: string;
  overweight: string;
  needstobescaled: string;
  IsHazardChecked: boolean = false;
  IsOverweightChecked: boolean = false;
  IsTrixaleChecked: boolean = false;
  IsNeedstobescaledChecked: boolean = false;

  constructor(
    private toastr: ToastrService,
    private http: HttpClient,
    private service: DeliveryOrderService,
    private master: MasterService,
    private router: Router,
    private route: ActivatedRoute,
    private fileUploadService: FileUploadService,
    private fileUploaderService:FileUploaderService
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
    
    this.master.getContainerSizeList().subscribe(
      data => (this.containersizelist = data),
      error => console.log(error),
      () => console.log("Get containersizelist", this.containersizelist)
    );

    this.master.getPriorityList().subscribe(
      data => (this.prioritylist = data),
      error => console.log(error),
      () => console.log("Get prioritylist", this.prioritylist)
    );

    this.master.getOrderTypeList().subscribe(
      data => (this.ordertypelist = data),
      error => console.log(error),
      () => console.log("Get ordertypelist", this.ordertypelist)
    );

    this.master.getHoldReasonList().subscribe(
      data => (this.holdreasonlist = data),
      error => console.log(error),
      () => console.log("Get holdreasonlist", this.holdreasonlist)
    );

    this.master.getStatusList().subscribe(
      data => (this.statuslist = data),
      error => console.log(error),
      () => console.log("Get statuslist", this.statuslist)
    );

    this.master.getSourceList().subscribe(
      data => (this.sourcelist = data),
      error => console.log(error),
      () => console.log("Get sourcelist", this.sourcelist)
    );

    this.master.getLoadDischargePortList(3).subscribe(
      data => (this.LoadDischargePortList = data),
      error => console.log(error),
      () => console.log("Get LoadDischargePortList", this.LoadDischargePortList)
    );

    this.master.getCarrierList().subscribe(
      data => (this.carrierlist = data),
      error => console.log(error),
      () => console.log("Get carrierlist", this.carrierlist)
    );

    this.service.getOrderlist().subscribe(
      data => (this.Orderlist = data),
      error => console.log(error),
      () => console.log("Get OrderList complete", this.Orderlist)
    );

    if (this.orderNo != undefined) {
      this.isContainerAttributeVisible = false;
      this.CreateOrEditOrder = false;

      //getting order info from the DB..
      this.service.GetbyKey(this.orderNo).subscribe(data => {
        ((this.doHeader = data),
        this.service.GetOrderDetailsbyKey(this.orderNo).subscribe(
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

    if(this.Orderlist.length<1)
    {
        this.showImage = true;
    }

    this.dropdownList = [
      { item_id: 1, item_text: "Hazard" },
      { item_id: 2, item_text: "Overweight" },
      { item_id: 3, item_text: "Triaxle" },
      { item_id: 4, item_text: "Needs to be scaled" }
    ];
    this.selectedItems = [
      // { item_id: 3, item_text: 'Pune' },
      // { item_id: 4, item_text: 'Navsari' }
    ];
    this.dropdownSettings = {
      singleSelection: false,
      idField: "item_id",
      textField: "item_text",
      selectAllText: "Select All",
      unSelectAllText: "UnSelect All",
      itemsShowLimit: 4,
      allowSearchFilter: false
    };
  }

  OnSubmit(form) {
    if (
      this.doHeader.CustKey == null ||
      this.doHeader.CustKey == undefined ||
      this.doHeader.CustKey == ""
    ) {
      this.showError("Customer is required", "Header");
      return;
    }
    if (
      this.doHeader.BillToAddress == null ||
      this.doHeader.BillToAddress == undefined ||
      this.doHeader.BillToAddress == ""
    ) {
      this.showError("Consignee is required", "Header");
      return;
    }
    if (
      this.doHeader.SourceAddress == null ||
      this.doHeader.SourceAddress == undefined ||
      this.doHeader.SourceAddress == ""
    ) {
      this.showError("Pickup is required", "Header");
      return;
    }
    if (
      this.doHeader.DestinationAddress == null ||
      this.doHeader.DestinationAddress == undefined ||
      this.doHeader.DestinationAddress == ""
    ) {
      this.showError("Delivery is required", "Header");
      return;
    }

    if (
      this.doHeader.orderdetails == null ||
      this.doHeader.orderdetails.length == 0
    ) {
      this.showError("Container data is missing!!", "Container");
      return;
    }
    if (!this.isEditmode) {
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
    } else {
      this.service.updateDOHeader(form.value).subscribe(
        result => {
          if (result) {
            this.updateDeliveryOrderDetails();
          }
        },
        error => {
          this.errorMessage = error;
          this.showError(this.errorMessage, "New-Order");
        }
      );
    }
  }

  updateDeliveryOrderDetails() {
    for (let order of this.doHeader.orderdetails) {
      order.OrderKey = this.doHeader.OrderKey;
    }
    this.service
      .updateDeliveryOrderDetails(this.doHeader.orderdetails)
      .subscribe(
        results => {
          this.showSuccess("Order Updated successfully", "Order Update");
          this.service.getOrderlist().subscribe(
            data => (this.Orderlist = data),
            error => console.log(error),
            () => console.log("Get OrderList complete", this.Orderlist)
          );
          this.cancel();
        },
        error => (this.errorMessage = error)
      );
  }

  private saveDeliveryDetails() {
    for (let order of this.doHeader.orderdetails) {
      order.OrderKey = this.orderKey;
    }

    this.service.saveOrderDetails(this.doHeader.orderdetails).subscribe(
      results => {
        this.showSuccess("Order Created successfully", "New-Order");
        this.service.getOrderlist().subscribe(
          data => (this.Orderlist = data),
          error => console.log(error),
          () => console.log("Get OrderList complete", this.Orderlist)
        );
        this.createNewOrder();
        this.cancel();
       // this.getOrderInfo(this.orderKey);
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

      // this.fileUploadService.upload(frmData).subscribe(
      //   res => {
      //     this.fileUpload.status = res.toString();
      //     console.log("his.fileUpload.status", this.fileUpload.status);
      //     this.fileUploadcount = this.fileUploadcount + 1;
      //     this.myFiles = [];
      //   },
      //   err => {
      //     this.error = err;
      //     this.showError(this.error, "Upload Error");
      //   }
      // );

      // this.fileUploaderService.uploadAll(this.doHeader.OrderNo,this.doHeader.CreatedBy).subscribe(
      //   res => {
      //     this.fileUpload.status = res.toString();
      //     console.log("his.fileUpload.status", this.fileUpload.status);
      //     this.fileUploadcount = this.fileUploadcount + 1;
      //     this.myFiles = [];
      //   },
      //   err => {
      //     this.error = err;
      //     this.showError(this.error, "Upload Error");
      //   }
      // );

      
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

  view(inputKey: string) {
    this.getOrderInfo(inputKey);
    this.customer_disabled = true;
    this.display = "block";
    this.showDO = false;
    this.ShowOrderList = true;
    this.CreateOrEditOrder = false;
    this.showImage = false;
  }
  update(inputKey: string) {
    this.lblCreateOrEdit = "Update";
    this.isEditmode = true;
    this.getOrderInfo(inputKey);
    this.CreateOrEditOrder = true;
    this.editmode = true;
    this.showDO = false;
    this.ShowOrderList = false;
    this.showImage = false;
  }
  getOrderInfo(inputKey: string) {
    this.doHeader = null;
    this.doHeader = new DeliveryOrderHeader();
    this.doHeader.orderdetails = new Array<Order_details>();

    this.btnShowcreateNewOrder = true;
    this.selectedKey = null;

    this.service.GetbyKey(inputKey).subscribe(data => {
      (this.doHeader = data), console.log("testing Model----", this.doHeader);
      this.service.GetOrderDetailsbyKey(inputKey).subscribe(
        data => (this.doHeader.orderdetails = data),
        error => console.log(error),
        () => console.log("Get OrderDetail", this.doHeader.orderdetails)
      ),
        error => console.log(error);
    });

    this.isContainerAttributeVisible = true;
    this.selectedKey = inputKey;
  }

  createNewOrder() {
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
    this.customer_disabled = false;
    this.CreateOrEditOrder = true;
    this.lblCreateOrEdit = "Create";
    this.btnShowcreateNewOrder = false;
    this.ShowOrderList = false;
    this.showDO = false;
    this.showImage = false;
    this.editmode = false;
  }

  openModalDialog() {
    this.display = "block"; //Set block css
  }

  closeModalDialog() {
    this.display = "none"; //set none css after close dialog
  }

  cancel() {
    this.ShowOrderList = true;
    this.CreateOrEditOrder = false;
    this.showImage = false;
    this.isEditmode = false;
  }

  /////////////////////////////////////////////

  deleteFieldValue(index) {
    this.doHeader.orderdetails.splice(index, 1);
  }
  onSelectedcontainersize(ContainerNo: string) {
    this.newAttribute.ContainerNo = ContainerNo;
  }

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }

  Checkbox1_Change(values: any) {
    //Checkbox1_Change_Hazard
    if (values.currentTarget.checked) {
      this.CommentItems.push("Hazard");
      console.log("Hazard Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Hazard");
      this.CommentItems.splice(index, 1);
      console.log("Hazard Removed", this.CommentItems);
    }
  }
  Checkbox2_Change(values: any) {
    //Checkbox2_Change_OverWeight
    if (values.currentTarget.checked) {
      this.CommentItems.push("Over Weight");
      console.log("Over Weight Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Over Weight");
      this.CommentItems.splice(index, 1);
      console.log("Over Weight Removed", this.CommentItems);
    }
  }
  Checkbox3_Change(values: any) {
    //Checkbox3_Change_Triaxle
    console.log(values.currentTarget.checked);

    if (values.currentTarget.checked) {
      this.CommentItems.push("Triaxle");
      console.log("Triaxle Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Triaxle");
      this.CommentItems.splice(index, 1);
      console.log("Triaxle Removed", this.CommentItems);
    }
  }
  Checkbox4_Change(values: any) {
    if (values.currentTarget.checked) {
      this.CommentItems.push("Needs to be scaled");
      console.log("Needs to be scaled Added", this.CommentItems);
    } else {
      const index = this.CommentItems.indexOf("Needs to be scaled");
      this.CommentItems.splice(index, 1);
      console.log("Needs to be scaled Removed", this.CommentItems);
    }
  }

  add() {
    console.log("Testing....", this.ContainerNo);

    if (this.OrderDetailKey != "") {

      this.doHeader.orderdetails[this.rowindex].ContainerNo = this.ContainerNo;
      this.doHeader.orderdetails[
        this.rowindex
      ].ContainerSize = this.ContainerSize;
      this.doHeader.orderdetails[
        this.rowindex
      ].ContainerSizeDesc = this.ContainerSizeDesc;
      this.doHeader.orderdetails[this.rowindex].Chassis = this.Chassis;
      this.doHeader.orderdetails[this.rowindex].SealNo = this.SealNo;
      this.doHeader.orderdetails[this.rowindex].Weight = this.Weight;
      this.doHeader.orderdetails[
        this.rowindex
      ].Comments = this.Comments = this.CommentItems.toString();
      this.rowRefresh();
      return;
    }

    var containerDetails: any = {};
    containerDetails.ContainerNo = this.ContainerNo;
    containerDetails.ContainerSize = this.ContainerSize;
    containerDetails.ContainerSizeDesc = this.ContainerSizeDesc;
    containerDetails.Chassis = this.Chassis;
    containerDetails.SealNo = this.SealNo;
    containerDetails.Weight = this.Weight;
    containerDetails.Comments = this.Comments = this.CommentItems.toString();
    containerDetails.Id= this.doHeader.OrderNo + "-"+ ( this.doHeader.orderdetails.length +1 );
    this.doHeader.orderdetails.push(containerDetails);

    this.rowRefresh();
  }
  rowRefresh() {
    this.OrderDetailKey = "";
    this.ContainerNo = undefined;
    this.ContainerSize = undefined;
    this.ContainerSizeDesc = undefined;
    this.Chassis = undefined;
    this.SealNo = undefined;
    this.Weight = undefined;
    this.Comments = undefined;
    this.CommentItems = [];

    this.hazard = undefined;
    this.trixale = undefined;
    this.overweight = undefined;
    this.needstobescaled = undefined;

    this.IsHazardChecked = false;
    this.IsOverweightChecked = false;
    this.IsTrixaleChecked = false;
    this.IsNeedstobescaledChecked = false;
  }

  drpcontainersizeChanged(value: any) {
    this.ContainerSizeDesc = this.containersizelist.find(
      x => x.containersize == value
    ).description;
  }
  edit(details: Order_details, index: number) {
    this.rowindex = index;
    this.ContainerNo = details.ContainerNo;
    this.OrderDetailKey = details.OrderDetailKey;
    this.ContainerSize = details.ContainerSize;
    this.ContainerSizeDesc = details.ContainerSizeDesc;
    this.Chassis = details.Chassis;
    this.SealNo = details.SealNo;
    this.Weight = details.Weight;

    var comments_array = details.Comments.split(",");
    this.CommentItems = comments_array;

    for (var i = 0; i < comments_array.length; i++) {
      // Trim the excess whitespace.
      comments_array[i] = comments_array[i]
        .replace(/^\s*/, "")
        .replace(/\s*$/, "");

      if (comments_array[i] == "Hazard") {
        this.IsHazardChecked = true;
      } else if (comments_array[i] == "Over Weight") {
        this.IsOverweightChecked = true;
      } else if (comments_array[i] == "Triaxle") {
        this.IsTrixaleChecked = true;
      } else if (comments_array[i] == "Needs to be scaled") {
        this.IsNeedstobescaledChecked = true;
      }
    }
  }
  
  onCompleteItem($event) {
    console.log($event);
  //  alert("Upload Complete");
  }
}
