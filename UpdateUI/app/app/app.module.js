"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var http_1 = require("@angular/common/http");
var app_component_1 = require("./app.component");
var home_1 = require("../app/home/home");
var login_component_1 = require("../app/login/login.component");
var orderType_component_1 = require("../app/Order/orderType.component");
var orderlist_component_1 = require("../app/Order/orderlist.component");
var fabricutilityorder_component_1 = require("../app/fabricutilityorder/fabricutilityorder.component");
var app_routing_1 = require("./app.routing");
var order_component_1 = require("../app/Order/order.component");
var overseaorder_component_1 = require("../app/overseaorder/overseaorder.component");
var overseaorderlist_component_1 = require("../app/overseaorder/overseaorderlist.component");
var rollerorder_component_1 = require("../app/rollerorder/rollerorder.component");
var unauthorised_1 = require("../app/erropages/unauthorised");
var auth_service_1 = require("../app/services/auth.service");
var misc_service_1 = require("../app/services/misc.service");
var order_service_1 = require("../app/services/order.service");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, forms_1.ReactiveFormsModule, router_1.RouterModule.forRoot(app_routing_1.routes),
                http_1.HttpClientModule
            ],
            declarations: [app_component_1.AppComponent,
                order_component_1.OrderComponent, orderType_component_1.OrderTypeComponent, home_1.HomeComponent, login_component_1.LoginComponent, orderlist_component_1.OrderListComponent,
                overseaorder_component_1.OverSeaComponent, overseaorderlist_component_1.OverSeaOrderListComponent, rollerorder_component_1.RollerOrderComponent,
                fabricutilityorder_component_1.FabricUtilityComponent, unauthorised_1.NotAuthorisedComponent
            ],
            bootstrap: [app_component_1.AppComponent],
            providers: [auth_service_1.AuthenticationService, misc_service_1.MiscellaneousService, order_service_1.OrderMiscService
            ]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map