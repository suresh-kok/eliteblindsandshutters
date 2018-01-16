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
var orderdata_1 = require("../DataModels/orderdata");
var orderInitiationData_1 = require("../DataModels/orderInitiationData");
var order_service_1 = require("../../app/services/order.service");
var OrderComponent = /** @class */ (function () {
    function OrderComponent(service, element, orderService) {
        this.service = service;
        this.orderService = orderService;
        this.OrderDetails = [];
        this.enableCheckOut = false;
        this.orderItem = new orderdata_1.OrderData();
        this.OrderInitiation = new orderInitiationData_1.OrderInitiation();
    }
    OrderComponent.prototype.ngOnInit = function () {
        this.service.checkCredentials();
        this.GetCordStyles();
        this.GetSlatStyles();
    };
    OrderComponent.prototype.ngAfterViewInit = function () {
        var el = this.basic.nativeElement;
        el.click();
    };
    OrderComponent.prototype.AddItem = function () {
        debugger;
        this.OrderDetails.push(this.orderItem);
        this.orderItem = new orderdata_1.OrderData();
    };
    OrderComponent.prototype.OrderConfirm = function () {
        debugger;
        // this.element.nativeElement.querySelector('#extraTab').click();
        var el = this.extra.nativeElement;
        // let elDetails: HTMLElement = this.details.nativeElement as HTMLElement;
        // let elHome: HTMLElement = this.home.nativeElement as HTMLElement;
        el.click();
        // elDetails.
        this.enableCheckOut = true;
    };
    OrderComponent.prototype.Proceed = function () {
        var elDetails = this.details.nativeElement;
        elDetails.click();
    };
    OrderComponent.prototype.Delete = function (item) {
        // this.OrderDetails.pop(item);
    };
    OrderComponent.prototype.GetSlatStyles = function () {
        var _this = this;
        this.orderService.GetSlatStyle('Venetian').subscribe(function (data) {
            _this.slatStyles = JSON.parse(data);
        }, function (err) { return console.error(err); });
    };
    OrderComponent.prototype.GetCordStyles = function () {
        var _this = this;
        this.orderService.GetCordStyle('OVenetian').subscribe(function (data) {
            _this.cordStyles = JSON.parse(data);
        }, function (err) { return console.error(err); });
    };
    OrderComponent.prototype.onCordChange = function () {
        var _this = this;
        debugger;
        var cordText = this.cordStyles.filter(function (cord) { return cord.CordStyleID == _this.orderItem.CordStyleID; });
        this.orderItem.cordStyleText = cordText[0].CordStyleDesc;
    };
    OrderComponent.prototype.onSlatChange = function () {
        var _this = this;
        debugger;
        var slatText = this.slatStyles.filter(function (slat) { return slat.SlatStyleID == _this.orderItem.SlatStyleID; });
        this.orderItem.slatStyleText = slatText[0].SlatStyleDesc;
    };
    OrderComponent.prototype.onReturnChange = function () {
        if (this.orderItem.ReturnRequired == true)
            this.orderItem.ReturnRequiredText = "Yes";
        else
            this.orderItem.ReturnRequiredText = "No";
    };
    OrderComponent.prototype.onMountChange = function () {
        if (this.orderItem.MountType == true)
            this.orderItem.MountTypeText = "In";
        else
            this.orderItem.MountTypeText = "Out";
    };
    OrderComponent.prototype.onNotify = function (message) {
        this.OrderInitiation = message;
        var elHome = this.home.nativeElement;
        elHome.click();
    };
    OrderComponent.prototype.SaveOrder = function () {
        var _this = this;
        var orderId = 0;
        debugger;
        this.OrderInitiation.CustomerId = this.service.GetCustomerId();
        this.OrderInitiation.orderType = 1;
        this.OrderInitiation.OrderDate = new Date();
        this.OrderInitiation.OrderStatus = "Order Received";
        this.orderService.SaveOrderInitiation(this.OrderInitiation).subscribe(function (data) {
            debugger;
            if (data) {
                var orderData = JSON.parse(data);
                if (orderData.OrderID != 0) {
                    _this.OrderInitiation.OrderID = orderData.OrderID;
                    for (var i = 0; i < _this.OrderDetails.length; i++) {
                        _this.OrderDetails[i].OrderID = orderData.OrderID;
                    }
                    _this.orderService.SaveOrderDetails(_this.OrderDetails).subscribe(function (data) {
                        if (data) {
                            _this.OrderConfirmed = true;
                            alert('Order saved successfully');
                        }
                    });
                }
                //  orderId=orderData.OrderID;
            }
        }, function (err) { return console.error(err); });
    };
    __decorate([
        core_1.ViewChild('extra'),
        __metadata("design:type", core_1.ElementRef)
    ], OrderComponent.prototype, "extra", void 0);
    __decorate([
        core_1.ViewChild('details'),
        __metadata("design:type", core_1.ElementRef)
    ], OrderComponent.prototype, "details", void 0);
    __decorate([
        core_1.ViewChild('home'),
        __metadata("design:type", core_1.ElementRef)
    ], OrderComponent.prototype, "home", void 0);
    __decorate([
        core_1.ViewChild('basic'),
        __metadata("design:type", core_1.ElementRef)
    ], OrderComponent.prototype, "basic", void 0);
    OrderComponent = __decorate([
        core_1.Component({
            selector: 'place-order',
            templateUrl: 'app/order/ordercomponent.html'
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthenticationService, core_1.ElementRef,
            order_service_1.OrderMiscService])
    ], OrderComponent);
    return OrderComponent;
}());
exports.OrderComponent = OrderComponent;
//# sourceMappingURL=order.component.js.map