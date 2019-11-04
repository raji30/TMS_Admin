import { Component, OnInit, EventEmitter, Input, Output } from "@angular/core";
import { Address } from "../../../../_models/address";
import { AddressService } from "../../../../_services/address.service";
import { MasterService } from "../../../../_services/master.service";

@Component({
  selector: "app-company",
  templateUrl: "./company.component.html",
  styleUrls: ["./company.component.scss"]
})
export class CompanyComponent implements OnInit {
  billtoCustomerName: string = "Select";
  AddrName: string = "";
  customer: Address[];
  addressTobind: Address = new Address();

  @Input() Type: number;
  @Input() AddressType: number;
  @Input() addressKeyTobind: string;
  @Output() CustomerSelectedOutput = new EventEmitter<string>();
  @Output() OrdernoGenerated = new EventEmitter<string>();

  customercount: any;
  Orderno: any;

  selectedCustomer: Address = new Address();
  constructor(private service: AddressService, private master: MasterService) {}

  ngOnInit() {
    console.log(this.addressKeyTobind);
    this.service
      .getAddress(this.Type)
      .subscribe(
        data => (this.customer = data),
        error => console.log(error),
        () => console.log("Get customer complete")
      );

    if (this.addressKeyTobind != undefined) {
      this.addressTobind = this.customer.find(
        x => x.AddrKey === this.addressKeyTobind
      );
      console.log(this.addressTobind);
      this.onSelect(this.addressTobind);
    }
  }

  ngOnChanges() {
    if (this.addressKeyTobind != undefined) {
      this.service.getAddress(this.Type).subscribe(
        (data: any) => {
          this.customer = data;
          if (this.addressKeyTobind) {
            this.selectedCustomer = this.customer.find(
              x => x.AddrKey === this.addressKeyTobind
            );
            this.billtoCustomerName = this.selectedCustomer.Name;
          }
        },
        error => console.log(error),
        () => console.log("Get customer complete")
      );
    }

    if (this.AddressType === 1) {
      this.AddrName = "Consignee";
    }
    if (this.AddressType === 2) {
      this.AddrName = "Pick-up   ";
    }
    if (this.AddressType === 3) {
      this.AddrName = "Delivery ";
    }
    if (this.AddressType === 4) {
      this.AddrName = "Return   ";
    }
  }

  onSelect(CustomerSelected: Address): void {
    this.selectedCustomer = CustomerSelected;
    this.billtoCustomerName = this.selectedCustomer.Name;
    console.log(this.selectedCustomer);

    this.master.getMaxcount_Customer(this.selectedCustomer.Name).subscribe(
      data => {
        this.customercount = data;

        var year = new Date();
        var autono = this.pad(this.customercount + 1, 4);
        this.Orderno =
          this.selectedCustomer.Name.substr(0, 2).toUpperCase() +
          year
            .getUTCFullYear()
            .toString()
            .substr(2, 2) +
          autono;

        this.CustomerSelectedOutput.emit(CustomerSelected.AddrKey);
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
}
