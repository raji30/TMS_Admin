import { Component, Input } from "@angular/core";
import { navItems } from "./../../_nav";
import { Login } from "../../_models/login";
import { AuthenticationService } from "../../_services/authentication.service";
import { Router, ActivatedRoute } from "@angular/router";
import { Loginresult } from "../../_models/loginresult";
import { Subscription } from "rxjs";
import { UserService } from "../../_services/user.service";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { UserpermissionService } from "../../_services/userpermission.service";
import { UserPermissions } from "../../_models/UserPermissions";

@Component({
  selector: "app-dashboard",
  templateUrl: "./default-layout.component.html"
})
export class DefaultLayoutComponent {
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;
  public menuColor: string;

  public lblDashboardColor: string;
  public lblOrdersColor: string;
  public lblSchedulersColor: string;
  public lblDispatchColor: string;
  public lblInvoiceColor: string;
  public lblAdminColor: string;

  public UserPermissions: UserPermissions[];
  public currentUser: Loginresult;

  private Show_Dashboard: boolean = false;
  private Show_Orders: boolean = false;
  private Show_Scheduler: boolean = false;
  private Show_Dispatch: boolean = false;
  private Show_Invoice: boolean = false;
  private Show_Admin: boolean = false;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private userpermission: UserpermissionService
  ) {
    this.changes = new MutationObserver(mutations => {
      this.sidebarMinimized = document.body.classList.contains(
        "sidebar-minimized"
      );
    });

    this.changes.observe(<Element>this.element, {
      attributes: true
    });

    this.authenticationService.currentUser.subscribe(
      x => (this.currentUser = x)
    );

   
   // }
  }

  ngOnInit() {
    this.getPermissionsByUser(this.currentUser.userId);

    console.log("current UserId:",this.currentUser.userId);
    console.log("User Permissions:",this.UserPermissions);
    //if (this.UserPermissions.length > 0) {
    //  for (var i = 0; i < this.UserPermissions.length; i++) {
       
        //DASHBOARD menu show or hide
        // if (this.UserPermissions[i].Modulename.toUpperCase() == "DASHBOARD") {
        //   if (this.UserPermissions[i].fView == 1) {
        //     this.Show_Dashboard = true;
        //   } else {
        //     this.Show_Dashboard = false;
        //   }
        // }

        //ORDERS menu show or hide
        // if (this.UserPermissions[i].Modulename.toUpperCase() == "DOINTAKE") {
        //   if (this.UserPermissions[i].fView == 1) {
        //     this.Show_Orders = true;
        //   } else {
        //     this.Show_Orders = false;
        //   }
        // }

        //SHEDULER menu show or hide
        // if (this.UserPermissions[i].Modulename.toUpperCase() == "SCHEDULING") {
        //   if (this.UserPermissions[i].fView == 1) {
        //     this.Show_Scheduler = true;
        //   } else {
        //     this.Show_Scheduler = false;
        //   }
        // }
         //DISPATCH menu show or hide
        //  if (this.UserPermissions[i].Modulename.toUpperCase() == "DISPATCHING") {
        //   if (this.UserPermissions[i].fView == 1) {
        //     this.Show_Dispatch = true;
        //   } else {
        //     this.Show_Dispatch = false;
        //   }
        // }

         //ADMIN menu show or hide
        //  if (this.UserPermissions[i].Modulename.toUpperCase() == "INVOICE") {
        //   if (this.UserPermissions[i].fView == 1) {
        //     this.Show_Invoice = true;
        //   } else {
        //     this.Show_Invoice = false;
        //   }
        // }

         //ADMIN menu show or hide
        //  if (this.UserPermissions[i].Modulename.toUpperCase() == "ADMIN") {
        //   if (this.UserPermissions[i].fView == 1) {
        //     this.Show_Admin = true;
        //   } else {
        //     this.Show_Admin = false;
        //   }
        // }
     // }

  }

  getPermissionsByUser(UserKey: string) {
    this.userpermission.getpermissionsByuserkey(UserKey).subscribe(
      data => {
        this.UserPermissions = data;
        console.log("User Permissions:",this.UserPermissions);
        //if (this.UserPermissions.length > 0) {
        for (var i = 0; i < this.UserPermissions.length; i++) {
       
        //DASHBOARD menu show or hide
        if (this.UserPermissions[i].Modulename.toUpperCase() == "DASHBOARD") {
          if (this.UserPermissions[i].fView == 1) {
            this.Show_Dashboard = true;
          } else {
            this.Show_Dashboard = false;
          }
        }

        //ORDERS menu show or hide
        if (this.UserPermissions[i].Modulename.toUpperCase() == "DOINTAKE") {
          if (this.UserPermissions[i].fView == 1) {
            this.Show_Orders = true;
          } else {
            this.Show_Orders = false;
          }
        }

        //SHEDULER menu show or hide
        if (this.UserPermissions[i].Modulename.toUpperCase() == "SCHEDULING") {
          if (this.UserPermissions[i].fView == 1) {
            this.Show_Scheduler = true;
          } else {
            this.Show_Scheduler = false;
          }
        }
         //DISPATCH menu show or hide
         if (this.UserPermissions[i].Modulename.toUpperCase() == "DISPATCHING") {
          if (this.UserPermissions[i].fView == 1) {
            this.Show_Dispatch = true;
          } else {
            this.Show_Dispatch = false;
          }
        }

         //ADMIN menu show or hide
         if (this.UserPermissions[i].Modulename.toUpperCase() == "INVOICE") {
          if (this.UserPermissions[i].fView == 1) {
            this.Show_Invoice = true;
          } else {
            this.Show_Invoice = false;
          }
        }

         //ADMIN menu show or hide
         if (this.UserPermissions[i].Modulename.toUpperCase() == "ADMIN") {
          if (this.UserPermissions[i].fView == 1) {
            this.Show_Admin = true;
          } else {
            this.Show_Admin = false;
          }
        }
     }
      },
      error => {
        //this.showError("Error in getting user ", "Error");
      }
    );
  }

  changeColor(menu: string) {
    this.refreshColor();
    if (menu == "Dashboard") {
      this.lblDashboardColor = "cornflowerblue";
    } else if (menu == "Orders") {
      this.lblOrdersColor = "cornflowerblue";
    } else if (menu == "Scheduler") {
      this.lblSchedulersColor = "cornflowerblue";
    } else if (menu == "Dispatch") {
      this.lblDispatchColor = "cornflowerblue";
    } else if (menu == "Invoice") {
      this.lblInvoiceColor = "cornflowerblue";
    } else if (menu == "Admin") {
      this.lblAdminColor = "cornflowerblue";
    }
  }
  refreshColor() {
    this.lblDashboardColor = this.lblOrdersColor = this.lblSchedulersColor = this.lblDispatchColor = this.lblInvoiceColor = this.lblAdminColor =
      "white";
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(["/login"]);
  }
}
