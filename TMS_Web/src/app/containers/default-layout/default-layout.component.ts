import { Component, Input } from '@angular/core';
import { navItems } from './../../_nav';
import { Login } from '../../_models/login';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {

  public loginInfo:Login = {
    first_name:'Andrew',
    last_name:'Yang',
    avatar:'ay.jpeg',
    title:'Senior Developer'
};
  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;
  constructor() {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = document.body.classList.contains('sidebar-minimized');
    });

    this.changes.observe(<Element>this.element, {
      attributes: true
    });
  }
}
