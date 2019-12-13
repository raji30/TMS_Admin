import { Component, OnInit } from "@angular/core";
import { Address } from "../../../../_models/address";
import { User } from "../../../../_models/User";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { UserService } from "../../../../_services/user.service";

@Component({
  selector: "app-userlist",
  templateUrl: "./userlist.component.html",
  styleUrls: ["./userlist.component.scss"]
})
export class UserlistComponent implements OnInit {
  Users: User[];
  public dataModel: User;

  show_addupdateUser: boolean = false;
  show_UserInfo: boolean = false;
  show_btnCreateUser: boolean = true;
  show_lblAddNewUser: boolean = false;
  show_lblEditUserDetail: boolean = false;
  isCancelbtnhidden: boolean = true;
  isResetbtnhidden: boolean = true;
  userKey: string;

  constructor(
    private usrService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.loadAllUsers();
  }

  loadAllUsers() {
    this.usrService.getAll().subscribe(
      data => (this.Users = data),
      error => {
        this.showError("Error in getting All user ", "Error");
      },
      () => console.log("Users list", this.Users)
    );
  }

  getUserById(userKey: string) {
    this.usrService.getUserById(userKey).subscribe(
      _userbyId => {
        this.dataModel = _userbyId;
        this.userKey = userKey;
        this.show_UserInfo = true;
        this.show_lblEditUserDetail = false;
        this.show_lblAddNewUser = false;
        this.show_addupdateUser = false;
        this.show_btnCreateUser = true;
        console.log("user by Id", this.dataModel);
      },
      error => {
        this.showError("Error in getting user ", "Error");
      }
    );
  }

  onSubmit() {
    // if (
    //   this.dataModel.UserId == null ||
    //   this.dataModel.UserId == undefined
    // ) {
    //  // this.showWarning("Please enter User Id.", "User");
    //  alert('Error!! \n\n' + 'Please enter User Id.');
    //   return;
    // }
    // if (
    //   this.dataModel.FirstName == null ||
    //   this.dataModel.FirstName == undefined
    // ) {
    //  // this.showWarning("Please enter First Name.", "User");
    //   alert('Error!! \n\n' + 'Please enter First Name.');
    //   return;
    // }
    // if (
    //   this.dataModel.Address.Address1 == null ||
    //   this.dataModel.Address.Address1 == undefined
    // ) {
    //  // this.showWarning("Please enter Address Line 1.", "User");
    //  alert('Error!! \n\n' + 'Please enter Address Line 1.');
    //   return;
    // }
    // alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.dataModel))
    // return;
    if (this.userKey == null) {
      this.usrService.CreateUser(this.dataModel).subscribe(
        () => {
          this.showSuccess("created successfully", "Create");
          this.loadAllUsers();
          this.userKey = null;
          this.show_addupdateUser = false;
          this.show_btnCreateUser = true;
          this.show_lblAddNewUser=false;
        },
        error => {
          this.showError("Error in Creation", "Error");
        }
      );
    } else {
      this.usrService.UpdateUser(this.dataModel).subscribe(
        () => {
          this.showSuccess("Updated successfully", "Update");
          this.loadAllUsers();
          this.userKey = null;
          this.show_addupdateUser = false;
          this.show_btnCreateUser = true;
          this.show_lblEditUserDetail=false;
        },
        error => {
          this.showError("Error in Update", "Error");
        }
      );
    }
  }

  bindFormControls() {
    this.show_addupdateUser = true;
    this.show_UserInfo = false;
    this.show_btnCreateUser = false;
    this.show_lblAddNewUser = false;
    this.show_lblEditUserDetail = true;
  }

  resetForm() {
    this.dataModel = null;
    this.dataModel = new User();
    this.dataModel.address = new Address();
    this.userKey = null;
  }

  toggle() {
    this.show_addupdateUser = true;
    this.isResetbtnhidden = true;
    this.show_btnCreateUser = false;
    this.show_lblAddNewUser = true;
    this.show_lblEditUserDetail = false;
    this.show_UserInfo = false;
    this.resetForm();
  }

  cancel() {
    this.isResetbtnhidden = false;
    if (this.dataModel != null) {
      this.show_UserInfo = false;
    }
    this.show_addupdateUser = false;
    this.show_btnCreateUser = true;
    this.show_lblAddNewUser = false;
    this.show_lblEditUserDetail = false;
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
