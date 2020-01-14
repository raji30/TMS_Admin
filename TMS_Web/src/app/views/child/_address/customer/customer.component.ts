import {
  Component,
  OnInit,
  EventEmitter,
  Input,
  Output,
  ChangeDetectorRef
} from "@angular/core";
import { Address } from "../../../../_models/address";
import { AddressService } from "../../../../_services/address.service";
import { MasterService } from "../../../../_services/master.service";
import { CustomerService } from "../../../../_services/customer.service";
import { Customer } from "./../../../../_models/customer";

@Component({
  selector: "app-customer",
  templateUrl: "./customer.component.html",
  styleUrls: ["./customer.component.scss"]
})
export class CustomerComponent implements OnInit {
  billtoCustomerName: string = "";
  customer: Customer[];
  addressTobind: Address = new Address();

  @Input() Type: number;
  @Input() AddressType: number;
  @Input() customerKeyTobind: string;
  @Output() CustomerSelectedOutput = new EventEmitter<string>();
  @Output() OrdernoGenerated = new EventEmitter<string>();
  @Input() disabled: boolean = false;

  customercount: any;
  Orderno: any;
  creditCheck: boolean;

  selectedCustomer: Customer;// = new Customer();
  constructor(
    private service: CustomerService,
    private master: MasterService
  ) {}

  ngOnInit() {
   
    this.service
      .getCustomers()
      .subscribe(
        data => (this.customer = data),
        error => console.log(error),
        () => console.log("Get customer complete", this.customer)
      );
  }

  ngOnChanges() {
   
    if (this.customerKeyTobind != undefined) {
      this.service.getCustomers().subscribe(
        (data: any) => {
          this.customer = data;
          if (this.customerKeyTobind) {
            this.selectedCustomer = this.customer.find(
              x => x.CustomerKey === this.customerKeyTobind
            );
            this.billtoCustomerName = this.selectedCustomer.CustName;
          }
        },
        error => console.log(error),
        () => console.log("Get customer complete")
      );
    }
  }

  onSelect(CustomerSelected: Customer): void {
    this.selectedCustomer = CustomerSelected;
    this.billtoCustomerName = this.selectedCustomer.CustName;
    this.creditCheck = this.selectedCustomer.CreditCheck;
    console.log("Customer Details with Address:", this.selectedCustomer);

    this.master.getMaxcount_Customer(this.selectedCustomer.CustName).subscribe(
      data => {
        this.customercount = data;

        var year = new Date();
        var autono = this.pad(this.customercount + 1, 4);

        let x = this.selectedCustomer.CustName.split(" ");
        console.log("x[1].length", x.length);
        if (x.length == 1) {
          this.Orderno =
            x[0].substr(0, 4).toUpperCase() +"-"+
            year
              .getUTCFullYear()
              .toString()
              .substr(2, 2) +
            autono;
          console.log("length is 1");
        } else {
          this.Orderno =
            x[0].substr(0, 2).toUpperCase() +
            x[1].substr(0, 2).toUpperCase() +"-" +
            year
              .getUTCFullYear()
              .toString()
              .substr(2, 2) +
            autono;
          console.log("length is > 1");
        }
        this.CustomerSelectedOutput.emit(CustomerSelected.CustomerKey);
        this.OrdernoGenerated.emit(this.Orderno);
      },
      error => console.log(error),
      () => console.log("Get customercount", this.customercount)
    );
  }
  pad(num: number, size: number): string {
    let s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
  }
        // Calling this enables the component
    onEnableComponent() {
      this.disabled = false;
  }

  // Calling this disables the component
  onDisableComponent() {
      this.disabled = true;
  }
}
