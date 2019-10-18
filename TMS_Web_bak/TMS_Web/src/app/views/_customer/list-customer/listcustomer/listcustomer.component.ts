import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { CustomerService } from "../../../../_services/customer.service";
import { Customer } from "../../../../_models/customer";
import { Observable } from "rxjs";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Address } from "../../../../_models/address";

@Component({
  selector: "app-listcustomer",
  templateUrl: "./listcustomer.component.html",
  styleUrls: ["./listcustomer.component.scss"]
})
export class ListcustomerComponent implements OnInit {
  customers: Observable<Customer[]>;
  dataSaved = false;
  customerForm: FormGroup;
  customerUpdate = null;
  message = null;
  show_addupdatecustomer: boolean = false;
  submitted: boolean = false;
  customer: Customer;
  address: Address;

  selectedCustomer: Customer;

  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;

  constructor(
    private formbulider: FormBuilder,
    private Service: CustomerService,
    private router: Router
  ) {}

  ngOnInit() {
    this.customerForm = this.formbulider.group({
      CustomerKey: [],
      custid: ["", [Validators.required]],
      CustName: ["", [Validators.required]],
      creditlimit: [
        "",
        [
          Validators.required,
          Validators.pattern("^[0-9]*$"),
          Validators.minLength(3)
        ]
      ],
      Address: this.formbulider.group({
        AddrKey: [""],
        Address1: ["", [Validators.required]],
        Address2: ["", [Validators.required]],
        City: ["", [Validators.required]],
        State: ["", [Validators.required]],
        Zip: [
          "",
          [
            Validators.required,
            Validators.pattern("^[0-9]*$"),
            Validators.minLength(3),
            Validators.maxLength(3)
          ]
        ]
      })
    });

    this.loadAllCustomers();
  }

  loadAllCustomers() {
    this.customers = this.Service.getCustomers();
  }
  onFormSubmit() {
    this.submitted = true;
    if (this.customerForm.invalid) {
      return;
    }
    this.dataSaved = false;

    let cust: Customer = this.customerForm.value;
    this.CreateCustomer(cust);
    this.customerForm.reset();
  }
  searchCustomer(customerkey: string) {}
  loadCustomerToEdit(customerkey: string) {
    this.Service.getCustomerById(customerkey).subscribe(customer_edit => {
      this.message = null;
      this.dataSaved = false;
      this.customerUpdate = customer_edit.CustomerKey;

      this.customerForm.controls["CustomerKey"].setValue(
        customer_edit.CustomerKey
      );
      this.customerForm.controls["custid"].setValue(customer_edit.CustId);
      this.customerForm.controls["CustName"].setValue(customer_edit.CustName);
      this.customerForm.controls["creditlimit"].setValue(
        customer_edit.CreditLimit
      );

      this.customerForm["controls"].Address["controls"].AddrKey.setValue(
        customer_edit.Address["AddrKey"]
      );
      this.customerForm["controls"].Address["controls"].Address1.setValue(
        customer_edit.Address["Address1"]
      );
      this.customerForm["controls"].Address["controls"].Address2.setValue(
        customer_edit.Address["Address2"]
      );
      this.customerForm["controls"].Address["controls"].City.setValue(
        customer_edit.Address["City"]
      );
      this.customerForm["controls"].Address["controls"].State.setValue(
        customer_edit.Address["State"]
      );
      this.customerForm["controls"].Address["controls"].Zip.setValue(
        customer_edit.Address["Zip"]
      );
    });
    this.show_addupdatecustomer = true;
    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
  }
  CreateCustomer(customer: Customer) {
    if (this.customerUpdate == null) {
      this.Service.createCustomer(customer).subscribe(() => {
        this.dataSaved = true;
        this.message = "Customer Record saved Successfully";
        this.loadAllCustomers();
        this.customerUpdate = null;
        this.customerForm.reset();
      });
    } else {
      customer.CustomerKey = this.customerUpdate;
      this.Service.updateCustomer(customer).subscribe(() => {
        this.dataSaved = true;
        this.message = "Customer Record Updated Successfully";
        this.loadAllCustomers();
        this.customerUpdate = null;
        this.customerForm.reset();
      });
    }
  }

  resetForm() {
    this.customerForm.reset();
    this.message = null;
    this.dataSaved = false;
  }

  toggle() {
    // if (this.show_addupdatecustomer) this.show_addupdatecustomer = false;
    // else if (!this.show_addupdatecustomer) this.show_addupdatecustomer = true;

    this.show_addupdatecustomer = true;
    this.isResetbtnhidden = true;
  }

  cancel() {
    this.isResetbtnhidden = false;
    this.show_addupdatecustomer = false;
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.customerForm.controls;
  }

  numberOnly(event): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
