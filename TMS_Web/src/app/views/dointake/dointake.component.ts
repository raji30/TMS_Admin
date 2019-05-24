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
import { FileSelectDirective, FileDropDirective,FileUploader } from 'ng2-file-upload';
import { Server } from "tls";

 const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';
//const URL = 'http://localhost:4200/api';
 
@Component({
  selector: "app-dointake",
  templateUrl: "./dointake.component.html",
  styleUrls: ["./dointake.component.scss"]
})
export class DOIntakeComponent implements OnInit, OnChanges,OnDestroy {
  subscription: Subscription;
  bsConfig: Partial<BsDatepickerConfig>;
 
  @Input() orderKeyinput: string;
  broker: Broker[];
  brokerName: string = "Select Broker";
  public doHeader: DeliveryOrderHeader;
  isContainerAttributeVisible: boolean = true;
  isNewDeliveryOrder: boolean = true;

  orderNo: string;
  errorMessage: string;
  orderKey: string;
  selectedBillToKey = "";

  percentDone: number;
  uploadSuccess: boolean;

    uploader = new FileUploader({url: URL });
  
  public hasBaseDropZoneOver:boolean = false;
  public hasAnotherDropZoneOver:boolean = false;

  constructor(  private http: HttpClient,
    private service: DeliveryOrderService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.doHeader= null;
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

  ngOnInit(): void {
    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" }
    );

    // this.bsConfig.containerClass = 'theme-orange';
    // this.bsConfig.dateInputFormat = 'MM/DD/YYYY';
    this.doHeader = null;
    this.doHeader = new DeliveryOrderHeader();
    //this.orderNo = this.route.snapshot.paramMap.get("order");
    this.orderNo = this.orderKeyinput;
    if (this.orderNo != undefined) {
      this.isContainerAttributeVisible = false;
      this.isNewDeliveryOrder = false;

      //getting order info from the DB..
      this.service.GetbyKey(this.orderNo).subscribe(data => {
        (this.doHeader = data),
          error => console.log(error),
          () =>
            console.log(
              "Get Order Detail BillToAddress",
              this.doHeader.BillToAddress
            );
      });

      this.service
        .GetOrderDetailsbyKey(this.orderNo)
        .subscribe(
          data => (this.doHeader.orderdetails = data),
          error => console.log(error),
          () => console.log("Get OrderDetail", this.doHeader.orderdetails)
        );
    } else {
    }
  }

  OnSubmit(form) {
    if (this.isNewDeliveryOrder) {
       this.service
        .saveDOHeader(form.value)
        .subscribe(
          result =>{ (this.orderKey = result)
            if(this.orderKey!=undefined && this.orderKey!="")
            {
            this.saveDeliveryDetails();
            }
          },
          error => (this.errorMessage = error)
        );

     //applying orderkey to order details
     
    } else {
    }
  }

  private saveDeliveryDetails() {

    for (let order of this.doHeader.orderdetails) {
      order.OrderKey = this.orderKey;
    }
    this.delay(300);
    this.service
      .saveOrderDetails(this.doHeader.orderdetails)
      .subscribe(results => results, error => (this.errorMessage = error));
    this.doHeader = null;
  }

  clear() {
    this.doHeader.BrokerRefNo = undefined;
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  ngOnChanges(changes: SimpleChanges) {
    let newFocusedChallenge = changes["orderKeyinput"].currentValue;

    this.bsConfig = Object.assign(
      {},
      { containerClass: "theme-orange" },
      { dateInputFormat: "MM/DD/YYYY" }
    );

    // this.bsConfig.containerClass = 'theme-orange';
    // this.bsConfig.dateInputFormat = 'MM/DD/YYYY';
    this.doHeader = null;
    this.doHeader = new DeliveryOrderHeader();
    //this.orderNo = this.route.snapshot.paramMap.get("order");
    this.orderNo = this.orderKeyinput;
    if (this.orderNo != undefined) {
      this.isContainerAttributeVisible = false;
      this.isNewDeliveryOrder = false;

      //getting order info from the DB..
      this.service.GetbyKey(this.orderNo).subscribe(data => {
        (this.doHeader = data),
          error => console.log(error),
          () =>
            console.log(
              "Get Order Detail BillToAddress",
              this.doHeader.BillToAddress
            );
      });

      this.service
        .GetOrderDetailsbyKey(this.orderNo)
        .subscribe(
          data => (this.doHeader.orderdetails = data),
          error => console.log(error),
          () => console.log("Get OrderDetail", this.doHeader.orderdetails)
        );
    } else {
    }
  }
  uploadAndProgress(files: File[]){
    console.log(files)
    var formData = new FormData();
    Array.from(files).forEach(f => formData.append('file',f))
    
    this.http.post('http://localhost:4200/api/', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.percentDone = Math.round(100 * event.loaded / event.total);
        } else if (event instanceof HttpResponse) {
          this.uploadSuccess = true;
        }
    });
  }

  public fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }
 
  public fileOverAnother(e:any):void {
    this.hasAnotherDropZoneOver = e;
  }

  ngOnDestroy() {
    this.subscription && this.subscription.unsubscribe();
  }
}
