import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Address } from "../../../../_models/address";
import { Carrier } from "../../../../common/master";
import { CarrierService } from "../../../../_services/carrier.service";
import { ToastrService } from "ngx-toastr";
@Component({
  selector: "app-carrierlist",
  templateUrl: "./carrierlist.component.html",
  styleUrls: ["./carrierlist.component.scss"]
})
export class CarrierlistComponent implements OnInit {
  carriers: Carrier[];
  public dataModel: Carrier;
  address: Address;

  dataSaved = false;
  updateCarrier = null;
  message = null;
  show_addupdateCarrier: boolean = false;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  show_CarrierInfo: boolean = false;
  show_btnCreateCarrier: boolean = true;

  constructor(
    private formbulider: FormBuilder,
    private Service: CarrierService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.dataModel = null;
  }

  ngOnInit() {
    this.dataModel = new Carrier();
    this.dataModel.Address = new Address();
    this.loadAllCarriers();
  }

  loadAllCarriers() {
    this.Service.GetCarriers().subscribe(
      data => (this.carriers = data),
      error => console.log(error),
      () => console.log("Get carriers", this.carriers)
    );
  }
  onFormSubmit() {
    this.dataSaved = false;
    this.CreateCarrier();
  }
  EditCarrier(carrierKey: string) {
    this.Service.GetCarrierByID(carrierKey).subscribe(_updateCarrier => {
      this.dataModel = _updateCarrier;
      this.updateCarrier = carrierKey;
      this.message = null;
      this.dataSaved = false;
    });
    this.show_addupdateCarrier = false;
    this.show_CarrierInfo = true;
    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
    this.show_btnCreateCarrier = true;
  }

  CreateCarrier() {
    if (this.updateCarrier == null) {
      this.Service.CreateCarrier(this.dataModel).subscribe(() => {
        this.dataSaved = true;   
        this.showSuccess("created successfully", "Create");
        this.loadAllCarriers();
        this.updateCarrier = null;
        this.show_addupdateCarrier = false;
      }, error => {
        this.showError("Error in Creation", "Error");
      });
    } else {
      // this.dataModel.CarrierKey = this.updateCarrier;
      this.Service.UpdateCarrier(this.dataModel).subscribe(() => {
        this.dataSaved = true;        
        this.showSuccess("Updated successfully", "Updated");
        this.loadAllCarriers();
        this.updateCarrier = null;
        this.show_addupdateCarrier = false;
      }, error => {
        this.showError("Error in update", "Error");
      });
    }
  }

  resetForm() {
    this.message = null;
    this.dataSaved = false;

    this.dataModel = null;
    this.dataModel = new Carrier();
    this.dataModel.Address = new Address();
    this.updateCarrier = null;
  }

  toggle() {
    this.show_btnCreateCarrier = false;
    this.show_addupdateCarrier = true;
    this.show_CarrierInfo = false;
    this.isResetbtnhidden = true;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    this.show_addupdateCarrier = false;
    this.show_CarrierInfo = true;
    this.show_btnCreateCarrier = true;
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
  bindFormControls() {
    this.show_addupdateCarrier = true;
    this.show_CarrierInfo = false;
    this.show_btnCreateCarrier = false;
  }
}
