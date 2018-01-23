"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var NotAuthorisedComponent = /** @class */ (function () {
    function NotAuthorisedComponent() {
    }
    NotAuthorisedComponent = __decorate([
        core_1.Component({
            selector: 'not-authorised',
            template: "\n    <div class=\"alert alert-danger\">\n    <strong>Please login to view this page</strong> \n  </div>\n    "
        })
    ], NotAuthorisedComponent);
    return NotAuthorisedComponent;
}());
exports.NotAuthorisedComponent = NotAuthorisedComponent;
//# sourceMappingURL=unauthorised.js.map