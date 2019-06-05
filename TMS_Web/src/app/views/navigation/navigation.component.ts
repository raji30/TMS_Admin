import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../_models/login';
import { DeliveryOrderService } from '../../_services/deliveryOrder.service';
import { AuthenticationService } from '../../_services/authentication.service';
import { UserService } from '../../_services/user.service';
import { Loginresult } from '../../_models/loginresult';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  currentUser: Loginresult;
  currentUserSubscription: Subscription; 

  Orderlist:any;
  loginInfo:Login = {
    first_name:'Andrew',
    last_name:'Yang',
    avatar:'ay.jpeg',
    title:'Senior Developer'
};
  orderkey: string='';
  // @Input() loginInfo:Login;

  constructor(private service: DeliveryOrderService,private router: Router,
    private authenticationService: AuthenticationService,private userService: UserService) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);   
  }

  ngOnInit() {
    this.service.getOrderlist().subscribe(data => this.Orderlist = data,  
      error => console.log(error),  
      () => console.log('Get OrderList complete',this.Orderlist));
  }  
//   navigate(orderkey:string)
//   {
//   console.log(orderkey);
// this.router.navigate(['/tab', orderkey]);
 
//   }
}
