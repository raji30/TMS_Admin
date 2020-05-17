import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { FormBuilder } from "@angular/forms";
import { Address } from "../../../../_models/address";
import { ToastrService } from "ngx-toastr";
import { CityService } from "../../../../_services/city.service";
import { City } from "../../../../_models/city";
import { Company } from "../../../../_models/company";
import { CompanyService } from '../../../../_services/company.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: "app-companylist",
  templateUrl: "./companylist.component.html",
  styleUrls: ["./companylist.component.scss"]
})
export class CompanylistComponent implements OnInit { 
  companies: Company[];
  citylist: City[];
  companyUpdate = null;
  show_addupdateCompany: boolean = false;
  show_companyInfo: boolean = false;
  submitted: boolean = false;
  company: Company;
  address: Address;
  selectedCompany: Company;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  show_btnCreateCompany: boolean = true;
  isACHrequired: boolean = false;
  searchText: string;

  
isDesc: boolean = false;
column: string = "CustId";
p: number = 1;
count: number;

  emailPattern: string = "[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,3}$";
  websitePattern: string =
    "(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?";

  constructor(
    private formbulider: FormBuilder,
    private Service: CompanyService,
    private router: Router,
    private cityService: CityService,
    private toastr: ToastrService,private SpinnerService: NgxSpinnerService
  ) {
    this.company = new Company();
    this.company.Address = new Address();
  }

  ngOnInit() {this.SpinnerService.show();
    this.loadAllCompanies();
    this.loadAllCity(); this.SpinnerService.hide();
  }

  loadAllCompanies() {
    
    this.Service.getCompanies().subscribe(
      data => (this.companies = data),
      error => console.log(error),
      () => console.log("get Companies complete")
    );
   
  }
  loadAllCity() {
    this.cityService.GetCity().subscribe(
      data => (this.citylist = data),
      error => console.log(error),
      () => console.log("Get citylist", this.citylist)
    );
  }
  onOutputAddressChange(addr: Address) {
   this.company.Address=addr;
  }

  onFormSubmit() {
    if (this.companyUpdate == null) {
      this.createCompany();
    } else {
      this.updateCompany();
    }
  }

  getCompanydataToEdit(companykey: string) {
    this.Service.getCompanyById(companykey).subscribe(company_edit => {
      this.companyUpdate = company_edit.compkey;
      this.selectedCompany = company_edit;
      this.company = company_edit;
    });
    this.show_companyInfo = true;
    this.show_addupdateCompany = false;
    this.show_btnCreateCompany = true;

    this.isCancelbtnhidden = true;
    this.isResetbtnhidden = false;
  }
  edit_click() {
    this.show_companyInfo = false;
    this.show_addupdateCompany = true;
    this.show_btnCreateCompany = false;
  }
  createCompany() {
    this.Service.createCompany(this.company).subscribe(
      () => {
        this.show_addupdateCompany = false;
        this.showSuccess("Company details created successfully", "Create");
        this.loadAllCompanies();
        this.show_btnCreateCompany = true;
        this.companyUpdate = null;
      },
      error => {
        this.showError("Error in Company detail creation: " + error, "Error");
      }
    );
  }
  updateCompany() {
    this.company.compkey = this.companyUpdate;
    this.Service.updateCompany(this.company).subscribe(
      () => {
        this.show_addupdateCompany = false;
        this.showSuccess("Customer updated successfully", "Edit");
        this.loadAllCompanies();
        this.show_btnCreateCompany = true;
        this.companyUpdate = null;
      },
      error => {
        this.showError("Error in Customer update: " + error, "Error");
      }
    );
  }

  clear() {
    this.company = null;
    this.company = new Company();
    this.company.Address = new Address();

    this.show_addupdateCompany = true;
    this.show_companyInfo = false;
    this.isResetbtnhidden = true;
    this.show_btnCreateCompany = false;
    this.companyUpdate = null;
  }

  cancel() {
    this.isResetbtnhidden = false;
    if (this.selectedCompany != null) {
      this.show_companyInfo = true;
    }
    this.show_addupdateCompany = false;
    this.show_btnCreateCompany = true;
  }

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
  clear_search() {
    this.searchText = undefined;
  } 

  sort(column) {
    this.isDesc = !this.isDesc; //change the direction
    this.column = column;
    let direction = this.isDesc ? 1 : -1;    

    this.companies = [...this.companies].sort((n1, n2) => {
      if ((this.column == "compname")) {
        if (n1.compname > n2.compname) {
          return 1* direction;
        } else if (n1.compname < n2.compname) {
          return -1* direction;
        } else return 0;
      }

      if ((this.column == "compid")) {
        if (n1.compid > n2.compid) {
          return 1* direction;
        } else if (n1.compid < n2.compid) {
          return -1* direction;
        } else return 0;
      }     
    });
  }
}
