import { Component, Input } from '@angular/core';
import { navItems } from './../../_nav';
import { Login } from '../../_models/login';
import { AuthenticationService } from '../../_services/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Loginresult } from '../../_models/loginresult';
import { Subscription } from 'rxjs';
import { UserService } from '../../_services/user.service';
import { DeliveryOrderService } from '../../_services/deliveryOrder.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
 
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;

  constructor(private router: Router,private authenticationService: AuthenticationService,private userService: UserService) {
    
    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains('sidebar-minimized');
    });

    this.changes.observe(<Element>this.element, {
      attributes: true
    });
  }
  
 

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
}
}
