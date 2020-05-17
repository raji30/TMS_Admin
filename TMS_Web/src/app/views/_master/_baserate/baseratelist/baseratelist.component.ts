import { Component, OnInit } from "@angular/core";
import { Customer } from "../../../../_models/customer";
import { CustomerService } from "../../../../_services/customer.service";
import { Address } from "../../../../_models/address";
import { City } from "./../../../../_models/city";
import { CityService } from "../../../../_services/city.service";
import { Baserate } from "./../../../../_models/baserate";
import { RateService } from "../../../../_services/rate.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-baseratelist",
  templateUrl: "./baseratelist.component.html",
  styleUrls: ["./baseratelist.component.scss"]
})
export class BaseratelistComponent implements OnInit {
  customers: Customer[];
  Citylist: City[];
  customersFilter: Customer[];
  searchText: string;
  selectedCustomer: Customer;
  CustomerName: string = "No customers selected";
  CustomerKey: string;

  BaseRateKey: string = "";
  citykey: string;
  cityname: string;
  BaseRate: string;
  UnitPrice: number;
  rowindex: number;
  ratedetails: Baserate[];
  btnLabel: string = "Add";
  Flag: string;
  ratesbycustomer: any[];

  btnEditShow: boolean = false;
  btnAddShow: boolean = false;
  showAddUpdate: boolean = false;
  
  private newAttribute: any = {}; 
  public newItem: any = {};

  isDesc: boolean = false;
  column: string = "CustId";
  p: number = 1;
  count: number;

  constructor(
    private customerService: CustomerService,
    private cityService: CityService,
    private rateService: RateService,
    private toastr: ToastrService
  ) {
    this.ratedetails = new Array<Baserate>();
  }

  ngOnInit() {
    this.loadCustomers();
    this.loadCity();
  }
  loadCustomers() {
    this.customerService.getCustomers().subscribe(
      data => (this.customers = this.customersFilter = data),
      error => console.log(error),
      () => console.log("Get customers complete", this.customers)
    );
  }
  loadCity() {
    this.cityService.GetCity().subscribe(
      data => (this.Citylist = data),
      error => console.log(error),
      () => console.log("Get citylist", this.Citylist)
    );
  }

  onSearchChange(searchValue: string): void {
    console.log(searchValue);
    if (!searchValue) {
      this.customersFilter = this.customers;
    }
    this.customersFilter = this.customers.filter(
      Customer =>
        Customer.CustName.toLowerCase().indexOf(searchValue.toLowerCase()) !==
        -1
    );
  }

  onSelect(CustomerSelected: Customer): void {
    this.selectedCustomer = CustomerSelected;
    this.CustomerName = this.selectedCustomer.CustName;
    this.CustomerKey = this.selectedCustomer.CustomerKey;
    this.getRates();
    this.showAddUpdate = false;
  }

  getRates()
  {
    this.rateService.GetBaseRateByCustomer(this.CustomerKey).subscribe(
      data => {this.ratesbycustomer = data;
        if (data.length > 0) {
          (this.btnEditShow = true), (this.btnAddShow = false);
        } else {
          (this.btnEditShow = false), (this.btnAddShow = true);
        }},
      error => console.log(error),
      () => console.log()
    );   
  }

  addupdateRate() {
    if (this.citykey == '') {
      return;
    }

    if (this.UnitPrice == 0 ) {
      return;
    }

    if(this.BaseRateKey == '')
    {
    for (var item of this.ratedetails) {
      if (item.citykey ==this.citykey) {
        this.showInfo("Add Rate", "Rate already fixed.");
        return;
      }
    }
  }

    if (this.BaseRateKey != '' ) {
      this.ratedetails[this.rowindex].unitprice = this.UnitPrice;
      this.btnLabel="Add";
    } else {
      var baseratedetails: any = {};
      baseratedetails.Flag = "NEW";
      baseratedetails.unitprice = this.UnitPrice;
      baseratedetails.cityname=   this.cityname;
      baseratedetails.citykey = this.citykey;
      baseratedetails.customerkey = this.CustomerKey;

      this.ratedetails.push(baseratedetails);
    }
    this.rowRefresh();
    return;
  }
  rowRefresh() {
    this.UnitPrice = 0;
    this.Flag = ''
    this.citykey = '';
    this.BaseRateKey ='';
  }

  editBaseRate(details: Baserate, index: number) {
    this.rowindex = index;
    this.UnitPrice = details.unitprice;
    this.citykey = details.citykey;
    this.CustomerKey = details.customerkey;
    this.BaseRateKey = details.baseratekey;
    this.btnLabel = "Update";
    this.Flag = details.Flag;
  }

  Submit() {
    if (this.ratedetails.length == 0) {
      this.showWarning("No data to save!", "Base Rate");
      return;
    }

    this.rateService.AddBaseRate(this.ratedetails).subscribe(
      data => {
        this.showSuccess("Rate applied successfully!", "Customer Base Rate");
        this.getRates();
        this.clear();
      },
      error => console.log(error),
      () => console.log()
    );
  }

  clear() {   
    this.ratedetails = null;
    this.showAddUpdate = false;
    this.rowindex = -1;
    this.UnitPrice = 0;
    this.Flag = "";
    this.citykey = '';
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
    this.toastr.info(message, title, { timeOut: 2000, closeButton: true });
  }

  sort(column) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = column;
    let direction = this.isDesc ? 1 : -1;

    this.ratesbycustomer = [...this.ratesbycustomer].sort((n1, n2) => {
      if (this.column == "cityname") {
        if (n1.cityname > n2.cityname) {
          return 1 * direction;
        } else if (n1.cityname < n2.cityname) {
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
  edit() {
    this.showAddUpdate = true;
  }
  newRate() {
    this.showAddUpdate = true;
  }

  drpCityChanged(val: any) {   
    this.cityname = this.Citylist.find(x => x.citykey == val).cityname;   
  }

  getRatesforEdit()
  {
    this.rateService.GetBaseRateByCustomer(this.CustomerKey).subscribe(
      data => {
        this.ratedetails =data;this.showAddUpdate = true;
        console.log("this.ratedetails",this.ratedetails);
      },
      error => console.log(error),
      () => console.log()
    );
  }
}
