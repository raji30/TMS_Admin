
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { ItemType } from "../../../../_models/ItemType";
import { City } from "../../../../_models/city";
import { CityService } from "../../../../_services/city.service";

@Component({
  selector: "app-listcity",
  templateUrl: "./listcity.component.html",
  styleUrls: ["./listcity.component.scss"]
})
export class ListcityComponent implements OnInit {
  citylist: City[];
  public dataModel: City;
 
  show_DivAddUpdate: boolean = false;
  show_DivInfo: boolean = false;
  show_btnAdd: boolean = true;
  show_lblAdd: boolean = false;
  show_lblEdit: boolean = false;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  itemKey: string;
  searchText: string;

  constructor(
    private cityService: CityService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {   

    this.loadAllItems();
  }

  loadAllItems() {
    this.cityService.GetCity()
    .subscribe(
      data => (this.citylist = data),
      error => console.log(error),
      () => console.log("Get citylist", this.citylist)
    );
  }

  getcityById(cityKey: string) {
    this.cityService.GetCityByID(cityKey).subscribe(
      _userbyId => {
        this.dataModel = _userbyId;
        this.itemKey = cityKey;
        this.show_DivInfo = true;
        this.show_lblEdit = false;
        this.show_lblAdd = false;
        this.show_DivAddUpdate = false;
        this.show_btnAdd = true;
        console.log("user by Id", this.dataModel);
      },
      error => {
        this.showError("Error in getting user ", "Error");
      }
    );
  }

  onSubmit() {
   
    if (this.itemKey == null) {
      this.cityService.AddCity(this.dataModel).subscribe(
        () => {
          this.showSuccess("created successfully", "Create");
          this.loadAllItems();
          this.itemKey = null;
          this.show_DivAddUpdate = false;
          this.show_btnAdd = true;
          this.show_lblAdd=false;
        },
        error => {
          this.showError("Error in Creation", "Error");
        }
      );
    } else {
      this.cityService.UpdateCity(this.dataModel).subscribe(
        () => {
          this.showSuccess("Updated successfully", "Update");
          this.loadAllItems();
          this.itemKey = null;
          this.show_DivAddUpdate = false;
          this.show_btnAdd = true;
          this.show_lblEdit=false;
        },
        error => {
          this.showError("Error in Update", "Error");
        }
      );
    }
  }

  bindFormControls() {
    this.show_DivAddUpdate = true;
    this.show_DivInfo = false;
    this.show_btnAdd = false;
    this.show_lblAdd = false;
    this.show_lblEdit = true;
  }

  resetForm() {
    this.dataModel = null;
    this.dataModel = new City();
    this.itemKey = null;
  }

  toggle() {
    this.show_DivAddUpdate = true;
    this.isResetbtnhidden = true;
    this.show_btnAdd = false;
    this.show_lblAdd = true;
    this.show_lblEdit = false;
    this.show_DivInfo = false;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    if (this.dataModel != null) {
      this.show_DivInfo = false;
    }
    this.show_DivAddUpdate = false;
    this.show_btnAdd = true;
    this.show_lblAdd = false;
    this.show_lblEdit = false;
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
  clear_search()
  {
    this.searchText = undefined;
  }
}
