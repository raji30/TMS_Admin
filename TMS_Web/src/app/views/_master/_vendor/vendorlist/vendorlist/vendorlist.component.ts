import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Address } from "../../../../../_models/address";
import { VendorService } from "../../../../../_services/vendor.service";
import { Vendor } from "../../../../../_models/vendor";
import { ToastrService } from "ngx-toastr";
@Component({
  selector: "app-vendorlist",
  templateUrl: "./vendorlist.component.html",
  styleUrls: ["./vendorlist.component.scss"]
})
export class VendorlistComponent implements OnInit {
  vendors: Vendor[];
  public dataModel: Vendor;
  address: Address;
  key: string;

  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;

  show_divAddUpdate: boolean = false;
  show_divInfo: boolean = false;
  show_btnAdd: boolean = true;
  show_lblAdd: boolean = false;
  show_lblUpdate: boolean = false;

  constructor(
    private Service: VendorService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.dataModel = null;
  }

  ngOnInit() {
    // this.dataModel = new Vendor();
    // this.dataModel.Address = new Address();
    this.loadAllVendors();
  }

  loadAllVendors() {
    this.Service.getVendors().subscribe(
      data => (this.vendors = data),
      error => console.log(error),
      () => console.log("Get vendors", this.vendors)
    );
  }
  onSubmit() {
    this.CreateVendor();
  }
  getVendorById(vendorkey: string) {
    this.Service.GetVendorByID(vendorkey).subscribe(_updateVendor => {
      this.dataModel = _updateVendor;
      this.key = vendorkey;
      this.show_divAddUpdate = false;
      this.show_divInfo = true;
      this.isCancelbtnhidden = true;
      this.isResetbtnhidden = false;
      console.log("Vendors list", this.dataModel);
    },error => {
      this.showError("Error", "Error");
    });
  }

  CreateVendor() {
    if (this.key == null) {
      this.Service.createVendor(this.dataModel).subscribe(() => {
        this.loadAllVendors();
        this.showSuccess("created successfully", "Create");
        this.key = null;
        this.show_divAddUpdate = false;
      },error => {
        this.showError("Error in vendor creation", "Error");
      });
    } else {
      this.dataModel.vendkey = this.key;
      this.Service.updateVendor(this.dataModel).subscribe(() => {
        this.loadAllVendors();
        this.showSuccess("Updated successfully", "Update");
        this.key = null;
        this.show_divAddUpdate = false;
      },error => {
        this.showError("Error in vendor update", "Error");
      });
    }
  }

  resetForm() {
    this.dataModel = null;
    this.dataModel = new Vendor();
    this.dataModel.Address = new Address();
    this.key = null;
  }

  toggle() {
    this.show_divAddUpdate = true;
    this.show_lblAdd = true;
    this.show_lblUpdate = false;
    this.show_btnAdd = false;
    this.isResetbtnhidden = true;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    this.show_divInfo = false;
    this.show_divAddUpdate = false;
    this.show_btnAdd = true;
    this.show_lblAdd = false;
    this.show_lblUpdate = false;
    this.key = null;
  }

  bindFormControls() {
    this.show_divAddUpdate = true;
    this.show_divInfo = false;
    this.show_btnAdd = false;
    this.show_lblAdd = false;
    this.show_lblUpdate = true;
  }

  // convenience getter for easy access to form fields
  numberOnly(event): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }


  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }

  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title,{ timeOut: 2000, closeButton: true });
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 2000, closeButton: true });
  }
}
