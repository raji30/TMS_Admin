import { Component, OnInit } from '@angular/core';
import { DeliveryOrderService } from '../../_services/deliveryOrder.service';
import { DeliveryOrderHeader } from '../../_models/DeliveryOrderHeader';
import { Router, ActivatedRoute } from '@angular/router';
import { Order_details } from '../../_models/order_details';


@Component({
  selector: 'app-order-dashboard',
  templateUrl: './order-dashboard.component.html',
  styleUrls: ['./order-dashboard.component.scss']
})
export class OrderDashboardComponent implements OnInit {
  Orderlist:any;
  orderKey:string;
  orderkey1: string;
  public order: DeliveryOrderHeader;
  public orderinfo: Order_details[];
  
  constructor(private service: DeliveryOrderService , private router: Router,private route: ActivatedRoute) {  }

  ngOnInit() {
    this.service.getOrderlist().subscribe(data => this.Orderlist = data,  
      error => console.log(error),  
      () => console.log('Get OrderList complete',this.Orderlist));
  }  

  viewOrder(orderKey:string)
  {
    //this.orderKey='399ba232-5c32-11e9-be2b-6b37a32de01c';
    this.router.navigate(['/doIntake', orderKey]); 
   //this.router.navigate(['/tab',orderKey]); 
  }
  navigatetoTab(orderKey:string)
  {
    //this.orderKey='399ba232-5c32-11e9-be2b-6b37a32de01c';
    this.router.navigate(['/tab', orderKey]); 
   //this.router.navigate(['/tab',orderKey]); 
  }
  viewOrderinfo(orderParams)
  {
    this.order = orderParams;
    //this.orderKey='399ba232-5c32-11e9-be2b-6b37a32de01c';
    //this.router.navigate(['/orderinfo', this.order.OrderKey]); 

    if ( this.order.OrderKey != undefined) {    
      this.service.GetbyKey(this.order.OrderKey  ).subscribe(data => {
        (this.order = data),console.log("testing Model----",this.order),
          error => console.log(error)          
      });

      this.service
        .GetOrderDetailsbyKey(this.order.OrderKey)
        .subscribe(
          data => (this.orderinfo = data),
          error => console.log(error),
          () => console.log("Get OrderDetail", this.orderinfo)
        );
    }
  }
 
}


