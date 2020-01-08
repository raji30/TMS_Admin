
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Item } from "../../../../_models/item";
import { ItemService } from "../../../../_services/item.service";
import { ItemType } from "../../../../_models/ItemType";

@Component({
  selector: "app-userlist",
  templateUrl: "./itemlist.component.html",
  styleUrls: ["./itemlist.component.scss"]
})
export class ItemlistComponent implements OnInit {
  Items: Item[];
  public dataModel: Item;
  ItemTypes:ItemType[];

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
    private itmService: ItemService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {

    this.itmService.GetItemTypes()
    .subscribe(
      data => (this.ItemTypes = data),
      error => console.log(error),
      () => console.log("Get ItemTypes", this.ItemTypes)
    );

    this.loadAllItems();
  }

  loadAllItems() {
    this.itmService.GetItems().subscribe(
      data => (this.Items = data),
      error => {
        this.showError("Error in getting All Items ", "Error");
      },
      () => console.log("Items list", this.Items)
    );
  }

  getItemById(itemKey: string) {
    this.itmService.GetItemByID(itemKey).subscribe(
      _userbyId => {
        this.dataModel = _userbyId;
        this.itemKey = itemKey;
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
      this.itmService.CreateItem(this.dataModel).subscribe(
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
      this.itmService.UpdateItem(this.dataModel).subscribe(
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
    this.dataModel = new Item();
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
