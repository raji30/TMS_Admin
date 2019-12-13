import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Address } from "../../../../_models/address";
import { Driver } from "../../../../_models/driver";
import { DriverService } from "../../../../_services/driver.service";
import { Carrier } from "../../../../common/master";
import { MasterService } from "../../../../_services/master.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-listdriver",
  templateUrl: "./listdriver.component.html",
  styleUrls: ["./listdriver.component.scss"]
})
export class ListdriverComponent implements OnInit {
  drivers: Driver[];
  carrierlist: Carrier[];
  dataSaved = false;
  driverUpdate = null;
  message = null;
  show_addupdatedriver: boolean = false;
  show_DriverInfo: boolean = false;
  show_btnCreatedriver: boolean = true;
  submitted: boolean = false;
  public driverModel: any;
  address: Address;

  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;

  constructor(
    private Service: DriverService,
    private master: MasterService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.driverModel = null;
  }

  ngOnInit() {
    // this.driverModel = new Driver();
    // this.driverModel.Address = new Address();
    this.master.getCarrierList().subscribe(
      data => (this.carrierlist = data),
      error => console.log(error),
      () => console.log("Get carrierlist", this.carrierlist)
    );

    this.loadAlldrivers();
  }

  loadAlldrivers() {
    this.Service.getDrivers().subscribe(
      data => (this.drivers = data),
      error => console.log(error),
      () => console.log("Get drivers", this.drivers)
    );
  }
  onFormSubmit() {
    this.submitted = true;
    this.dataSaved = false;
    this.CreateDriver();
  }
  loadCustomerToEdit(driverkey: string) {
    this.Service.getDriverById(driverkey).subscribe(driver_edit => {
      this.driverModel = driver_edit;
      this.driverUpdate = driverkey;
      this.message = null;
      this.dataSaved = false;
    });
    this.show_addupdatedriver = false;
    this.show_DriverInfo = true;
    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
    this.show_btnCreatedriver = true;
  }

  CreateDriver() {
    if (this.driverUpdate == null) {
      this.Service.createDriver(this.driverModel).subscribe(
        () => {
          this.dataSaved = true;
          this.message = "Driver Record saved Successfully";
          this.showSuccess("created successfully", "Create");
          this.loadAlldrivers();
          this.driverUpdate = null;
          this.show_addupdatedriver = false;
        },
        error => {
          this.showError("Error in Creation", "Error");
        }
      );
    } else {
      // this.driverModel.DriverKey = this.driverUpdate;
      this.Service.updateDriver(this.driverModel).subscribe(
        () => {
          this.dataSaved = true;
          this.message = "Driver Record Updated Successfully";
          this.showSuccess("Updated successfully", "Update");
          this.loadAlldrivers();
          this.driverUpdate = null;
          this.show_addupdatedriver = false;
        },
        error => {
          this.showError("Error in Update", "Error");
        }
      );
    }
  }

  resetForm() {
    // this.driverForm.reset();
    this.message = null;
    this.dataSaved = false;

    this.driverModel = null;
    this.driverModel = new Driver();
    this.driverModel.Address = new Address();
    this.driverUpdate = null;
  }

  toggle() {
    this.show_addupdatedriver = true;
    this.isResetbtnhidden = true;
    this.show_btnCreatedriver = false;
    this.show_DriverInfo = false;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    if (this.driverModel != null) {
      this.show_DriverInfo = false;
    }
    this.show_addupdatedriver = false;
    this.show_btnCreatedriver = true;
  }

  // convenience getter for easy access to form fields
  numberOnly(event): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  bindFormControls() {
    this.show_addupdatedriver = true;
    this.show_DriverInfo = false;
    this.show_btnCreatedriver = false;
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
}
