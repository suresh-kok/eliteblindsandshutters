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
var RollerOrderComponent = /** @class */ (function () {
    function RollerOrderComponent(service, orderService) {
        this.service = service;
        this.orderService = orderService;
        this.OrderDetails = [];
        this.OrderConfirmed = false;
    }
    RollerOrderComponent.prototype.ngOnInit = function () {
        this.service.checkCredentials();
        this.OrderDetail = new orderdata_1.OrderData();
        this.OrderInitiation = new orderInitiationData_1.OrderInitiation();
        this.OrderConfirmed = false;
        this.OrderDetails = [];
        this.GetColor();
        this.GetMaterial();
        this.GetControls();
    };
    RollerOrderComponent.prototype.ngAfterViewInit = function () {
        var el = this.basic.nativeElement;
        el.click();
    };
    RollerOrderComponent.prototype.onNotify = function (message) {
        this.OrderInitiation = message;
        var elHome = this.home.nativeElement;
        elHome.click();
    };
    RollerOrderComponent.prototype.GetMaterial = function () {
        var _this = this;
        this.orderService.GetMaterial('Roller').subscribe(function (data) {
            if (data)
                _this.materialList = JSON.parse(data.toString());
        }, function (err) { return console.error(err); });
    };
    RollerOrderComponent.prototype.GetColor = function () {
        var _this = this;
        this.orderService.GetColors('Roller').subscribe(function (data) {
            if (data)
                _this.colors = JSON.parse(data.toString());
        }, function (err) { return console.error(err); });
    };
    RollerOrderComponent.prototype.GetControls = function () {
        this.controls = [{ ControlID: "11", ControlDesc: "Left" }, { ControlID: "12", ControlDesc: "RIGHT" }];
        this.OrderDetail.ControlID = 11;
    };
    RollerOrderComponent.prototype.onControlChange = function () {
        var _this = this;
        debugger;
        var text = this.controls.filter(function (control) { return control.ControlID == _this.OrderDetail.ControlID; });
        this.OrderDetail.ControlName = text[0].ControlDesc;
    };
    RollerOrderComponent.prototype.onColorChange = function () {
        var _this = this;
        debugger;
        var text = this.colors.filter(function (color) { return color.ColorsID == _this.OrderDetail.ColorID; });
        this.OrderDetail.ColorName = text[0].ColorsDesc;
    };
    RollerOrderComponent.prototype.OnMaterialChange = function () {
        var _this = this;
        debugger;
        var text = this.materialList.filter(function (material) { return material.MaterialID == _this.OrderDetail.MaterialID; });
        this.OrderDetail.MaterialName = text[0].MaterialDesc;
    };
    RollerOrderComponent.prototype.AddItem = function () {
        debugger;
        this.OrderDetails.push(this.OrderDetail);
        this.OrderDetail = new orderdata_1.OrderData();
    };
    RollerOrderComponent.prototype.Proceed = function () {
        var elDetails = this.details.nativeElement;
        elDetails.click();
    };
    RollerOrderComponent.prototype.OrderConfirm = function () {
        debugger;
        var el = this.extra.nativeElement;
        el.click();
    };
    RollerOrderComponent.prototype.Delete = function (item) {
        var index = this.OrderDetails.indexOf(item);
        if (index !== -1) {
            this.OrderDetails.splice(index, 1);
        }
    };
    RollerOrderComponent.prototype.SaveOrder = function () {
        var _this = this;
        var orderId = 0;
        debugger;
        this.OrderInitiation.CustomerID = this.service.GetCustomerId();
        this.OrderInitiation.OrderTypeID = 2;
        this.OrderInitiation.OrderDate = new Date();
        this.OrderInitiation.OrderStatusID = 1;
        this.OrderInitiation.OrderStatus = "IN_Processing";
        this.orderService.SaveOrderInitiation(this.OrderInitiation).subscribe(function (data) {
            debugger;
            if (data) {
                // let orderData:any;
                var orderData = JSON.parse(data.toString());
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
            }
        }, function (err) { return console.error(err); });
    };
    RollerOrderComponent.prototype.NewOrder = function () {
        this.OrderDetail = new orderdata_1.OrderData();
        this.OrderInitiation = new orderInitiationData_1.OrderInitiation();
        this.OrderConfirmed = false;
        this.OrderDetails = [];
    };
    __decorate([
        core_1.ViewChild('extra'),
        __metadata("design:type", core_1.ElementRef)
    ], RollerOrderComponent.prototype, "extra", void 0);
    __decorate([
        core_1.ViewChild('details'),
        __metadata("design:type", core_1.ElementRef)
    ], RollerOrderComponent.prototype, "details", void 0);
    __decorate([
        core_1.ViewChild('home'),
        __metadata("design:type", core_1.ElementRef)
    ], RollerOrderComponent.prototype, "home", void 0);
    __decorate([
        core_1.ViewChild('basic'),
        __metadata("design:type", core_1.ElementRef)
    ], RollerOrderComponent.prototype, "basic", void 0);
    RollerOrderComponent = __decorate([
        core_1.Component({
            selector: 'roller-order',
            templateUrl: 'app/rollerorder/rollerorder.html'
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthenticationService, order_service_1.OrderMiscService])
    ], RollerOrderComponent);
    return RollerOrderComponent;
}());
exports.RollerOrderComponent = RollerOrderComponent;
//# sourceMappingURL=rollerorder.component.js.map