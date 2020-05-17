import { Component, OnInit } from "@angular/core";
import { User } from "../../../_models/User";
import { UserService } from "../../../_services/user.service";
import { ToastrService } from "ngx-toastr";
import { UserPermissions } from "../../../_models/UserPermissions";
import { UserpermissionService } from "../../../_services/userpermission.service";
import { UserRole } from "../../../_models/UserRole";

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
  roles: UserRole[];
  role: UserRole;
  UserPermissions: UserPermissions[];
  AddUpdateUserPermissions: UserPermissions[];
  temp_AddUpdateUserPermissions: UserPermissions[];
  

  show_DivAddUpdate: boolean = false;
  show_DivInfo: boolean = false;
  show_btnAdd: boolean = false;
  show_btnEdit: boolean = false;
  show_AddCancel: boolean = false;

  userKey: string;
  roleKey: string;
  roleDescription: string;
  IsNew: boolean;
  searchText: string;

  ngOnInit() {
    this.loadAllUsers();
    this.loadMenus();
    this.loadRoles();
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
  loadMenus() {
    this.userpermission.getMenus().subscribe(
      data => {
        this.AddUpdateUserPermissions = this.temp_AddUpdateUserPermissions = data;
        console.log("AddUpdateUserPermissions ", this.AddUpdateUserPermissions);
      },
      error => {
        this.showError("Error in getting user ", "Error");
      }
    );
  }
  loadRoles() {
    this.userpermission.getRoles().subscribe(
      data => (this.roles = data),
      error => {
        this.showError("Error in getting Roles ", "Error");
      },
      () => console.log("User roles", this.roles)
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

    this.AddUpdateUserPermissions = this.UserPermissions;
  }

  cancel() {
    this.show_AddCancel = false;
    this.show_DivAddUpdate = false;
    this.show_DivInfo = true;

    this.AddUpdateUserPermissions = null;
    this.AddUpdateUserPermissions = new Array<UserPermissions>();
  }
  clear() {
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
    this.getUserRoleByUserkey(UserKey);     
    this.getPermissionsByUser(UserKey);
  }

  drpRoles_ChangedEvent(RoleKey: any) {
    if (RoleKey == 0) {
      return;
    }
    this.roleKey = RoleKey;
  }

  getPermissionsByUser(UserKey: string) {
    this.userpermission.getpermissionsByuserkey(UserKey).subscribe(
      data => {
        this.UserPermissions = data;
        console.log("UserPermissions ", this.UserPermissions);
        this.show_AddCancel = false;
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

  getUserRoleByRolekey(RoleKey: string) {
    this.userpermission.getUserRoleByRolekey(RoleKey).subscribe(
      data => {
        this.role = data;
      },
      error => {
        this.showError("Error in getting role ", "Error");
      }
    );
  }

  getUserRoleByUserkey(UserKey: string) {
    this.userpermission.getUserRoleByUserkey(UserKey).subscribe(
      data => {
        this.role = data;
         this.role.description = this.roles.find(x => x.rolekey == this.role.rolekey).description;
    //this.role.rolekey = test.rolekey;
  //  this.role.description = test.description;
        console.log("Get_Role_ByUserkey",  this.role );
      },
      error => {
        this.showError("Error in getting role ", "Error");
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
      this.showError("Please select User", "User");
      return;
    }
    if (this.roleKey == "0" || this.roleKey == undefined) {
      this.showError("Please select Role.", "User");
      return;
    }

    var role: any = {};
    role.userkey = this.userKey;
    role.rolekey = this.roleKey;

    if (this.IsNew) {
      //Adding role
      this.userpermission.AddUserRole(role).subscribe(
        () => {
          // this.showSuccess("Permissions Created", "Add Permission");
        },
        error => {
          this.showError("Error in Creation", "Error");
          return;
        }
      );

      //adding user permissions
      for (var i = 0; i < this.AddUpdateUserPermissions.length; i++) {
        this.AddUpdateUserPermissions[i].UserKey = this.userKey;
      }

      this.userpermission
        .AddUserPermissions(this.AddUpdateUserPermissions)
        .subscribe(
          () => {
            this.AddUpdateUserPermissions = null;
            this.AddUpdateUserPermissions = new Array<UserPermissions>();
            this.AddUpdateUserPermissions = this.temp_AddUpdateUserPermissions;
          },
          error => {
            this.showError("Error in Creation", "Error");
            return;
          }
        );

      this.showSuccess("Roles & Permission created.", "Roles & Permission");
    } else {
      //Updating User Role
      this.userpermission.UpdateUserRole(role).subscribe(
        () => {
          // this.showSuccess("Permissions Created", "Add Permission");
        },
        error => {
          this.showError("Error in Update", "Error");
        }
      );

      this.userpermission
        .UpdateUserPermissions(this.AddUpdateUserPermissions)
        .subscribe(
          () => {
            this.AddUpdateUserPermissions = null;
            this.AddUpdateUserPermissions = new Array<UserPermissions>();
            this.AddUpdateUserPermissions = this.temp_AddUpdateUserPermissions;

            this.getPermissionsByUser(this.userKey);
          },
          error => {
            this.showError("Error in Update", "Error");
          }
        );

      this.showSuccess("Permissions Updated.", "Update Permission");
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
