import {
  Component,
  OnInit,
  ViewChild,
  Directive,
  Input,
  EventEmitter,
  Output,
  QueryList,
  ViewChildren
} from "@angular/core";
import { Router } from "@angular/router";
import { CustomerService } from "../../../../_services/customer.service";
import { Customer } from "../../../../_models/customer";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Address } from "../../../../_models/address";
import { ToastrService } from "ngx-toastr";
import { FileUploadService } from "./../../../../_services/fileupload.service";
import { CityService } from "../../../../_services/city.service";
import { City } from "../../../../_models/city";


@Component({
  selector: "app-listcustomer",
  templateUrl: "./listcustomer.component.html",
  styleUrls: ["./listcustomer.component.scss"]
})
export class ListcustomerComponent implements OnInit {
  
  isDesc: boolean = false;
  column: string = "CustId";

  customers: Customer[];
  citylist: City[];
  customerUpdate = null;
  show_addupdatecustomer: boolean = false;
  show_customerInfo: boolean = false;
  submitted: boolean = false;
  customer: any;
  address: Address;
  selectedCustomer: Customer;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  show_btnCreateCustomer: boolean = true;
  isACHrequired: boolean = false;

  searchText: string;
  p: number = 1;
  count: number;
  emailPattern: string = "[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,3}$";
  websitePattern: string =
    "(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?";

  constructor(
    private formbulider: FormBuilder,
    private Service: CustomerService,
    private router: Router,
    private cityService: CityService,
    private toastr: ToastrService
  ) {
    this.customer = new Customer();
    this.customer.Address = new Address();
  }

  ngOnInit() {
    this.loadAllCustomers();
    this.loadAllCity();
   
  }

  loadAllCustomers() {
    this.Service.getCustomers().subscribe(
      data => (
        (this.customers = data),
        (this.count = this.customers.length)       
      ),
      error => console.log(error),
      () => console.log("Get customers complete")
    );
    //this.dataSource.data = this.customers;
    console.log("Get customers complete", this.customers);
  }
  loadAllCity() {
    this.cityService.GetCity().subscribe(
      data => (this.citylist = data),
      error => console.log(error),
      () => console.log("Get citylist", this.citylist)
    );
  }
  onFormSubmit() {
    if (this.customerUpdate == null) {
      this.createCustomer();
    } else {
      this.updateCustomer();
    }
  }

  loadCustomerToEdit(customerkey: string) {
    this.Service.getCustomerById(customerkey).subscribe(customer_edit => {
      this.customerUpdate = customer_edit.CustomerKey;
      this.selectedCustomer = customer_edit;
      this.customer = customer_edit;
    });
    this.show_customerInfo = true;
    this.show_addupdatecustomer = false;
    this.show_btnCreateCustomer = true;

    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
  }
  edit_click() {
    this.show_customerInfo = false;
    this.show_addupdatecustomer = true;
    this.show_btnCreateCustomer = false;
  }
  createCustomer() {
    console.log("createCustomer", this.customer.Address);
    console.log("createCustomer", this.customer.address);
    this.customer.achrequired = this.isACHrequired;

    this.Service.createCustomer(this.customer).subscribe(
      () => {
        this.show_addupdatecustomer = false;
        this.showSuccess("Customer created successfully", "Create");
        this.loadAllCustomers();
        this.show_btnCreateCustomer = true;
        this.customerUpdate = null;
      },
      error => {
        this.showError("Error in Customer creation: " + error, "Error");
      }
    );
  }
  updateCustomer() {
    this.customer.achrequired = this.isACHrequired;

    this.customer.CustomerKey = this.customerUpdate;
    this.Service.updateCustomer(this.customer).subscribe(
      () => {
        this.show_addupdatecustomer = false;
        this.showSuccess("Customer updated successfully", "Edit");
        this.loadAllCustomers();
        this.show_btnCreateCustomer = true;
        this.customerUpdate = null;
      },
      error => {
        this.showError("Error in Customer update: " + error, "Error");
      }
    );
  }

  clear() {
    this.customer = null;
    this.customer = new Customer();
    this.customer.Address = new Address();

    this.show_addupdatecustomer = true;
    this.show_customerInfo = false;
    this.isResetbtnhidden = true;
    this.show_btnCreateCustomer = false;
    this.customerUpdate = null;
    this.customer.paymentterms = -1;
  }

  cancel() {
    this.isResetbtnhidden = false;
    if (this.selectedCustomer != null) {
      this.show_customerInfo = true;
    }
    this.show_addupdatecustomer = false;
    this.show_btnCreateCustomer = true;
  }

  // convenience getter for easy access to form fields
  // get f() {
  //   return this.customerForm.controls;
  // }

  numberOnly(event): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 4000, closeButton: true });
  }

  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 4000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title);
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 4000, closeButton: true });
  }
  clear_search() {
    this.searchText = undefined;
  }
  Checkbox_Change(value: any) {
    this.isACHrequired = value.target.checked;
    //alert(this.isACHrequired);
  }

  sort(column) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = column;
    let direction = this.isDesc ? 1 : -1;    

    this.customers = [...this.customers].sort((n1, n2) => {
      if ((this.column == "CustId")) {
        if (n1.CustId > n2.CustId) {
          return 1* direction;
        } else if (n1.CustId < n2.CustId) {
          return -1* direction;
        } else return 0;
      }

      if ((this.column == "CustName")) {
        if (n1.CustName > n2.CustName) {
          return 1* direction;
        } else if (n1.CustName < n2.CustName) {
          return -1* direction;
        } else return 0;
      }
      if ((this.column == "CreditLimit")) {
        if (n1.CreditLimit > n2.CreditLimit) {
          return 1* direction;
        } else if (n1.CreditLimit < n2.CreditLimit) {
          return -1* direction;
        } else return 0;
      }
    });
  }
}
