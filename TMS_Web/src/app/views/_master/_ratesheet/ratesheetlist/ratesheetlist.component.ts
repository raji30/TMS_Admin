import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Item } from "../../../../_models/item";
import { Ratesheet } from "../../../../_models/ratesheet";
import { Customer } from "../../../../_models/customer";
import { RateService } from "../../../../_services/rate.service";
import { CustomerService } from "../../../../_services/customer.service";
import { ItemService } from "../../../../_services/item.service";
//import { nlLocale } from "ngx-bootstrap";

@Component({
  selector: "app-ratesheetlist",
  templateUrl: "./ratesheetlist.component.html",
  styleUrls: ["./ratesheetlist.component.scss"]
})
export class RatesheetlistComponent implements OnInit {
  ratesheet: Array<Item> = [];

  private newAttribute: any = {};
  private newAttributeinRate: any = {};
  item: Item[] = [];

  public newItem: any = {};
  public dataModel: Ratesheet[];
  public addModel: Ratesheet;
  public rates: Ratesheet[];
  public ratesbycustomer: Ratesheet[];
  customers: Customer[];
  CustomerKey: string;
  selectedCustomer: any;

  show_DivAddUpdate: boolean = false;
  show_DivInfo: boolean = false;
  show_btnAdd: boolean = false;
  show_btnEdit: boolean = false;
  show_lblAdd: boolean = false;
  show_lblEdit: boolean = false;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  ratesCount: boolean = true;
  itemKey: string;

  isDesc: boolean = false;
  column: string = "CustId";
  p: number = 1;
  count: number;

  constructor(
    private rateService: RateService,
    private itmService: ItemService,
    private customerService: CustomerService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getItems();
    this.loadAllCustomerRates();
    this.loadAllCustomers();
  }

  loadAllCustomers() {
    this.customerService.getCustomers().subscribe(
      data => (this.customers = data),
      error => console.log(error),
      () => console.log("Get customers complete", this.customers)
    );
  }

  loadAllCustomerRates() {
    this.rateService.GetRates().subscribe(
      data => (this.rates = data),
      error => {
        this.showError("Error in getting All Items ", "Error");
      },
      () => console.log("Rate list", this.rates)
    );
  }
  getItems() {
    this.itmService.GetItems().subscribe(
      data => (this.item = data),
      error => {
        this.showError("Error in getting All Items ", "Error");
      },
      () => console.log("Items list", this.ratesheet)
    );
  }
  GetRateByCustomer() {
    if (this.CustomerKey == null || this.CustomerKey == undefined) {
      return;
    }
    this.rateService.GetRateByCustomer(this.CustomerKey).subscribe(
      _ratesByCustomer => {
        this.dataModel = _ratesByCustomer;      

        this.show_DivInfo = true;
        this.show_DivAddUpdate = false;
        console.log("user by Id", this.dataModel);
      },
      error => {
        this.showError("Error in getting user ", "Error");
      }
    );
    console.log("Rates By Customer", this.ratesbycustomer);
    //this.dataModel = this.ratesbycustomer
  }

  onSubmit() {
    console.log("On Submit Data", this.item);
    console.log("Ratesheet list", this.ratesheet);

    if (this.addModel.customerkey == "0") {
      this.showError("Please select Customer!", "Customer");
      return;
    }
    if (this.ratesheet.length == 0) {
      this.showError("No Rate-Items found to add!", "Customer");
      return;
    }
    //this.dataModel.item = new Item();
    //this.dataModel.item=  Object.assign({}, ...this.ratesheet);
    this.addModel.customerkey = this.CustomerKey;
    this.addModel.item = this.ratesheet;

    if (this.itemKey == null) {
      this.rateService.AddRate(this.addModel).subscribe(
        () => {
          this.showSuccess("created successfully", "Create");
          this.loadAllCustomerRates();
          this.itemKey = null;
          this.show_DivAddUpdate = false;
          this.show_lblAdd = false;
        },
        error => {
          this.showError("Error in Creation", "Error");
        }
      );
    } else {
      this.rateService.UpdateRate(this.dataModel).subscribe(
        () => {
          this.showSuccess("Updated successfully", "Update");
          this.loadAllCustomerRates();
          this.itemKey = null;
          this.show_DivAddUpdate = false;
          this.show_lblEdit = false;
        },
        error => {
          this.showError("Error in Update", "Error");
        }
      );
    }
  }

  bindFormControls() {
    this.show_DivAddUpdate = true;
    this.show_DivInfo = false;
    this.show_btnAdd = false;
    this.show_lblAdd = false;
    this.show_lblEdit = true;
  }

  resetForm() {
    this.dataModel = null;
    this.addModel = new Ratesheet();
    this.itemKey = null;
  }

