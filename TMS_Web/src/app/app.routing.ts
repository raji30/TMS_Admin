import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

// Import Containers
import { DefaultLayoutComponent } from "./containers";
import { LoginComponent } from "./views/login/login.component";
import { DOIntakeComponent } from "./views/dointake/dointake.component";
import { OrderlistComponent } from "./views/orderlist/orderlist.component";
import { OrderinfoComponent } from "./views/child/orderinfo/orderinfo.component";
import { TabComponent } from "./views/tab/tab.component";
import { ContainerComponent } from "./views/child/container/container.component";
import { ListcustomerComponent } from "./views/_customer/list-customer/listcustomer/listcustomer.component";
import { SchedulerComponent } from "./views/scheduler/scheduler.component";
import { SchedulerlistComponent } from "./views/schedulerlist/schedulerlist.component";
import { DispatchComponent } from "./views/dispatch/dispatch.component";
import { DispathdeliveryComponent } from "./views/dispathdelivery/dispathdelivery.component";
import { DispatchAssignmentComponent } from "./views/dispatchAssignment/dispatchAssignment.component";
import { ListcityComponent } from "./views/_master/_city/listcity/listcity.component";
import { ListdriverComponent } from "./views/_master/_driver/listdriver/listdriver.component";
import { VendorlistComponent } from "./views/_master/_vendor/vendorlist/vendorlist/vendorlist.component";
import { BrokerlistComponent } from "./views/_master/_broker/brokerlist/brokerlist/brokerlist.component";
import { CarrierlistComponent } from "./views/_master/_carrier/carrierlist/carrierlist.component";
import { InvoiceComponent } from "./views/invoice/invoice/invoice.component";
import { UserlistComponent } from "./views/_master/_user/userlist/userlist.component";
import { RatesheetlistComponent } from "./views/_master/_ratesheet/ratesheetlist/ratesheetlist.component";
import { ItemlistComponent } from "./views/_master/_item/itemlist/itemlist.component";
import { ContainerStatusComponent } from './views/container-status/container-status.component';
import { BaseratelistComponent } from './views/_master/_baserate/baseratelist/baseratelist.component';
import { TestComponent } from './views/test/test.component';
import { CompanylistComponent } from './views/_master/_company/companylist/companylist.component';
import { UserpermissionsComponent } from './views/_master/userpermissions/userpermissions.component';

export const routes: Routes = [
  {
    path: "",
    redirectTo: "login",
    pathMatch: "full"
  },
  {
    path: "",
    component: DefaultLayoutComponent,
    data: {
      title: "Home"
    },
    children: [
      {
        path: "dashboard",
        // loadChildren: './views/dashboard/dashboard.module#DashboardModule',
        component: OrderlistComponent
      },
      {
        path: "doIntake",
        component: DOIntakeComponent
      },
      {
        path: "doIntake/:order",
        component: DOIntakeComponent
      },
      {
        path: "orderList",
        component: OrderlistComponent
      },
      {
        path: "orderinfo/:order",
        component: OrderinfoComponent
      },      
      {
        path: "Containers",
        component: ContainerStatusComponent
      },
      {
        path: "Scheduler",
        component: SchedulerlistComponent
      },
      {
        path: "DispatchAssignment",
        component: DispatchAssignmentComponent
      },
      {
        path: "DispatchDelivery",
        component: DispathdeliveryComponent
      },
      {
        path: "Customers",
        component: ListcustomerComponent
      },
      {
        path: "Company",
        component: CompanylistComponent
      },
      
      {
        path: "RateSheet",
        component: RatesheetlistComponent
      },      
      {
        path: "Items",
        component: ItemlistComponent
      },
      {
        path: "Drivers",
        component: ListdriverComponent
      },
            {
        path: "Vendors",
        component: VendorlistComponent
      },      
      {
        path: "Brokers",
        component: BrokerlistComponent
      },
      {
        path: "Users",
        component: UserlistComponent
      },      
      {
        path: "UserPermissions",
        component: UserpermissionsComponent
      },
      {
        path: "Carriers",
        component: CarrierlistComponent
      },      
      {
        path: "City",
        component: ListcityComponent
      },
      {
        path: "GetOrderstoGenerateInvoice",
        component: InvoiceComponent
      },      
      
      {
        path: "BaseRate",
        component: BaseratelistComponent
      },
      
      {
        path: "TestComponent",
        component: TestComponent
      },
      { path: "tab/:order", component: TabComponent },
      { path: "tab", component: TabComponent }
      // {
      //   path: 'login',
      //   loadChildren: './views/login/login.module#LoginModule'
      // }
    ]
  },
  { path: "login", component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
