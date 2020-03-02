import { DeliveryOrderHeader } from "../../../_models/DeliveryOrderHeader";
import { Order_details } from "../../../_models/order_details";
import { InvoiceService } from "../../../_services/invoice.service";
import { Invoicemodel } from "../../../_models/invoicemodel";
import { Invoice } from "./../../../_models/invoice";
import { Address } from "./../../../_models/address";
import { Item } from "../../../_models/item";
import { ItemService } from "../../../_services/item.service";
import { Invoicedetails } from "../../../_models/invoicedetails";
import { OnInit, Component } from "@angular/core";
import { Rate } from "../../../_models/rate";
import { ToastrService } from "ngx-toastr";
import { saveAs } from "file-saver";

@Component({
  selector: "app-invoice",
  templateUrl: "./invoice.component.html",
  styleUrls: ["./invoice.component.scss"]
})
export class InvoiceComponent implements OnInit {
  Itemlist: Item[];
  BillToAddress: Address;

  public Data: Invoicemodel[];
  public invoiceList: Invoicemodel[];
  public invoiceModel: Invoicemodel;
  public rate: Array<Rate> = [];
  public itemkey: string;
  public searchText: string;

  // invoice input data
  public InvoiceMaxcount: number = 0;
  public invoiceHeader: Invoice;
  public invoiceHeaderList: Invoice[];
  public invoiceDetails: Invoicedetails[];
  public invoiceDet: Invoicedetails;

  public invoiceHeaderResult: Invoice;
  public OrderKey: string;
  public OrderNo: string;  
  public InvoiceKey: string;
  public InvoiceNo: number;
  public invoiceDate: Date;
  public CustKey: string;
  public BilltoAddrKey: string;
  public dueDate: Date;
  public InvoiceAmt: number;
  public drpCharge: number = 0;
  public rowIndex = -1;

  public invoiceDetail: Array<any> = [];
  public invoiceItem: any = {};
  private total: number = 0;
  private ItemRate: number = 0;
  private ContainerQuantity: number = 0;

  public showInvoice: boolean = false;
  public showImage: boolean = true;
  public showAddUpdateDiv: boolean = true;
  public item_editing: boolean = false;
  public showInvoiceList: boolean = true;
  public lblrowaddupdate: string;
  public lblInvoice: string;

  isDesc: boolean = false;
  column: string = "InvoiceNo";
  p: number = 1;
  count: number;

  constructor(
    private invoiceService: InvoiceService,
    private itemService: ItemService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.showImage = true;
    this.showAddUpdateDiv = false;
    this.invoiceModel = new Invoicemodel();
    this.invoiceModel.invoice = new Invoice();
    this.rate = new Array<Rate>();

    this.itemService.GetItems().subscribe(
      data => (this.Itemlist = data),
      error => console.log(error),
      () => console.log("Get Itemlist", this.Itemlist)
    );

    this.load_NewInvoiceList();
    this.load_InvoiceList();
  }

  load_InvoiceList() {
    this.invoiceService.getInvoiceHeaderList().subscribe(
      data => {
        this.invoiceHeaderList = data;

        if (this.invoiceHeaderList.length > 0) {
          this.showImage = false;
          this.showInvoiceList = true;
        }
      },
      error => console.log(error),
      () => console.log("Get invoiceHeaderList", this.invoiceHeaderList)
    );
  }
  load_NewInvoiceList() {
    this.invoiceService.GetOrderstoGenerateInvoice().subscribe(
      data => {
        this.Data = data;
      },
      error => console.log(error),
      () => console.log("Get Data", this.Data)
    );
  }

