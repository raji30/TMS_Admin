import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Address } from "../../../../../_models/address";
import { Broker } from "../../../../../_models/broker";
import { BrokerService } from "../../../../../_services/broker.service";
import { ToastrService } from "ngx-toastr";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";
@Component({
  selector: 'app-brokerlist',
  templateUrl: './brokerlist.component.html',
  styleUrls: ['./brokerlist.component.scss']
})
export class BrokerlistComponent implements OnInit {
  brokers: Broker[];
  public dataModel: Broker;
  address: Address;

  dataSaved = false;
  updateBroker = null;
  message = null;
  show_addupdateBroker: boolean = false;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  show_btnCreateBroker : boolean = true; 
  show_BrokerInfo: boolean = false;
  searchText: string;

  constructor(
    private formbulider: FormBuilder,
    private Service: BrokerService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.dataModel = null;
  }

  ngOnInit() {
    this.dataModel = new Broker();
    this.dataModel.Address = new Address();
    this.loadAllBrokers();
  }

  loadAllBrokers() {
    this.Service.GetBrokers().subscribe(
      data => (this.brokers = data),
      error => console.log(error),
      () => console.log("Get brokers", this.brokers)
    );
  }
  onFormSubmit() {
    this.dataSaved = false;
    this.CreateBroker();
  }
  EditBroker(brokerKey: string) {
    this.Service.GetBrokerByID(brokerKey).subscribe(_updateVendor => {
      this.dataModel = _updateVendor;
      this.updateBroker = brokerKey;
      this.message = null;
      this.dataSaved = false;
    });
  
    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;

    this.show_btnCreateBroker = true;
    this.show_addupdateBroker = false;
    this.show_BrokerInfo = true;
  }

  CreateBroker() {
    if (this.updateBroker == null) {
      this.Service.CreateBroker(this.dataModel).subscribe(() => {
        this.dataSaved = true;       
        this.showSuccess("saved successfully", "Create");
        this.loadAllBrokers();
        this.updateBroker = null;
        this.show_addupdateBroker = false;
        this.show_btnCreateBroker = true;
      },
      error => {
        this.showError("Error in Creation", "Error");
      });
    } else {
      this.dataModel.BrokerKey = this.updateBroker;
      this.Service.UpdateBroker(this.dataModel).subscribe(() => {
        this.dataSaved = true;       
        this.showSuccess("Updated successfully", "Update");
        this.loadAllBrokers();
        this.updateBroker = null;
        this.show_addupdateBroker = false;
        this.show_btnCreateBroker = true;
      },error => {
        this.showError("Error in Update", "Error");
      });
    }
  }

  resetForm() {
    this.message = null;
    this.dataSaved = false;

    this.dataModel = null;
    this.dataModel = new Broker();
    this.dataModel.Address = new Address();
    this.updateBroker = null;
  }

  toggle() {
    this.show_btnCreateBroker = false;
    this.show_addupdateBroker = true;
    this.show_BrokerInfo = false;
  
    this.isResetbtnhidden = true;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    this.show_addupdateBroker = false;
    this.show_btnCreateBroker = true;
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
    this.show_btnCreateBroker = false;
    this.show_addupdateBroker = true;
    this.show_BrokerInfo = false;
  }
  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, { timeOut: 2000, closeButton: true });
  }

  showError(message: string, title: string) {
    this.toastr.error(message, "Oops!", { timeOut: 2000, closeButton: true });
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title, { timeOut: 2000, closeButton: true });
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, { timeOut: 2000, closeButton: true });
  }
  clear_search()
  {
    this.searchText = undefined;
  }
}

