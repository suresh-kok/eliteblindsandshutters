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
var OverSeaComponent = /** @class */ (function () {
    function OverSeaComponent(service, element, orderService) {
        this.service = service;
        this.orderService = orderService;
        this.OrderDetails = [];
        this.OrderConfirmed = false;
        this.OrderDetail = new orderdata_1.OrderData();
        this.OrderInitiation = new orderInitiationData_1.OrderInitiation();
    }
    OverSeaComponent.prototype.ngOnInit = function () {
        this.service.checkCredentials();
    };
    OverSeaComponent.prototype.ngAfterViewInit = function () {
        var el = this.basic.nativeElement;
        el.click();
    };
    OverSeaComponent.prototype.onNotify = function (message) {
        this.OrderInitiation = message;
        if (this.OrderInitiation.BlindTypeID) {
            this.GetMaterial();
            this.GetColors();
            this.GetControls();
        }
        if (this.OrderInitiation.BlindTypeID == 2)
            this.GetCordStyles();
        var elHome = this.home.nativeElement;
        elHome.click();
    };
    OverSeaComponent.prototype.GetControls = function () {
        if (this.OrderInitiation.BlindTypeID == 4) {
            this.controls = [{ ControlID: "5", ControlDesc: "MANUAL" }, { ControlID: "6", ControlDesc: "MANUAL WITH HEADER" }, { ControlID: "7", ControlDesc: "MANUAL WITH FASCIA BOX" }, { ControlID: "8", ControlDesc: "MOTOR" }];
            this.OrderDetail.ControlID = 5;
        }
        else {
            this.controls = [{ ControlID: "1", ControlDesc: "MANUAL" }, { ControlID: "2", ControlDesc: "MOTOR" }];
            this.OrderDetail.ControlID = 1;
        }
    };
    OverSeaComponent.prototype.onControlChange = function () {
        var _this = this;
        debugger;
        var text = this.controls.filter(function (control) { return control.ControlID == _this.OrderDetail.ControlID; });
        this.OrderDetail.ControlName = text[0].ControlDesc;
    };
    OverSeaComponent.prototype.GetCordStyles = function () {
        var _this = this;
        this.orderService.GetCordStyle('OVenetian').subscribe(function (data) {
            debugger;
            _this.cordStyles = JSON.parse(data.toString());
        }, function (err) { return console.error(err); });
    };
    OverSeaComponent.prototype.GetColors = function () {
        var _this = this;
        var blindType;
        switch (this.OrderInitiation.BlindTypeID) {
            case "1":
                blindType = 'OVertical';
                break;
            case "2":
                blindType = 'OVenetian';
                break;
            case "3":
                blindType = 'OHoneycomb';
                break;
            case "4":
                blindType = 'ORoman';
                break;
            case "5":
                blindType = 'ORoller';
                break;
            case "6":
                blindType = 'OZebra';
                break;
        }
        this.orderService.GetColors(blindType).subscribe(function (data) {
            if (data)
                _this.colors = JSON.parse(data.toString());
        });
    };
    OverSeaComponent.prototype.GetMaterial = function () {
        var _this = this;
        var blindType;
        switch (this.OrderInitiation.BlindTypeID) {
            case "1":
                blindType = 'OVertical';
                break;
            case "2":
                blindType = 'OVenetian';
                break;
            case "3":
                blindType = 'OHoneycomb';
                break;
            case "4":
                blindType = 'ORoman';
                break;
            case "5":
                blindType = 'ORoller';
                break;
            case "6":
                blindType = 'OZebra';
                break;
        }
        if (blindType != '') {
            debugger;
            this.orderService.GetMaterial(blindType).subscribe(function (data) {
                if (data)
                    _this.materialList = JSON.parse(data.toString());
            }, function (err) { return console.error(err); });
        }
    };
    OverSeaComponent.prototype.onCordChange = function () {
        var _this = this;
        debugger;
        var cordText = this.cordStyles.filter(function (cord) { return cord.CordStyleID == _this.OrderDetail.CordStyleID; });
        this.OrderDetail.CordStyleName = cordText[0].CordStyleDesc;
    };
    OverSeaComponent.prototype.onColorChange = function () {
        var _this = this;
        debugger;
        var text = this.colors.filter(function (color) { return color.ColorsID == _this.OrderDetail.ColorID; });
        this.OrderDetail.ColorName = text[0].ColorsDesc;
    };
    OverSeaComponent.prototype.OnMaterialChange = function () {
        var _this = this;
        debugger;
        var text = this.materialList.filter(function (material) { return material.MaterialID == _this.OrderDetail.MaterialID; });
        this.OrderDetail.MaterialName = text[0].MaterialDesc;
    };
    OverSeaComponent.prototype.onReturnChange = function () {
        if (this.OrderDetail.ReturnRequired == true)
            this.OrderDetail.ReturnRequiredText = "Yes";
        else
            this.OrderDetail.ReturnRequiredText = "No";
    };
    OverSeaComponent.prototype.onMountChange = function () {
        if (this.OrderDetail.MountType == true)
            this.OrderDetail.MountTypeText = "In";
        else
            this.OrderDetail.MountTypeText = "Out";
    };
    OverSeaComponent.prototype.AddItem = function () {
        debugger;
        this.OrderDetails.push(this.OrderDetail);
        this.OrderDetail = new orderdata_1.OrderData();
    };
    OverSeaComponent.prototype.Proceed = function () {
        var elDetails = this.details.nativeElement;
        elDetails.click();
    };
    OverSeaComponent.prototype.OrderConfirm = function () {
        debugger;
        var el = this.extra.nativeElement;
        el.click();
        // elDetails.
        //this.enableCheckOut=true;
    };
    OverSeaComponent.prototype.SaveOrder = function () {
        var _this = this;
        switch (this.OrderInitiation.BlindTypeID) {
            case "1":
                this.OrderInitiation.OrderTypeID = 3;
                break;
            case "2":
                this.OrderInitiation.OrderTypeID = 4;
                break;
            case "3":
                this.OrderInitiation.OrderTypeID = 5;
                break;
            case "4":
                this.OrderInitiation.OrderTypeID = 6;
                break;
            case "5":
                this.OrderInitiation.OrderTypeID = 7;
                break;
            case "6":
                this.OrderInitiation.OrderTypeID = 8;
                break;
        }
        var orderId = 0;
        debugger;
        this.OrderInitiation.CustomerID = this.service.GetCustomerId();
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
    OverSeaComponent.prototype.Delete = function (item) {
        var index = this.OrderDetails.indexOf(item);
        if (index !== -1) {
            this.OrderDetails.splice(index, 1);
        }
    };
    __decorate([
        core_1.ViewChild('extra'),
        __metadata("design:type", core_1.ElementRef)
    ], OverSeaComponent.prototype, "extra", void 0);
    __decorate([
        core_1.ViewChild('details'),
        __metadata("design:type", core_1.ElementRef)
    ], OverSeaComponent.prototype, "details", void 0);
    __decorate([
        core_1.ViewChild('home'),
        __metadata("design:type", core_1.ElementRef)
    ], OverSeaComponent.prototype, "home", void 0);
    __decorate([
        core_1.ViewChild('basic'),
        __metadata("design:type", core_1.ElementRef)
    ], OverSeaComponent.prototype, "basic", void 0);
    OverSeaComponent = __decorate([
        core_1.Component({
            selector: 'oversea-order',
            templateUrl: 'app/overseaorder/overseaorder.html'
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthenticationService, core_1.ElementRef,
            order_service_1.OrderMiscService])
    ], OverSeaComponent);
    return OverSeaComponent;
}());
exports.OverSeaComponent = OverSeaComponent;
//# sourceMappingURL=overseaorder.component.js.map