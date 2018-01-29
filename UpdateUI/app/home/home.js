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
var order_service_1 = require("../../app/services/order.service");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(service, orderService) {
        this.service = service;
        this.orderService = orderService;
        this.ViewDetailsBool = false;
        this.Criteria = "select";
        this.Value = "";
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.service.checkCredentials();
        debugger;
        this.CustomerID = this.service.GetCustomerId();
        this.RoleID = this.service.GetCustomerRoleId();
        this.GetOrders();
    };
    HomeComponent.prototype.GetOrders = function () {
        var _this = this;
        this.orderService.GetOrders(this.CustomerID, this.Criteria, this.Value == "" ? "0" : this.Value, "OrderDate").subscribe(function (data) {
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
    HomeComponent.prototype.Search = function () {
        this.GetOrders();
    };
    HomeComponent.prototype.ReturnToAllOrders = function () {
        this.ViewDetailsBool = false;
    };
    HomeComponent.prototype.Approve = function (OrderID) {
        var _this = this;
        if (this.RoleID == 1) {
            this.orderService.ApproveOrder(OrderID).subscribe(function (data) {
                if (data) {
                    _this.ChangeOrderStatus(OrderID, 3, true);
                }
            });
        }
        else if (this.RoleID == 2) {
            this.ChangeOrderStatus(OrderID, 10, true);
        }
        else if (this.RoleID == 3) {
            this.ChangeOrderStatus(OrderID, 11, true);
        }
    };
    HomeComponent.prototype.Reject = function (OrderID) {
        if (this.RoleID == 1) {
            this.ChangeOrderStatus(OrderID, 7, false);
        }
        else if (this.RoleID == 2) {
            this.ChangeOrderStatus(OrderID, 8, false);
        }
        else if (this.RoleID == 3) {
            this.ChangeOrderStatus(OrderID, 9, false);
        }
    };
    HomeComponent.prototype.ChangeOrderStatus = function (OrderID, status, isApprove) {
        var _this = this;
        this.orderService.ChangeOrderStatus(OrderID, status).subscribe(function (data) {
            if (data && isApprove) {
                alert('Order ' + OrderID + 'approved successfully');
                _this.ViewDetailsBool = false;
            }
            else if (data && !isApprove) {
                alert('Order ' + OrderID + 'rejected successfully');
                _this.ViewDetailsBool = false;
            }
        });
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'home',
            templateUrl: 'app/home/home.html'
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthenticationService, order_service_1.OrderMiscService])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.js.map