  getOrderdata(orderKey: string) {
    this.invoiceDet = null;
    this.invoiceDetail = null;
    this.invoiceDetail = new Array<Invoicedetails>();

    this.invoiceService.GetInvoiceMaxcount().subscribe(
      data => {
        this.InvoiceMaxcount = data;
        this.InvoiceNo = this.InvoiceMaxcount + 1;
      },
      error => console.log(error),
      () => console.log("Get InvoiceMaxcount", this.InvoiceMaxcount)
    );

    // this.invoiceModel = this.Data.find(x => x.order.OrderKey == orderKey);
    this.invoiceService.getOrderDatabyKey(orderKey).subscribe(
      data => {
        this.invoiceModel = data;
        console.log("Get invoice Model", this.invoiceModel);
      },
      error => {
        console.log(error);
      }
    );
    this.invoiceService.getorderratesbykey(orderKey).subscribe(
      data => {
        this.rate = data;
        for (var item of this.rate) {
          var count = 0;
          if (this.invoiceDetail.length == 0) {
            this.invoiceDet = new Invoicedetails();
            this.invoiceDet.Itemkey = item.itemkey;
            this.invoiceDet.Description = item.description;
            this.invoiceDet.Quantity = 1;
            this.invoiceDet.UnitPrice = item.unitprice;
            this.invoiceDet.Price = this.invoiceDet.UnitPrice;
            this.invoiceDet.InvoiceLineKey = undefined;
            this.invoiceDet.InvoiceKey = undefined;
            this.invoiceDet.InvoiceDescription = "";
            this.invoiceDet.ExcessAmount = "";
            this.invoiceDet.containerno = item.containerno;
            this.invoiceDetail.push(this.invoiceDet);
            console.log("this.invoiceDetail", this.invoiceDetail);
          } else {
            for (var items of this.invoiceDetail) {
              if (items.Itemkey == item.itemkey) {
                this.invoiceDetail[count].containerno =  this.invoiceDetail[count].containerno + ','+ items.containerno;
                this.invoiceDetail[count].Quantity = items.Quantity + 1;
                this.invoiceDetail[count].Price =
                  this.invoiceDetail[count].Quantity * items.UnitPrice;
              } else {
                count = count + 1;
              }
            }

            if (count == this.invoiceDetail.length) {
              this.invoiceDet = new Invoicedetails();
              this.invoiceDet.Itemkey = item.itemkey;
              this.invoiceDet.Description = item.description;
              this.invoiceDet.Quantity = 1;
              this.invoiceDet.containerno = item.containerno;
              this.invoiceDet.UnitPrice = item.unitprice;
              this.invoiceDet.Price = this.invoiceDet.UnitPrice;
              this.invoiceDet.InvoiceLineKey = undefined;
              this.invoiceDet.InvoiceKey = undefined;
              this.invoiceDet.InvoiceDescription = "";
              this.invoiceDet.ExcessAmount = "";
              this.invoiceDetail.push(this.invoiceDet);
              console.log("this.invoiceDetail", this.invoiceDetail);
              count = 0;
            }
          }
        }

        this.InvoiceAmt = 0;
        this.total = 0;

        for (var items of this.invoiceDetail) {
          var val2 = +items.Price;
          this.total = this.addNumbers(this.total, val2);
        }

        this.InvoiceAmt = +this.total.toFixed(2);
        this.drpCharge = 0;
      },
      error => {
        console.log(error);
      }
    );

    this.showInvoice = true;
    this.showImage = false;
    this.showInvoiceList = false;
    this.lblInvoice = "Create Invoice";
    console.log("Get invoiceModel", this.invoiceModel);
  }

  load_invoiceHeaderandDetailData(model: Invoice) {
    this.showInvoice = true;
    this.showImage = false;
    this.showInvoiceList = false;
    this.lblInvoice = "Update Invoice";

    this.invoiceModel = new Invoicemodel();
    this.invoiceModel.order = new DeliveryOrderHeader();
    this.invoiceModel.order.OrderKey = model.OrderKey;
    this.OrderKey = model.OrderKey;
    this.invoiceModel.order.BillToAddressBO = new Address();
    this.invoiceModel.order.SourceAddressBO = new Address();
    this.invoiceModel.order.DestinationAddressBO = new Address();
    this.BillToAddress = new Address();

    this.invoiceDet = null;
    this.invoiceDet = new Invoicedetails();
    this.InvoiceKey = model.Invoicekey;
    this.InvoiceNo = model.InvoiceNo;
    this.invoiceDate = model.InvoiceDate;
    this.dueDate = model.DueDate;
    this.InvoiceAmt = +model.InvoiceAmt.toFixed(2);

    this.invoiceService.getOrderDatabyKey(model.OrderKey).subscribe(
      data => {
        this.invoiceModel = data;
        this.OrderNo = this.invoiceModel.order.OrderNo;
        console.log("Edit for Invoice:  -- ", this.invoiceModel);
        console.log("OrderNo:  -- ", this.invoiceModel.order.OrderNo);
      },
      error => {
        console.log(error);
      }
    );

    this.invoiceService.getinvoicedetail(model.Invoicekey).subscribe(
      data => {
        this.invoiceDetail = data;
        console.log("Edit for invoice Detail:  -- ", this.invoiceDetail);
      },
      error => {
        console.log(error);
      }
    );    
    this.BillToAddress = this.invoiceModel.order.BillToAddressBO;
    console.log(
      " this.BillToAddress ",
      this.invoiceModel.order.BillToAddressBO
    );
  }

