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
var OverSeaComponent = /** @class */ (function () {
    function OverSeaComponent(service) {
        this.service = service;
    }
    OverSeaComponent.prototype.ngOnInit = function () {
        this.service.checkCredentials();
    };
    OverSeaComponent.prototype.ngAfterViewInit = function () {
        var el = this.basic.nativeElement;
        el.click();
    };
    OverSeaComponent.prototype.onNotify = function (message) {
        debugger;
        this.OrderInitiation = message;
        var elHome = this.home.nativeElement;
        elHome.click();
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
            selector: "oversea-order",
            templateUrl: 'app/overseaorder/overseaorder.html'
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthenticationService])
    ], OverSeaComponent);
    return OverSeaComponent;
}());
exports.OverSeaComponent = OverSeaComponent;
//# sourceMappingURL=overseaorder.component.js.map