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
var orderInitiationData_1 = require("../DataModels/orderInitiationData");
var OrderTypeComponent = /** @class */ (function () {
    function OrderTypeComponent() {
        this.notify = new core_1.EventEmitter();
        this.IsRemake = false;
        this.newOrder = new orderInitiationData_1.OrderInitiation();
    }
    OrderTypeComponent.prototype.ngOnInit = function () {
        this.newOrder.OrderType = this.OrderTypeVal;
    };
    OrderTypeComponent.prototype.orderChange = function () {
        debugger;
        if (this.newOrder.IsNew == true)
            this.IsRemake = false;
        else
            this.IsRemake = true;
    };
    OrderTypeComponent.prototype.ProceedData = function () {
        debugger;
        this.notify.emit(this.newOrder);
    };
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], OrderTypeComponent.prototype, "notify", void 0);
    __decorate([
        core_1.Input('order'),
        __metadata("design:type", String)
    ], OrderTypeComponent.prototype, "OrderTypeVal", void 0);
    OrderTypeComponent = __decorate([
        core_1.Component({
            selector: 'order-type',
            templateUrl: 'app/order/orderType.html'
        }),
        __metadata("design:paramtypes", [])
    ], OrderTypeComponent);
    return OrderTypeComponent;
}());
exports.OrderTypeComponent = OrderTypeComponent;
//# sourceMappingURL=orderType.component.js.map