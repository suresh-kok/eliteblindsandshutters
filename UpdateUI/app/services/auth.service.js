"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var http_1 = require("@angular/common/http");
// import {AuthService} from 'angular4-oauth-login';
var User = /** @class */ (function () {
    function User() {
    }
    return User;
}());
exports.User = User;
//   var users = [
//     new User('admin@admin.com','adm9'),
//     new User('user1@gmail.com','a23')
//   ];
var AuthenticationService = /** @class */ (function () {
    function AuthenticationService(route, http) {
        this.route = route;
        this.http = http;
        this.showMenu = false;
        this.rootUrl = "http://localhost:62253/api/Customer/";
        this.loginUrl = this.rootUrl + "CheckLogin/";
        this.registerUrl = this.rootUrl + "PostCustomer/";
    }
    AuthenticationService.prototype.logout = function () {
        localStorage.removeItem("user");
        this.showMenu = false;
        this.route.navigate(['']);
    };
    AuthenticationService.prototype.login = function (user) {
        return this.http.get(this.loginUrl + user.Email + "/" + user.Password);
        //  return false;
    };
    AuthenticationService.prototype.checkCredentials = function () {
        if (localStorage.getItem("user") === null) {
            this.route.navigate(['']);
        }
    };
    AuthenticationService.prototype.GetCustomerId = function () {
        var cust = localStorage.getItem("user");
        if (cust != null) {
            var temp = JSON.parse(cust);
            return temp.CustomerID;
        }
    };
    AuthenticationService.prototype.GetCustomerRoleId = function () {
        var cust = localStorage.getItem("user");
        if (cust != null) {
            var temp = JSON.parse(cust);
            return temp.RoleID;
        }
    };
    AuthenticationService.prototype.canActivate = function () {
        if (localStorage.getItem("user") === null) {
            this.route.navigate(['/unauthorized']);
            return false;
        }
        else {
            return true;
        }
    };
    AuthenticationService.prototype.NewUser = function (user) {
        debugger;
        var headers = new http_1.HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
        var strUser = JSON.stringify(user);
        // let options = new RequestOptions({ headers: headers });
        // var data =JSON.stringify(user);
        return this.http.post(this.registerUrl, JSON.stringify(user), { headers: headers });
        //  return false;
    };
    AuthenticationService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [router_1.Router, http_1.HttpClient])
    ], AuthenticationService);
    return AuthenticationService;
}());
exports.AuthenticationService = AuthenticationService;
//# sourceMappingURL=auth.service.js.map