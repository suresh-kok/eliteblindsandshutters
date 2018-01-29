import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import{RouterModule,Routes} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import { AppComponent } from './app.component';
import {HomeComponent} from '../app/home/home';
import {LoginComponent} from '../app/login/login.component';
import {OrderTypeComponent} from '../app/Order/orderType.component';
import {OrderListComponent} from '../app/Order/orderlist.component';
import {RollerOrderListComponent} from '../app/rollerorder/rollerorderlist.component';
import {FabricUtilityComponent} from '../app/fabricutilityorder/fabricutilityorder.component'
import {routes} from './app.routing';
import{ OrderComponent} from '../app/Order/order.component';
import{ OverSeaComponent} from '../app/overseaorder/overseaorder.component';
import{ OverSeaOrderListComponent} from '../app/overseaorder/overseaorderlist.component';
import {RollerOrderComponent} from '../app/rollerorder/rollerorder.component';
import {DefaultDatePipe} from '../app/pipes/defaultdate.pipe';
import{NotAuthorisedComponent} from '../app/erropages/unauthorised';
import {AuthenticationService} from '../app/services/auth.service';
import {MiscellaneousService} from '../app/services/misc.service';
import {OrderMiscService} from '../app/services/order.service';

@NgModule({
  imports: [ BrowserModule, FormsModule,ReactiveFormsModule,RouterModule.forRoot(routes),
    HttpClientModule
],
  declarations: [ AppComponent
    ,OrderComponent,OrderTypeComponent,HomeComponent,LoginComponent,OrderListComponent
    ,OverSeaComponent,OverSeaOrderListComponent,RollerOrderComponent,RollerOrderListComponent,DefaultDatePipe,
    FabricUtilityComponent,NotAuthorisedComponent
],
  bootstrap: [ AppComponent ], 
  providers:[AuthenticationService,MiscellaneousService,OrderMiscService

]
})

export class AppModule {}