  editItem(data: any, index: number) {
    this.rowIndex = index;
    this.item_editing = true;
    this.drpCharge = data.Itemkey;
    this.ContainerQuantity = data.Quantity;
    this.ItemRate = data.UnitPrice;
    this.showAddUpdateDiv = true;
    this.lblrowaddupdate = "Edit Item";
  }
  CancelCreateInvoice() {
    this.showInvoice = false;
    this.showImage = false;
    this.showInvoiceList = true;
    this.invoiceDet = null;
  }

  ClearRow() {
    this.item_editing = false;
    this.rowIndex = -1;
    this.drpCharge = 0;
    this.showAddUpdateDiv = false;
    this.drpCharge = undefined;
    this.ContainerQuantity = undefined;
    this.ItemRate = undefined;
  }

  addupdateItem() {
    if (this.drpCharge == 0) {
      this.showInfo("Item must be selected!", "Rate");
      return;
    }
    if (this.ItemRate == 0) {
      this.showInfo("Rate must be entered!", "Rate");
      return;
    }
    if (this.ContainerQuantity == 0) {
      this.showInfo("Quantity must be entered!", "Rate");
      return;
    }
    if (this.ContainerQuantity > this.invoiceModel.containers.length) {
      this.showInfo("Quantity must be within container no(s)", "Rate");
      return;
    }

    if (this.item_editing) {
      this.invoiceDetail[this.rowIndex].Quantity = this.ContainerQuantity;
      this.invoiceDetail[this.rowIndex].unitprice = this.ItemRate;
      this.invoiceDetail[this.rowIndex].Price =
        this.ContainerQuantity * this.ItemRate;

      this.item_editing = false;
      this.rowIndex = -1;
      this.drpCharge = 0;
      this.drpCharge = undefined;
      this.ContainerQuantity = undefined;
      this.showAddUpdateDiv = false;
      return;
    } else {
      for (var item of this.invoiceDetail) {
        if (item.Itemkey == this.itemkey) {
          this.showInfo("Item already added!", "Rate");
          return;
        }
      }

      var itemData = this.Itemlist.find(key => key.itemkey == this.itemkey);
      //this.ItemRate = +(+itemData.unitprice).toFixed(2);

      this.invoiceItem.Itemkey = itemData.itemkey;
      this.invoiceItem.Description = itemData.description;
      this.invoiceItem.Quantity = this.ContainerQuantity;
      this.invoiceItem.Price = (
        this.ItemRate * this.invoiceItem.Quantity
      ).toFixed(2);
      this.invoiceItem.UnitPrice = this.ItemRate;
      this.invoiceItem.InvoiceKey = this.InvoiceKey;
      var itemdetails = this.invoiceItem;
      this.invoiceDetail.push(itemdetails);

      this.drpCharge = undefined;
      this.ContainerQuantity = undefined;
      this.ItemRate = 0;
      this.invoiceItem = {};
      this.InvoiceAmt = 0;
      this.total = 0;
      this.showAddUpdateDiv = false;
    }

    for (var item of this.invoiceDetail) {
      var val2 = +item.Price;
      this.total = this.addNumbers(this.total, val2);
    }
    this.InvoiceAmt = +this.total.toFixed(2);
  }
  showChargeDiv() {
    this.showAddUpdateDiv = true;
    this.lblrowaddupdate = "Add Item";
  }

  deleteItem(index) {
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
    for (var item of this.invoiceDetail) {
      if (item.Itemkey == itemkey) {
        this.showInfo("Item already added!", "Rate");
        return;
      }
    }

    this.itemkey = itemkey;
    var itemData = this.Itemlist.find(key => key.itemkey == itemkey);
    this.ContainerQuantity = 1;
    this.ItemRate = +(+itemData.unitprice).toFixed(2);
  }

