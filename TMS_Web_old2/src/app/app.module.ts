import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import {
  LocationStrategy,
  HashLocationStrategy,
  DatePipe
} from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from "./app.component";
// Import containers
import { DefaultLayoutComponent } from "./containers";
const APP_CONTAINERS = [DefaultLayoutComponent];
import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule
} from "@coreui/angular";
// Import routing module
import { AppRoutingModule } from "./app.routing";
// Import 3rd party components
import { ChartsModule } from "ng2-charts/ng2-charts";
import { LoginComponent } from "./views/login/login.component";
import { DOIntakeComponent } from "./views/dointake/dointake.component";
import { AddressComponent } from "./views/child/address/address.component";
import { BillingratesComponent } from "./views/child/billingrates/billingrates.component";
import { UserService } from "./_services/user.service";
import { HttpClientModule } from "@angular/common/http";
import { AppSettings } from "./_constants/appsettings";
import { CustomerComponent } from "./views/child/_address/customer/customer.component";
import { AddressService } from "./_services/address.service";
import { ContainerComponent } from "./views/child/container/container.component";
import { BrokerComponent } from "./views/child/_broker/broker.component";
import { OrderlistComponent } from "./views/orderlist/orderlist.component";
import { OrderinfoComponent } from "./views/child/orderinfo/orderinfo.component";
import { ContainersizeComponent } from "./views/child/containersize/containersize.component";
import { TabComponent } from "./views/tab/tab.component";
import { SchedulerComponent } from "./views/scheduler/scheduler.component";
import { NavigationComponent } from "./views/navigation/navigation.component";
import { ToastrModule } from "ngx-toastr";
import { FileuploadComponent } from "./views/fileupload/fileupload.component";
import { NgbModule, NgbAlertModule, NgbPaginationModule } from "@ng-bootstrap/ng-bootstrap";
import { CompanyComponent } from "./views/child/_company/company/company.component";
import { DispatchComponent } from "./views/dispatch/dispatch.component";
import { ListcustomerComponent } from "./views/_customer/list-customer/listcustomer/listcustomer.component";
import { AddcustomerComponent } from "./views/_customer/add-customer/addcustomer/addcustomer.component";
import { DispathdeliveryComponent } from "./views/dispathdelivery/dispathdelivery.component";
import { GrdFilterPipe } from "./_models/grd-filter.pipe";
import { SchedulerlistComponent } from "./views/schedulerlist/schedulerlist.component";
import { OwlDateTimeModule, OwlNativeDateTimeModule } from "ng-pick-datetime";
import { DispatchAssignmentComponent } from "./views/dispatchAssignment/dispatchAssignment.component";
import { ListcityComponent } from "./views/_master/_city/listcity/listcity.component";
import { BlockCopyPaste } from "./common/block-copy-paste";
import { ListdriverComponent } from "./views/_master/_driver/listdriver/listdriver.component";
import { VendorlistComponent } from "./views/_master/_vendor/vendorlist/vendorlist/vendorlist.component";
import { BrokerlistComponent } from "./views/_master/_broker/brokerlist/brokerlist/brokerlist.component";
import { CarrierlistComponent } from "./views/_master/_carrier/carrierlist/carrierlist.component";
import { InvoiceComponent } from "./views/invoice/invoice/invoice.component";
import { UserlistComponent } from "./views/_master/_user/userlist/userlist.component";
import { RatesheetlistComponent } from "./views/_master/_ratesheet/ratesheetlist/ratesheetlist.component";
import { ItemlistComponent } from "./views/_master/_item/itemlist/itemlist.component";
import { FileUploadComponent } from "./views/file-upload/file-upload.component";
import { FileUploaderService } from "./_services/file-uploader.service";
import { FiledownloadService } from "./_services/filedownload.service";
import { PdfviewcomponentComponent } from "./views/pdfviewcomponent/pdfviewcomponent.component";
import { PdfViewerModule } from "ng2-pdf-viewer";
import { PdfJsViewerModule } from "ng2-pdfjs-viewer";
import { ContainerStatusComponent } from "./views/container-status/container-status.component";
import { SchedulerUpdateComponent } from "./views/scheduler-update/scheduler-update.component";
import { FilterPipe } from "./_filter/filter.pipe";
import { BaseratelistComponent } from "./views/_master/_baserate/baseratelist/baseratelist.component";
import { TestComponent } from "./views/test/test.component";
import { GroupByPipe } from "./_models/grdPipe";
import { DispatchupdateComponent } from "./views/dispatchupdate/dispatchupdate.component";
import { CompanylistComponent } from "./views/_master/_company/companylist/companylist.component";
import { NgxPaginationModule } from 'ngx-pagination';
import { UserpermissionsComponent } from './views/_master/userpermissions/userpermissions.component';
import { NgxSpinnerModule } from "ngx-spinner";  

@NgModule({
  imports: [
    NgxSpinnerModule ,
    NgbModule,NgxPaginationModule,NgbAlertModule,NgbPaginationModule,
    PdfViewerModule,
    PdfJsViewerModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    ToastrModule.forRoot(),
    ChartsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
  
      ],
  declarations: [
    FilterPipe,
    AppComponent,
    ...APP_CONTAINERS,
    LoginComponent,
    UserlistComponent,
    DOIntakeComponent,
    AddressComponent,
    CustomerComponent,
    RatesheetlistComponent,
    BaseratelistComponent,
    ItemlistComponent,
    AddcustomerComponent,
    ListcustomerComponent,
    ListcityComponent,
    CompanyComponent,
    CompanylistComponent,
    BrokerComponent,
    VendorlistComponent,
    BrokerlistComponent,
    CarrierlistComponent,
    UserpermissionsComponent,
    ListdriverComponent,
    BillingratesComponent,
    ContainerComponent,
    OrderlistComponent,
    OrderinfoComponent,
    ContainersizeComponent,
    InvoiceComponent,
    TabComponent,
    ContainerStatusComponent,
    SchedulerlistComponent,
    NavigationComponent,
    FileuploadComponent,
    DispatchComponent,
    DispatchAssignmentComponent,
    DispathdeliveryComponent,
    DispatchupdateComponent,
    FileUploadComponent,
    PdfviewcomponentComponent,
    SchedulerUpdateComponent,
    GrdFilterPipe,
    GroupByPipe,
    BlockCopyPaste,
    TestComponent,
    
  ],
  providers: [
    UserService,
    AddressService,
    DatePipe,
    FileUploaderService,
    FiledownloadService
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    TabComponent,
    SchedulerUpdateComponent,
    DispatchupdateComponent
  ]
})
export class AppModule {}
