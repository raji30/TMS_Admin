import {
  Component,
  OnInit,
  Input,
  SimpleChanges,
  OnChanges,
  OnDestroy
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { BsModalRef, BsModalService } from "ngx-bootstrap";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { DeliveryOrderService } from "../../_services/deliveryOrder.service";
import { DeliveryOrderHeader } from "../../_models/DeliveryOrderHeader";
import { Order_details } from "../../_models/order_details";
@Component({
  selector: "app-tab",
  templateUrl: "./tab.component.html"
})
export class TabComponent implements OnInit, OnChanges, OnDestroy {
  @Input() orderKeyinput: string;

  public order: DeliveryOrderHeader;
  public orderinfo: Order_details[];

  subscription: Subscription;
  modalRef: BsModalRef;
  constructor(  private service: DeliveryOrderService,
    private router: Router,
    private route: ActivatedRoute,
    private modalService: NgbModal
  ) {
    //alert(this.orderKey);
    // this.orderKey = this.route.snapshot.paramMap.get("order");
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    /* This part will only be accessable on load only. So, it will not be accessable on Reload. */
    // this.ngOnInit();
  }

  ngOnInit() {
    // alert('Tab Initiate:  '+ this.orderKeyinput);
    //this.orderKeyinput = this.route.snapshot.paramMap.get("order");
    // this.orderKey ='43976812-5c31-11e9-be2a-93c1a1c5ac18';

       if (this.orderKeyinput != undefined) {
      this.service.GetbyKey(this.orderKeyinput).subscribe(data => {
        (this.order = data), 
        console.log("testing Model----", this.order);
        this.service
          .GetOrderDetailsbyKey(this.orderKeyinput)
          .subscribe(
            data => (this.orderinfo = data),
            error => console.log(error),
            () => console.log("Get OrderDetail", this.orderinfo)
          ),
          error => console.log(error);
      });
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    // alert('Tab Onchange:  '+ this.orderKeyinput);
    // this.orderKey = this.route.snapshot.paramMap.get("order");
    this.orderKeyinput = this.orderKeyinput; //changes["orderKeyinput"].currentValue;

    //  alert('Tab Onchange2:  '+ this.orderKeyinput);
  }
  ngOnDestroy() {
    this.orderKeyinput = null;
  }
  onClick(event) {
    //alert('TEst');
  }
  
}
