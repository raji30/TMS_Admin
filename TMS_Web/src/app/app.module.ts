import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { ModalModule, ProgressbarModule } from "ngx-bootstrap";
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';
// Import containers
import { DefaultLayoutComponent } from './containers';
const APP_CONTAINERS = [  DefaultLayoutComponent];
import {  AppAsideModule,  AppBreadcrumbModule,  AppHeaderModule,  AppFooterModule,  AppSidebarModule,} from '@coreui/angular';
// Import routing module
import { AppRoutingModule } from './app.routing';
// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { LoginComponent } from './views/login/login.component';
import { DOIntakeComponent } from './views/dointake/dointake.component';
import { AddressComponent } from './views/child/address/address.component';
import { BillingratesComponent } from './views/child/billingrates/billingrates.component';
import { UserService } from './_services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { AppSettings } from './_constants/appsettings';
import { CustomerComponent } from './views/child/_address/customer/customer.component';
import { AddressService } from './_services/address.service';
import { ContainerComponent } from './views/child/container/container.component';
import { BrokerComponent } from './views/child/_broker/broker.component';

import {A11yModule} from '@angular/cdk/a11y';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {PortalModule} from '@angular/cdk/portal';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {CdkStepperModule} from '@angular/cdk/stepper';
import {CdkTableModule} from '@angular/cdk/table';
import {CdkTreeModule} from '@angular/cdk/tree';
import { OrderlistComponent } from './views/orderlist/orderlist.component';
import { OrderinfoComponent } from './views/child/orderinfo/orderinfo.component';
import { ContainersizeComponent } from './views/child/containersize/containersize.component';
import { TabComponent } from './views/tab/tab.component';
import { SchedulerComponent } from './views/scheduler/scheduler.component';
import { NavigationComponent } from './views/navigation/navigation.component';
import { FileSelectDirective } from 'ng2-file-upload';
import { ToastrModule } from 'ngx-toastr';
import {MatButtonModule, MatCheckboxModule, MatInputModule} from '@angular/material';
import { FileuploadComponent } from './views/fileupload/fileupload.component';
import { OrderDashboardComponent } from './views/order-dashboard/order-dashboard.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CompanyComponent } from './views/child/_company/company/company.component';
import { DispatchComponent } from './views/dispatch/dispatch.component';


@NgModule({
  imports: [
    NgbModule,   
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ProgressbarModule.forRoot(),
    ToastrModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    FormsModule,
    ModalModule.forRoot(),
    HttpClientModule,
    A11yModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    DragDropModule,
    PortalModule,
    ScrollingModule ,MatButtonModule,MatInputModule
    
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    LoginComponent,
    OrderDashboardComponent,
    DOIntakeComponent,
    AddressComponent,
    CustomerComponent, CompanyComponent,  
    BrokerComponent,
    BillingratesComponent,
    ContainerComponent,
    OrderlistComponent,
    OrderinfoComponent,
    ContainersizeComponent, TabComponent,SchedulerComponent,NavigationComponent,FileuploadComponent,DispatchComponent,
    FileSelectDirective 
  ],
  providers:  [UserService,AddressService,DatePipe ],
  bootstrap: [ AppComponent],
  entryComponents:[TabComponent]
 
})
export class AppModule { }