import { Component, OnInit } from "@angular/core";
import { User } from "../../../_models/User";
import { UserService } from "../../../_services/user.service";
import { ToastrService } from "ngx-toastr";
import { UserPermissions } from "../../../_models/UserPermissions";
import { UserpermissionService } from "../../../_services/userpermission.service";

@Component({
  selector: "app-userpermissions",
  templateUrl: "./userpermissions.component.html",
  styleUrls: ["./userpermissions.component.scss"]
})
export class UserpermissionsComponent implements OnInit {
  constructor(
    private usrService: UserService,
    private toastr: ToastrService,
    private userpermission: UserpermissionService
  ) {}

  Users: User[];
  UserPermissions: UserPermissions[];
  AddUpdateUserPermissions: UserPermissions[];
  public dataModel: User;

  show_DivAddUpdate: boolean = false;
  show_DivInfo: boolean = false;
  show_btnAdd: boolean = false;
  show_btnEdit: boolean = false;
  show_AddCancel: boolean = false;

  userKey: string;
  IsNew: boolean;
  searchText: string;

  ngOnInit() {
    this.loadAllUsers();

    this.userpermission.getMenus().subscribe(
      data => {
        this.AddUpdateUserPermissions = data;
        console.log("AddUpdateUserPermissions ", this.AddUpdateUserPermissions);
      },
      error => {
        this.showError("Error in getting user ", "Error");
      }
    );
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

  fnNew_click() {
    this.IsNew = true;
    this.show_DivAddUpdate = true;
    this.show_DivInfo = false;
    this.show_AddCancel = true;
  }
  fnUpdate_click() {
    this.IsNew = false;
    this.show_DivAddUpdate = true;
    this.show_DivInfo = false;
    this.show_AddCancel = true;

    this.AddUpdateUserPermissions =  this.UserPermissions;
  }

  cancel() {
    this.show_AddCancel = false;
    this.show_DivAddUpdate = false;
    this.show_DivInfo = true;
  }
  clear()
  {
    this.show_btnEdit = false;
    this.show_btnAdd = false;
    this.show_DivAddUpdate = false;
    this.show_DivInfo = false;
    this.show_DivAddUpdate = false;    
    this.show_AddCancel = false;
  }

  drpUsers_ChangedEvent(UserKey: any) {
    if (UserKey == 0) {
     this.clear();
      return;
    }
    this.userKey = UserKey;
    this.getPermissionsByUser(UserKey);
  }

  getPermissionsByUser(UserKey:string)
  {
    this.userpermission.getpermissionsByuserkey(UserKey).subscribe(
      data => {
        this.UserPermissions = data;
        console.log("UserPermissions ", this.UserPermissions);
        if (this.UserPermissions.length > 0) {
          this.show_btnEdit = true;
          this.show_btnAdd = false;
          this.show_DivAddUpdate = false;
          this.show_DivInfo = true;
        }
        if (this.UserPermissions.length == 0) {
          this.show_btnEdit = false;
          this.show_btnAdd = true;
          this.show_DivAddUpdate = false;
          this.show_DivInfo = false;
        }
      },
      error => {
        this.showError("Error in getting user ", "Error");
      }
    );
  }

  onViewCheckboxChange(option, event) {
    if (event.target.checked) {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fView = 1;
          return;
        }
      }
    } else {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fView = 0;
          return;
        }
      }
    }
  }
  onNewCheckboxChange(option, event) {
    if (event.target.checked) {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fNew = 1;
          return;
        }
      }
    } else {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fNew = 0;
          return;
        }
      }
    }
  }
  onEditCheckboxChange(option, event) {
    if (event.target.checked) {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fEdit = 1;
          return;
        }
      }
    } else {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fEdit = 0;
          return;
        }
      }
    }
  }
  onDeleteCheckboxChange(option, event) {
    if (event.target.checked) {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fDelete = 1;
          return;
        }
      }
    } else {
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        if (this.AddUpdateUserPermissions[i].Modulename == option.Modulename) {
          this.AddUpdateUserPermissions[i].fDelete = 0;
          return;
        }
      }
    }
  }

  onSubmit() {
    if (this.userKey == "0") {
      this.showError("Please select User!", "User");
      return;
    }

    if (this.IsNew) {

      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {     
          this.AddUpdateUserPermissions[i].UserKey = this.userKey;;          
        }

      this.userpermission
        .AddUserPermissions(this.AddUpdateUserPermissions)
        .subscribe(
          () => {
            this.showSuccess("Permissions Created", "Add Permission");
          },
          error => {
            this.showError("Error in Creation", "Error");
          }
        );
    } else {
      this.userpermission
        .UpdateUserPermissions(this.AddUpdateUserPermissions)
        .subscribe(
          () => {    
            this.showSuccess("Permissions Updated.", "Update Permission");        
            this.getPermissionsByUser(this.userKey);
            this.show_AddCancel=false;
          },
          error => {
            this.showError("Error in Update", "Error");
          }
        );
    }
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
}
