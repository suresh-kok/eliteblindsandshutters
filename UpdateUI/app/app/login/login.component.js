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
var auth_service_1 = require("../../app/services/auth.service");
var misc_service_1 = require("../../app/services/misc.service");
var http_1 = require("@angular/common/http");
var LoginComponent = /** @class */ (function () {
    // GOOGLE_CLIENT_ID:any = '69036395730-22jg334m029vcak2qvbdt610ra391ooe.apps.googleusercontent.com';
    function LoginComponent(route, service, http, miscService) {
        this.route = route;
        this.service = service;
        this.http = http;
        this.miscService = miscService;
        this.showMsg = false;
        this.showSuccessMsg = false;
        this.showSignUp = false;
        this.country = "";
        this.region = "";
        this.city = "";
        this.regionLoaded = false;
        this.showModal = false;
        this.user = new auth_service_1.User();
        // this.LoadCountries();
    }
    LoginComponent.prototype.ngOnInit = function () {
    };
    LoginComponent.prototype.onSignIn = function (googleUser) {
        var user;
        debugger;
        var userAuthToken = googleUser.id_token;
        var userDisplayName = googleUser.getBasicProfile().getName();
        console.log(this);
    };
    ;
    LoginComponent.prototype.LoadCountries = function () {
        var _this = this;
        this.miscService.GetAllCountries().subscribe(function (data) { _this.countries = data; }, function (err) { return console.error(err); });
    };
    LoginComponent.prototype.LoadRegions = function () {
        var _this = this;
        this.region = "";
        this.user.City = "";
        // this.regions=this.miscService.GetRegions(this.country);
        this.miscService.GetRegions(this.user.Country).subscribe(function (data) {
            _this.regions = data;
        }, function (err) { return console.error(err); });
    };
    LoginComponent.prototype.LoadCities = function () {
        var _this = this;
        this.city = "";
        this.miscService.GetCities(this.user.Country, this.region).subscribe(function (data) {
            _this.cities = data;
        }, function (err) { return console.error(err); });
    };
    LoginComponent.prototype.GoogleSignIn = function () {
        // this._googleAuth.signIn(GoogleLoginProvider.PROVIDER_ID);
        // this._googleAuth.authenticateUser((profile)=>{
        //     // let data=profile.
        //     debugger;
        //     console.log(profile);
        //   });
        //     gapi.load('auth2',  () => {
        //    this.auth2= gapi.auth2.init({ 
        //         client_id: this.GOOGLE_CLIENT_ID, 
        //         scope: 'profile email'
        //         // response_type: 'id_token permission'
        //     }, this.onSignIn);
        // }
        // );
    };
    LoginComponent.prototype.signOut = function () {
        gapi.load('auth2', function () {
            debugger;
            var auth = gapi.auth2.getAuthInstance();
        });
    };
    LoginComponent.prototype.Login = function () {
        var _this = this;
        //   let res= this.service.login(this.user);
        //   debugger;
        this.service.login(this.user).subscribe(function (data) {
            debugger;
            if (data) {
                var customer = JSON.parse(data.toString());
                if (customer.CustomerID > 0) {
                    localStorage.setItem("user", data.toString());
                    _this.route.navigate(['Home']);
                }
                else {
                    _this.errorMsg = 'Invalid Credentials';
                    _this.showMsg = true;
                }
            }
            else {
                _this.errorMsg = 'Failed to login';
                _this.showMsg = true;
            }
            return data;
        }, function (err) { return console.error(err); });
    };
    LoginComponent.prototype.ShowLogin = function () {
        this.user = new auth_service_1.User();
        this.showSignUp = false;
    };
    LoginComponent.prototype.SignUp = function () {
        this.user = new auth_service_1.User();
        this.showSignUp = true;
    };
    LoginComponent.prototype.Register = function () {
        var _this = this;
        if (this.Validation() == false) {
            return false;
        }
        this.user.IsActive = 1;
        this.service.NewUser(this.user).subscribe(function (data) {
            if (data) {
                _this.user = new auth_service_1.User();
                alert('Registration Successfull. Please login to continue');
                _this.showSignUp = false;
                // this.showSuccessMsg=true;
                // this.showMsg=false;
            }
            else {
                alert('Error occured. Please try again');
                // this.errorMsg = 'Failed to login';
                // this.showMsg=true;
                // this.showSuccessMsg=false;
            }
        }, function (err) { return console.error(err); });
    };
    LoginComponent.prototype.Validation = function () {
        if (this.user.Password != this.user.ConfirmPassword) {
            alert("Passwords donot match");
            return false;
        }
    };
    LoginComponent = __decorate([
        core_1.Component({
            selector: 'login',
            templateUrl: 'app/login/login.html',
            styleUrls: ['app/login/login.css'],
        }),
        __metadata("design:paramtypes", [router_1.Router, auth_service_1.AuthenticationService,
            http_1.HttpClient, misc_service_1.MiscellaneousService])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map