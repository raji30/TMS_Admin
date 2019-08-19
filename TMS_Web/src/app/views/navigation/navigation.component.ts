import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { Login } from "../../_models/login";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { AuthenticationService } from "../../_services/authentication.service";
import { UserService } from "../../_services/user.service";
import { Loginresult } from "../../_models/loginresult";
import { Subscription } from "rxjs";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { TabComponent } from "../tab/tab.component";
import { Order_details } from "../../_models/order_details";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";

@Component({
  selector: "app-navigation",
  templateUrl: "./navigation.component.html",
  styleUrls: ["./navigation.component.scss"]
})
export class NavigationComponent implements OnInit {
  currentUser: Loginresult;
  currentUserSubscription: Subscription;
  
  Orderlist: Array<DeliveryOrderHeader> = [];
  schedulerlist:Array<Order_details> = [];
  dispatchAssignmentlist:Array<Order_details> = [];
  dispatchDeliverylist:Array<Order_details> = [];

  ModalOrderKey: string;
  show:boolean=true;

  constructor(
    private service: DeliveryOrderService,
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private modalService: NgbModal,
  ) {
    this.authenticationService.currentUser.subscribe(
      x => (this.currentUser = x)
    );
  }

  ngOnInit() {
    this.service
      .getOrderlist()
      .subscribe(
        data => (this.Orderlist = data),
        error => console.log(error),
        () => console.log("Get OrderList complete", this.Orderlist)
      );
  }
  test(type:number)
  {
    alert();
  }
  //   navigate(orderkey:string)
  //   {
  //   console.log(orderkey);
  // this.router.navigate(['/tab', orderkey]);

  //   }

  open(orderParams) {
    // this.order = orderParams;
     this.ModalOrderKey = orderParams;
        const modalRef = this.modalService.open(TabComponent,{ size:'xl',backdrop:true, windowClass : 'myCustomModalClass'});
       modalRef.componentInstance.orderKeyinput =   this.ModalOrderKey;  
   }
}
