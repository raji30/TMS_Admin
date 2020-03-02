import { Component, Input } from "@angular/core";
import { navItems } from "./../../_nav";
import { Login } from "../../_models/login";
import { AuthenticationService } from "../../_services/authentication.service";
import { Router, ActivatedRoute } from "@angular/router";
import { Loginresult } from "../../_models/loginresult";
import { Subscription } from "rxjs";
import { UserService } from "../../_services/user.service";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";

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

  currentUser: Loginresult;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService
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
  }

  changeColor(menu: string) {
    this.refreshColor();
    if (menu == "Dashboard") {
      this.lblDashboardColor = "cornflowerblue" ;
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
