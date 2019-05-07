import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { LoginComponent } from './views/login/login.component';
import { DOIntakeComponent } from './views/dointake/dointake.component';
import { OrderlistComponent } from './views/orderlist/orderlist.component';
import { OrderinfoComponent } from './views/child/orderinfo/orderinfo.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },

  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: './views/dashboard/dashboard.module#DashboardModule'
      },
      {
        path :'doIntake',
        component : DOIntakeComponent
      },
      {
        path :'doIntake/:order',
        component : DOIntakeComponent
      },
      {
        path :'orderList',
        component : OrderlistComponent
      },
      {
        path :'orderinfo/:order',
        component : OrderinfoComponent
      },
     
      // {
      //   path: 'login',
      //   loadChildren: './views/login/login.module#LoginModule'
      // }
   ]
  },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
