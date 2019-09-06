import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDashboardComponent } from './order-dashboard.component';
import { TabComponent } from '../tab/tab.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [OrderDashboardComponent,TabComponent]
})
export class OrderDashboardModule { }
