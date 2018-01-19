"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var order_component_1 = require("../app/Order/order.component");
var home_1 = require("../app/home/home");
var login_component_1 = require("../app/login/login.component");
var rollerorder_component_1 = require("../app/rollerorder/rollerorder.component");
var unauthorised_1 = require("../app/erropages/unauthorised");
exports.routes = [
    { path: '', component: login_component_1.LoginComponent },
    { path: 'Home', component: home_1.HomeComponent },
    { path: 'VenetianOrder', component: order_component_1.OrderComponent },
    { path: 'RollerOrder', component: rollerorder_component_1.RollerOrderComponent },
    { path: 'unauthorized', component: unauthorised_1.NotAuthorisedComponent }
    // {path:'MovieDetails/:id',component:MovieListComponent,canActivate:[AuthService]},
    // {path:'Registration',component:ReactiveUserComponent},
    // {path:'Books',component:BookDataComponent},
    // {path:'unauthorized',component:NotAuthorisedComponent},
    // {path:'Cart',component:CartComponent},
    // {path:'**',component:PageNotFoundComponent}
];
//# sourceMappingURL=app.routing.js.map