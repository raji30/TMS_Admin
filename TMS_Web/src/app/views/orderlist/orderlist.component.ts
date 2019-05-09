import { Component, OnInit } from '@angular/core';
import { DeliveryOrderService } from '../../_services/deliveryOrder.service';
import { DeliveryOrderHeader } from '../../_models/DeliveryOrderHeader';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-orderlist',
  templateUrl: './orderlist.component.html',
  styleUrls: ['./orderlist.component.scss']
})
export class OrderlistComponent implements OnInit {
  Orderlist:any;
  orderKey:string;
  orderkey1: string;
  public orderinfo: DeliveryOrderHeader;
  
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
  }
  viewOrderinfo(orderKey1:string)
  {
    this.orderinfo = new DeliveryOrderHeader();
    //this.orderKey='399ba232-5c32-11e9-be2b-6b37a32de01c';
    //this.router.navigate(['/orderinfo', orderKey1]); 

    if ( this.orderkey1 != undefined) {    
      this.service.GetbyKey(this.orderkey1 ).subscribe(data => {
        (this.Orderlist = data),
          error => console.log(error)          
      });

      this.service
        .GetOrderDetailsbyKey(this.orderkey1)
        .subscribe(
          data => (this.Orderlist.orderdetails = data),
          error => console.log(error),
          () => console.log("Get OrderDetail", this.Orderlist.orderdetails)
        );
    }
  }
 
}

