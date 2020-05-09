import { Injectable } from "@angular/core";
var LaddaConfigArgs = /** @class */ (function () {
    function LaddaConfigArgs() {
    }
    return LaddaConfigArgs;
}());
export { LaddaConfigArgs };
export var configAttributes = {
    "data-style": "style",
    "data-spinner-size": "spinnerSize",
    "data-spinner-color": "spinnerColor",
    "data-spinner-lines": "spinnerLines",
};
var LaddaConfig = /** @class */ (function () {
    function LaddaConfig(config) {
        if (config === void 0) { config = {}; }
        Object.assign(this, config);
    }
    LaddaConfig.decorators = [
        { type: Injectable },
    ];
    /** @nocollapse */
    LaddaConfig.ctorParameters = function () { return [
        { type: LaddaConfigArgs, },
    ]; };
    return LaddaConfig;
}());
export { LaddaConfig };
//# sourceMappingURL=ladda-config.js.map