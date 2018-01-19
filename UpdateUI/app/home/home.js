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
var auth_service_1 = require("../../app/services/auth.service");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(service, orderService) {
        this.service = service;
        this.orderService = orderService;
        this.ViewDetailsBool = false;
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.service.checkCredentials();
        this.CustomerID = this.service.GetCustomerId();
        this.GetOrders();
    };
    HomeComponent.prototype.GetOrders = function () {
        var _this = this;
        this.orderService.GetOrders(this.CustomerID, "0", "0", "OrderDate").subscribe(function (data) {
            _this.OrderInfoList = JSON.parse(data.toString());
        }, function (err) { return console.error(err); });
    };
    HomeComponent.prototype.ViewDetails = function (OrderID) {
        var _this = this;
        debugger;
        this.OrderItem = this.OrderInfoList.find(function (f) { return f.OrderID == OrderID; });
        this.orderService.GetOrderDetails(OrderID).subscribe(function (data) {
            if (data)
                _this.OrderDetails = JSON.parse(data.toString());
            _this.ViewDetailsBool = true;
        }, function (err) { return console.error(err); });
    };
    HomeComponent.prototype.ReturnToAllOrders = function () {
        this.ViewDetailsBool = false;
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'home',
            templateUrl: 'app/home/home.html'
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthenticationService, Object])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.js.map