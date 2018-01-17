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
var http_1 = require("@angular/common/http");
var OrderMiscService = /** @class */ (function () {
    function OrderMiscService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this.rootUrl = "http://localhost:62253/api/Customer/";
        this.headers.append('Access-Control-Allow-Headers', 'Content-Type');
        this.headers.append('Access-Control-Allow-Methods', 'GET');
        this.headers.append('Access-Control-Allow-Origin', '*');
    }
    OrderMiscService.prototype.GetColors = function (type) {
        return this.http.get(this.rootUrl + "GetColors/" + type);
    };
    OrderMiscService.prototype.GetMaterial = function (type) {
        return this.http.get(this.rootUrl + "GetMaterial/" + type);
    };
    OrderMiscService.prototype.GetCordStyle = function (type) {
        return this.http.get(this.rootUrl + "GetCordStyle/" + type);
    };
    OrderMiscService.prototype.GetSlatStyle = function (type) {
        return this.http.get(this.rootUrl + "GetSlatStyle/" + type);
    };
    OrderMiscService.prototype.GetSize = function (type) {
        return this.http.get(this.rootUrl + "GetSize/" + type);
    };
    OrderMiscService.prototype.GetBlindType = function () {
        return this.http.get(this.rootUrl + "GetBlindType");
    };
    OrderMiscService.prototype.SaveOrderInitiation = function (order) {
        debugger;
        var headers = new http_1.HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
        return this.http.post(this.rootUrl + "OrderInitiation", JSON.stringify(order), { headers: headers });
    };
    OrderMiscService.prototype.SaveOrderDetails = function (orderDetails) {
        debugger;
        var headers = new http_1.HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
        return this.http.post(this.rootUrl + "OrderDetails", JSON.stringify(orderDetails), { headers: headers });
    };
    OrderMiscService.prototype.GetOrders = function (CustomerID, FilterBy, SearchCriteria, OrderBy) {
        return this.http.get(this.rootUrl + "SearchOrders/" + CustomerID + "/" + FilterBy + "/" + SearchCriteria + "/" + OrderBy);
    };
    OrderMiscService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], OrderMiscService);
    return OrderMiscService;
}());
exports.OrderMiscService = OrderMiscService;
//# sourceMappingURL=order.service.js.map