import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import{RouterModule,Routes} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
// import { SocialLoginModule } from "angular4-social-login";
// import { AuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider } from 'angular4-social-login';
import { AppComponent } from './app.component';
import {HomeComponent} from '../app/home/home';
import {LoginComponent} from '../app/login/login.component';
import {OrderTypeComponent} from '../app/Order/orderType.component';
import {OrderListComponent} from '../app/Order/orderlist.component';
import {routes} from './app.routing';
import{ OrderComponent} from '../app/Order/order.component';
import {RollerOrderComponent} from '../app/rollerorder/rollerorder.component';
import{NotAuthorisedComponent} from '../app/erropages/unauthorised';
import {AuthenticationService} from '../app/services/auth.service';
import {MiscellaneousService} from '../app/services/misc.service';
import {OrderMiscService} from '../app/services/order.service';
// let config = new AuthServiceConfig([
//     {
//       id: GoogleLoginProvider.PROVIDER_ID,
//       provider: new GoogleLoginProvider("69036395730-22jg334m029vcak2qvbdt610ra391ooe.apps.googleusercontent.com")
//     },
//     // {
//     //   id: FacebookLoginProvider.PROVIDER_ID,
//     //   provider: new FacebookLoginProvider("Facebook-App-Id")
//     // }
//   ]);
//   export function provideConfig() {
//     return config;
//   }
@NgModule({
  imports: [ BrowserModule, FormsModule,ReactiveFormsModule,RouterModule.forRoot(routes),
    // SocialLoginModule.initialize(config)
    HttpClientModule
],
  declarations: [ AppComponent
    ,OrderComponent,OrderTypeComponent,HomeComponent,LoginComponent,OrderListComponent
    ,RollerOrderComponent, NotAuthorisedComponent
],
  bootstrap: [ AppComponent ], 
  providers:[AuthenticationService,MiscellaneousService,OrderMiscService
    // {
    //     provide: AuthServiceConfig,
    //     useFactory: provideConfig
    //   }
]
})

export class AppModule {}