  submit() {
    if (this.dueDate == undefined) {
      alert("Please enter a due date.");
      return;
    }
    if (this.invoiceDetail.length < 1) {
      alert("Please add invoice deatils.");
      return;
    }

    if (this.lblInvoice == "Create Invoice") {
      this.createInvoice();
    }
    if (this.lblInvoice == "Update Invoice") {
      this.updateInvoice();
    }
  }
  updateInvoice() {
    this.invoiceHeader = new Invoice();
    this.invoiceHeader.Invoicekey = this.InvoiceKey;
    this.invoiceHeader.InvoiceNo = this.InvoiceNo;
    this.invoiceHeader.InvoiceDate = this.invoiceDate;
    this.invoiceHeader.InvoiceAmt = +this.InvoiceAmt.toFixed(2);
    this.invoiceHeader.DueDate = this.dueDate;

    this.invoiceService.UpdateInvoiceHeader(this.invoiceHeader).subscribe(
      result => {},
      error => {
        this.showError(error, "Invoice Update");
        return;
      }
    );

    this.invoiceService.UpdateInvoiceDetail(this.invoiceDetail).subscribe(
      res => {
        if (res) {
          this.load_NewInvoiceList();
          this.load_InvoiceList();
          this.showInvoice = false;
          this.showSuccess("Invoice updated successfully", "Invoice");
        }
      },
      error => {
        this.showError(error, "Invoice Update");
        return;
      }
    );
  }

  createInvoice() {
    //adding header data to invoiceHeader
    this.invoiceHeader = new Invoice();
    this.invoiceHeader.OrderKey = this.invoiceModel.order.OrderKey;
    this.invoiceHeader.InvoiceNo = this.InvoiceNo;
    this.invoiceHeader.InvoiceDate = this.invoiceDate;
    this.invoiceHeader.InvoiceAmt = +this.InvoiceAmt.toFixed(2);
    this.invoiceHeader.CustKey = this.invoiceModel.order.CustKey;
    this.invoiceHeader.BilltoAddrKey = this.invoiceModel.order.BillToAddress;
    this.invoiceHeader.DueDate = this.dueDate;

    this.invoiceService.CreateInvoiceHeader(this.invoiceHeader).subscribe(
      result => {
        this.invoiceHeaderResult = result;
        if (
          this.invoiceHeaderResult.Invoicekey != undefined &&
          this.invoiceHeaderResult.Invoicekey != ""
        ) {
          for (let i = 0; i < this.invoiceDetail.length; i++) {
            this.invoiceDetail[
              i
            ].InvoiceKey = this.invoiceHeaderResult.Invoicekey;
          }
          this.invoiceHeader.invoicedetails = this.invoiceDetail;
          this.invoiceService
            .CreateInvoiceDetail(this.invoiceHeader.invoicedetails)
            .subscribe(res => {
              if (res) {
                this.load_NewInvoiceList();
                this.load_InvoiceList();
                this.showInvoice = false;
                this.showSuccess("Invoice Created successfully", "Invoice");
              }
            });            
        }
      },
      error => {
        this.showError(error, "New-Order");
      }
    );
  }

  downloadPDF() {
    this.invoiceService.downloadInvoice(this.OrderNo).subscribe(
      result => {
        if (confirm("Are you sure to download the file? ")) {
          saveAs(result, this.OrderNo+ '.pdf');
      }},
      error => {
        this.showError(error, "Server error while downloading file.");
        return;
      }
    );    
  }

  
  createPDF() {
    this.invoiceService.createPDF(this.InvoiceKey).subscribe(
      result => {
        this.showSuccess("Invoice PDF Created successfully", "Invoice PDF");
      },
      error => {
        this.showError(error, "PDF Creation");
        return;
      }
    );
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }
  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 1000, closeButton: true });
  }

  sort(column) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = column;
    let direction = this.isDesc ? 1 : -1;

    this.invoiceHeaderList = [...this.invoiceHeaderList].sort((n1, n2) => {
      if (this.column == "InvoiceNo") {
        if (n1.InvoiceNo > n2.InvoiceNo) {
          return 1 * direction;
        } else if (n1.InvoiceNo < n2.InvoiceNo) {
          return -1 * direction;
        } else return 0;
      }

      if (this.column == "CustName") {
        if (n1.CustName > n2.CustName) {
          return 1 * direction;
        } else if (n1.CustName < n2.CustName) {
          return -1 * direction;
        } else return 0;
      }
      if (this.column == "InvoiceAmt") {
        if (n1.InvoiceAmt > n2.InvoiceAmt) {
          return 1 * direction;
        } else if (n1.InvoiceAmt < n2.InvoiceAmt) {
          return -1 * direction;
        } else return 0;
      }

      if (this.column == "DueDate") {
        if (n1.DueDate > n2.DueDate) {
          return 1 * direction;
        } else if (n1.DueDate < n2.DueDate) {
          return -1 * direction;
        } else return 0;
      }
      if (this.column == "StatusDesc") {
        if (n1.StatusDesc > n2.StatusDesc) {
          return 1 * direction;
        } else if (n1.StatusDesc < n2.StatusDesc) {
          return -1 * direction;
        } else return 0;
      }
    });
  }
}
