import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts/ng2-charts';

import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { CommonModule } from '@angular/common'; 
@NgModule({
  imports: [
    FormsModule,CommonModule,
    DashboardRoutingModule,
    ChartsModule
  ],
  declarations: [ DashboardComponent ]
})
export class DashboardModule { }
