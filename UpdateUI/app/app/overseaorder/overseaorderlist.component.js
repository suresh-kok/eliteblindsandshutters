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
var OverSeaOrderListComponent = /** @class */ (function () {
    function OverSeaOrderListComponent() {
        this.OrderPlaced = false;
    }
    __decorate([
        core_1.Input('orderInitiate'),
        __metadata("design:type", orderInitiationData_1.OrderInitiation)
    ], OverSeaOrderListComponent.prototype, "OrderInfo", void 0);
    __decorate([
        core_1.Input('orderDetails'),
        __metadata("design:type", Array)
    ], OverSeaOrderListComponent.prototype, "OrderDetails", void 0);
    __decorate([
        core_1.Input('orderPlaced'),
        __metadata("design:type", Boolean)
    ], OverSeaOrderListComponent.prototype, "OrderPlaced", void 0);
    OverSeaOrderListComponent = __decorate([
        core_1.Component({
            selector: 'overseaorder-list',
            templateUrl: 'app/overseaorder/overseaorderlist.html'
        }),
        __metadata("design:paramtypes", [])
    ], OverSeaOrderListComponent);
    return OverSeaOrderListComponent;
}());
exports.OverSeaOrderListComponent = OverSeaOrderListComponent;
//# sourceMappingURL=overseaorderlist.component.js.map