  toggle() {
    this.show_DivAddUpdate = true;
    this.isResetbtnhidden = true;
    this.show_btnAdd = false;
    this.show_lblAdd = true;
    this.show_lblEdit = false;
    this.show_DivInfo = false;
    this.resetForm();
    this.addModel.customerkey = "0";
    this.ratesheet = null;
    this.ratesheet =new  Array<Item>();
  }

  cancel() {
    this.isResetbtnhidden = false;
    if (this.dataModel != null) {
      this.show_DivInfo = false;
    }
    this.show_DivAddUpdate = false;
    this.show_lblAdd = false;
    this.show_lblEdit = false;
  }

  addFieldValue() {
    if (Object.keys(this.newAttribute).length === 0) {
      return;
    }

    for (var item of this.ratesheet) {
      if (item.itemkey ==this.newAttribute.itemkey) {
        this.showInfo("Add Rate", "Rate already fixed.");
        return;
      }
    }

    this.newItem = this.newAttribute;
    this.ratesheet.push(this.newItem);
    this.newAttribute = {};
  }

  addNewRowinRateSheet() {
    if (Object.keys(this.newAttributeinRate).length === 0) {
      return;
    }
    if (
      this.newAttributeinRate.unitprice === undefined ||
      this.newAttributeinRate.unitprice === null
    ) {
      this.showInfo("Add Rate", "Enter Unit Price.");
      return;
    }
    for (var data of this.dataModel) {
      if (data.itemkey == this.itemKey) {
        this.showInfo("Add Rate", "Rate already available.");
        return;
      }
    }
    this.newItem = this.newAttributeinRate;
    this.newItem.ratekey = null;
    this.newItem.customerkey = this.CustomerKey;
    this.newItem.createdate = null;
    this.newItem.userkey = null;
    this.newItem.customername = null;
    this.newItem.Item = null;
    this.newItem.lastupdatedate = null;

    this.dataModel.push(this.newItem);
    this.newAttributeinRate = {};
  }
  deleteFieldValue(index) {
    this.ratesheet.splice(index, 1);
  }
  deleteFieldValueInRateSheet(index) {
    this.dataModel.splice(index, 1);
  }
  drpAddnewCharge_ChangedEvent(val: any) {
    this.itemKey = val;
    this.newItem = this.item.find(x => x.itemkey == val);
    this.newAttribute.description = this.newItem.description;
    this.itemKey = null;
  }

  drpAddnewCharge_ChangedEventEdit(val: any) {
    this.itemKey = val;
    this.newItem = this.item.find(x => x.itemkey == val);
    this.newAttributeinRate.description = this.newItem.description;
    //this.itemKey = null;
  }

  drpCustomer_ChangedEvent(CustomerKey: any) {
    // var test= this.rates.filter(x => x.customerkey == val);
    // this.ratesbycustomer = new Array<Ratesheet>();
    // this.ratesbycustomer.push(test);
    // console.log("Rates By Customer", this.ratesbycustomer);
    console.log("Rates By Customer", this.selectedCustomer);
    this.CustomerKey = CustomerKey;
    this.selectedCustomer = this.customers.find(
      x => x.CustomerKey == CustomerKey
    );
    this.ratesbycustomer = this.rates.filter(x => x.customerkey == CustomerKey);
    if (this.ratesbycustomer.length > 0) {
      this.show_btnEdit = true;
      this.ratesCount = false;
      this.show_btnAdd = false;
    } else {
      this.show_btnEdit = false;
      this.ratesCount = false;
      this.show_btnAdd = true;
    }

    console.log("Rates By Customer", this.ratesbycustomer);
  }

  updateRate() {
    this.rateService.UpdateRate(this.dataModel).subscribe(
      () => {
        this.showSuccess("Updated successfully", "Update");
        this.loadAllCustomerRates();
        this.show_DivInfo = false;
        this.dataModel = null;
        this.CustomerKey = null;
        this.selectedCustomer = null;
        this.itemKey = null;
        this.show_DivAddUpdate = false;
        this.show_lblEdit = false;
      },
      error => {
        this.showError("Error in Update", "Error");
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
    this.toastr.warning(message, title, { timeOut: 2000, closeButton: true });
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 2000, closeButton: true });
  }

  sort(column) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = column;
    let direction = this.isDesc ? 1 : -1;

    this.ratesbycustomer = [...this.ratesbycustomer].sort((n1, n2) => {
      if (this.column == "description") {
        if (n1.description > n2.description) {
          return 1 * direction;
        } else if (n1.description < n2.description) {
          return -1 * direction;
        } else return 0;
      }

      if (this.column == "unitprice") {
        if (n1.unitprice > n2.unitprice) {
          return 1 * direction;
        } else if (n1.unitprice < n2.unitprice) {
          return -1 * direction;
        } else return 0;
      }
    });
  }
}
