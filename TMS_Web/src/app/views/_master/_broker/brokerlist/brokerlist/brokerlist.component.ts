import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Address } from "../../../../../_models/address";
import { Broker } from "../../../../../_models/broker";
import { BrokerService } from "../../../../../_services/broker.service";
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

  constructor(
    private formbulider: FormBuilder,
    private Service: BrokerService,
    private router: Router
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
    this.show_addupdateBroker = true;
    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
  }

  CreateBroker() {
    if (this.updateBroker == null) {
      this.Service.CreateBroker(this.dataModel).subscribe(() => {
        this.dataSaved = true;
        this.message = "Driver Record saved Successfully";
        this.loadAllBrokers();
        this.updateBroker = null;
        this.show_addupdateBroker = false;
      });
    } else {
      this.dataModel.BrokerKey = this.updateBroker;
      this.Service.UpdateBroker(this.dataModel).subscribe(() => {
        this.dataSaved = true;
        this.message = "Driver Record Updated Successfully";
        this.loadAllBrokers();
        this.updateBroker = null;
        this.show_addupdateBroker = false;
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
    this.show_addupdateBroker = true;
    this.isResetbtnhidden = true;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    this.show_addupdateBroker = false;
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

