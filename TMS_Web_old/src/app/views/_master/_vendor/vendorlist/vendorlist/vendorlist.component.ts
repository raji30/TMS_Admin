import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Address } from "../../../../../_models/address";
import { VendorService } from "../../../../../_services/vendor.service";
import { Vendor } from "../../../../../_models/vendor";
@Component({
  selector: "app-vendorlist",
  templateUrl: "./vendorlist.component.html",
  styleUrls: ["./vendorlist.component.scss"]
})
export class VendorlistComponent implements OnInit {
  vendors: Vendor[];
  public dataModel: Vendor;
  address: Address;

  dataSaved = false;
  updateVendor = null;
  message = null;
  show_addupdatevendor: boolean = false;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;

  constructor(
    private formbulider: FormBuilder,
    private Service: VendorService,
    private router: Router
  ) {
    this.dataModel = null;
  }

  ngOnInit() {
    this.dataModel = new Vendor();
    this.dataModel.Address = new Address();
    this.loadAllVendors();
  }

  loadAllVendors() {
    this.Service.getVendors().subscribe(
      data => (this.vendors = data),
      error => console.log(error),
      () => console.log("Get vendors", this.vendors)
    );
  }
  onFormSubmit() {
    this.dataSaved = false;
    this.CreateVendor();
  }
  EditVendor(vendorkey: string) {
    this.Service.GetVendorByID(vendorkey).subscribe(_updateVendor => {
      this.dataModel = _updateVendor;
      this.updateVendor = vendorkey;
      this.message = null;
      this.dataSaved = false;
    });
    this.show_addupdatevendor = true;
    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
  }

  CreateVendor() {
    if (this.updateVendor == null) {
      this.Service.createVendor(this.dataModel).subscribe(() => {
        this.dataSaved = true;
        this.message = "Driver Record saved Successfully";
        this.loadAllVendors();
        this.updateVendor = null;
        this.show_addupdatevendor = false;
      });
    } else {
      this.dataModel.vendkey = this.updateVendor;
      this.Service.updateVendor(this.dataModel).subscribe(() => {
        this.dataSaved = true;
        this.message = "Driver Record Updated Successfully";
        this.loadAllVendors();
        this.updateVendor = null;
        this.show_addupdatevendor = false;
      });
    }
  }

  resetForm() {
    this.message = null;
    this.dataSaved = false;

    this.dataModel = null;
    this.dataModel = new Vendor();
    this.dataModel.Address = new Address();
    this.updateVendor = null;
  }

  toggle() {
    this.show_addupdatevendor = true;
    this.isResetbtnhidden = true;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    this.show_addupdatevendor = false;
  }

  // convenience getter for easy access to form fields

  numberOnly(event): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
