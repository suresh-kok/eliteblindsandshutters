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
var MiscellaneousService = /** @class */ (function () {
    function MiscellaneousService(http) {
        this.http = http;
        this.headers = new http_1.HttpHeaders();
        this.api_key = '17bca5a48c36fd2fc5ee89653708b849';
        this.headers.append('Access-Control-Allow-Headers', 'Content-Type');
        this.headers.append('Access-Control-Allow-Methods', 'GET');
        this.headers.append('Access-Control-Allow-Origin', '*');
    }
    MiscellaneousService.prototype.GetAllCountries = function () {
        return this.http.get('http://battuta.medunes.net/api/country/all/?key=' + this.api_key, { headers: this.headers });
    };
    MiscellaneousService.prototype.GetRegions = function (country) {
        var regionUrl = 'http://battuta.medunes.net/api/region/' + country + '/all/?key=' + this.api_key;
        return this.http.get(regionUrl, { headers: this.headers });
    };
    MiscellaneousService.prototype.GetCities = function (country, region) {
        var url = "http://battuta.medunes.net/api/city/" + country + "/search/?region=" + region + "&key=" + this.api_key;
        return this.http.get(url, { headers: this.headers });
    };
    MiscellaneousService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], MiscellaneousService);
    return MiscellaneousService;
}());
exports.MiscellaneousService = MiscellaneousService;
//# sourceMappingURL=misc.service.js.map