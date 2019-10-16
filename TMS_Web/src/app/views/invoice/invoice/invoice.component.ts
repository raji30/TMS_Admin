import { Component, OnInit } from "@angular/core";
import { DeliveryOrderHeader } from "../../../_models/DeliveryOrderHeader";
import { Order_details } from "../../../_models/order_details";
import { InvoiceService } from "../../../_services/invoice.service";
import { Invoicemodel } from "../../../_models/invoicemodel";
import { Invoice } from "./../../../_models/invoice";
import { Address } from "./../../../_models/address";
import { Item } from "../../../_models/item";
import { ItemService } from "../../../_services/item.service";
import { Invoicedetails } from "../../../_models/invoicedetails";
import { forEach } from "@angular/router/src/utils/collection";

@Component({
  selector: "app-invoice",
  templateUrl: "./invoice.component.html",
  styleUrls: ["./invoice.component.scss"]
})
export class InvoiceComponent implements OnInit {
  Itemlist: Item[];

  Data: Invoicemodel[];
  public invoiceModel: Invoicemodel;
  public itemkey: string;

  // invoice input data
  public InvoiceMaxcount: number = 0;
  public invoiceHeader: Invoice;  
  public invoiceDetails: Invoicedetails[];

  public invoiceHeaderResult: Invoice;  

  public InvoiceKey: any;
  public InvoiceNo: any;
  public invoiceDate: any;
  public CustKey: any;
  public BilltoAddrKey: any;
  public dueDate: any;
  public InvoiceAmt: any;
  public drpCharge: number = 0;

  public invoiceDetail: Array<any> = [];
  public invoiceItem: any = {};
  private total: number = 0;

  public showInvoice:boolean=false;
  public showImage:boolean=true;
  

  constructor(
    private invoiceService: InvoiceService,
    private itemService: ItemService
  ) {}

  ngOnInit() {
    this.showImage =true;
    this.invoiceModel = new Invoicemodel();
    this.invoiceModel.invoice = new Invoice();

    this.itemService
      .GetItems()
      .subscribe(
        data => (this.Itemlist = data),
        error => console.log(error),
        () => console.log("Get Itemlist", this.Itemlist)
      );

    this.invoiceService.GetOrderstoGenerateInvoice().subscribe(
      data => {
        this.Data = data; 
      },
      error => console.log(error),
      () => console.log("Get Data", this.Data)
    );
  }

  getOrderdata(orderKey: string) {
    this.invoiceService.GetInvoiceMaxcount().subscribe(
      data => {
        this.InvoiceMaxcount = data;
        this.InvoiceNo = this.InvoiceMaxcount + 1;
      },
      error => console.log(error),
      () => console.log("Get InvoiceMaxcount", this.InvoiceMaxcount)
    );

    this.invoiceModel = this.Data.find(x => x.order.OrderKey == orderKey);
    this.showInvoice = true;
    this.showImage =false;
    console.log("Get invoiceModel", this.invoiceModel);
    console.log("Get invoiceModel", this.invoiceModel.BillFrom["Name"]);
  }

  addFieldValue() {
    console.log("Insert:", this.invoiceItem);

    for (var item of this.invoiceDetail) {
      if (item.Itemkey == this.itemkey) {
        return;
      }
    }

    var itemData = this.Itemlist.find(key => key.itemkey == this.itemkey);
    
    this.invoiceItem.Itemkey = itemData.itemkey;
    this.invoiceItem.Description = itemData.description;
    this.invoiceItem.Quantity = "2.9";
    this.invoiceItem.Price = (+itemData.unitprice * this.invoiceItem.Quantity
    ).toFixed(2);
    this.invoiceItem.UnitPrice = (+itemData.unitprice).toFixed(2);
    this.invoiceItem.InvoiceLineKey = itemData.itemkey;
    this.invoiceItem.InvoiceKey = itemData.itemkey;
    this.invoiceItem.ItemType =1;
    this.invoiceItem.InvoiceDescription ="";
    this.invoiceItem.ExcessAmount ="";

    var itemdetails = this.invoiceItem;
    this.invoiceDetail.push(itemdetails);
    this.invoiceItem = {};
    this.InvoiceAmt = "";
    this.total = 0;

    for (var item of this.invoiceDetail) {
      var val2 = +item.Price;
      this.total = this.addNumbers(this.total, val2);
    }
    console.log("Insert:", this.total);
    this.InvoiceAmt =  this.total.toFixed(2);

    this.drpCharge = 0;
  }

  deleteFieldValue(index) {
    if (this.invoiceDetail.length == 1) {
      // this.toastr.error("Can't delete the row when there is only one row", 'Warning');
      return false;
    } else {
      this.invoiceDetail.splice(index, 1);
      return true;
    }
  }

  addNumbers(a: number, b: number) {
    return a + b;
  }

  drpAddnewCharge_ChangedEvent(itemkey: string) {
    this.itemkey = itemkey;
  }

  createInvoice() {
    if (this.dueDate == undefined) {
      alert("Please enter a due date.");
      return;
    }
    if (this.invoiceDetail.length < 1) {
      alert("Please add invoice deatils.");
      return;
    }
//"4f8bbc20-8dd4-11e9-921e-2b0d17e7b3ca"
    //adding header data to invoiceHeader
    this.invoiceHeader = new Invoice();
    this.invoiceHeader.InvoiceNo = this.InvoiceNo;
    this.invoiceHeader.InvoiceDate = this.invoiceDate;
    this.invoiceHeader.InvoiceAmt = this.InvoiceAmt;
    this.invoiceHeader.CustKey = this.invoiceModel.BillFrom.AddrKey;
    this.invoiceHeader.BilltoAddrKey = this.invoiceModel.BillTo.AddrKey;
    this.invoiceHeader.BilltoAddrCopy = this.invoiceModel.BillTo.AddrKey;
    this.invoiceHeader.DueDate = this.dueDate;
    this.invoiceHeader.InvoiceType = 1;
    this.invoiceHeader.OrderDetailKey = "4f8bbc20-8dd4-11e9-921e-2b0d17e7b3ca" ;//this.invoiceModel.order.orderdetails["OrderDetailKey"].OrderDetailKey;
    console.log("this.invoiceDetail",this.invoiceDetail);
    

    this.invoiceService.CreateInvoiceHeader(this.invoiceHeader).subscribe(
      result => {
        this.invoiceHeaderResult = result;
        if (this.invoiceHeaderResult.Invoicekey != undefined && this.invoiceHeaderResult.Invoicekey != "") {
          for (let i = 0; i < this.invoiceDetail.length; i++) {
            this.invoiceDetail[i].InvoiceKey = this.invoiceHeaderResult.Invoicekey;
          }
          this.invoiceHeader.invoicedetails = this.invoiceDetail;
          console.log("this.invoiceDetail", this.invoiceHeader.invoicedetails);console.log("this.invoiceDetails",this.invoiceDetails);
          this.invoiceService.CreateInvoiceDetail(this.invoiceHeader.invoicedetails).subscribe(res=>{res;});
        }
      },
      error => {
        //this.errorMessage = error;
        // this.showError(this.errorMessage, "New-Order");
      }
    );
  }
}
