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
        path: "Scheduler",
        component: ContainerComponent
      },
      {
        path: "Customers",
        component: ListcustomerComponent
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
