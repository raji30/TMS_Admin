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
  billtoCompanyName: string = "Select";
  AddrName: string = "";
  company: Address[];
  companyFilter: Address[];
  addressTobind: Address = new Address();

  @Input() Type: number;
  @Input() AddressType: number;
  @Input() addressKeyTobind: string;
  @Output() CustomerSelectedOutput = new EventEmitter<string>();
  @Output() OrdernoGenerated = new EventEmitter<string>();

  customercount: any;
  Orderno: any;
  searchText:string;

  selectedCompany: Address;// = new Address();
  constructor(private service: AddressService, private master: MasterService) {}

  ngOnInit() {
   // console.log(this.addressKeyTobind);
    this.service
      .getAddress(this.Type)
      .subscribe(
        data => (this.company = this.companyFilter=data),
        error => console.log(error),
        () => console.log("Get customer complete",this.company)
      );

    // if (this.addressKeyTobind != undefined) {
    //   this.addressTobind = this.company.find(
    //     x => x.AddrKey === this.addressKeyTobind
    //   );
    //   console.log(this.addressTobind);
    //   this.onSelect(this.addressTobind);
    // }
  }

  ngOnChanges() {
    if (this.addressKeyTobind != "") {
      this.service.getAddress(this.Type).subscribe(
        (data: any) => {
          this.company =this.companyFilter= data;
          if (this.addressKeyTobind) {
            this.selectedCompany = this.company.find(
              x => x.AddrKey === this.addressKeyTobind
            );
            this.billtoCompanyName = this.selectedCompany.Name;
          }
        },
        error => console.log(error)        
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
    this.selectedCompany = CustomerSelected;
    this.billtoCompanyName = this.selectedCompany.Name;
    console.log(this.selectedCompany);

    // this.master.getMaxcount_Customer(this.selectedCompany.Name).subscribe(
    //   data => {
    //     this.customercount = data;

    //     var year = new Date();
    //     var autono = this.pad(this.customercount + 1, 4);
    //     this.Orderno =
    //       this.selectedCompany.Name.substr(0, 2).toUpperCase() +
    //       year
    //         .getUTCFullYear()
    //         .toString()
    //         .substr(2, 2) +
    //       autono;

    //     this.CustomerSelectedOutput.emit(CustomerSelected.AddrKey);
    //     this.OrdernoGenerated.emit(this.Orderno);
    //   },
    //   error => console.log(error),
    //   () => console.log("Get customercount", this.customercount)
    // );

    this.CustomerSelectedOutput.emit(CustomerSelected.AddrKey);
  }
  pad(num: number, size: number): string {
    let s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
  }

  onSearchChange(searchValue: string): void {  
    console.log(searchValue);
     if(!searchValue){
       this.assignCopy();
   } // when nothing has typed
   this.companyFilter = this.company.filter(item => item.Name.toLowerCase().indexOf(searchValue.toLowerCase()) !== -1
   )
  }
  assignCopy(){
   this.companyFilter = Object.assign([], this.company);
}
}
