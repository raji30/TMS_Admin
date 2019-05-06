import { Component, OnInit } from "@angular/core";
import { DeliveryOrderHeader } from "../../../_models/DeliveryOrderHeader";
import { DeliveryOrderService } from "../../../_services/deliveryOrder.service";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-orderinfo",
  templateUrl: "./orderinfo.component.html",
  styleUrls: ["./orderinfo.component.scss"]
})
export class OrderinfoComponent implements OnInit {
  private orderkey: string;
  public orderinfo: DeliveryOrderHeader;
  constructor(
    private service: DeliveryOrderService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.orderinfo = new DeliveryOrderHeader();
    this.orderkey = this.route.snapshot.paramMap.get("order");

    if ( this.orderkey != undefined) {    
      this.service.GetbyKey(this.orderkey ).subscribe(data => {
        (this.orderinfo = data),
          error => console.log(error)          
      });

      this.service
        .GetOrderDetailsbyKey(this.orderkey)
        .subscribe(
          data => (this.orderinfo.orderdetails = data),
          error => console.log(error),
          () => console.log("Get OrderDetail", this.orderinfo.orderdetails)
        );
    }
  }

  ngOnChange()
  {
    this.service
    .GetOrderDetailsbyKey(this.orderkey)
    .subscribe(
      data => (this.orderinfo.orderdetails = data),
      error => console.log(error),
      () => console.log("Get OrderDetail", this.orderinfo.orderdetails)
    );
  }
}
