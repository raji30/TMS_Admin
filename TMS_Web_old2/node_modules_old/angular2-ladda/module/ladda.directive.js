import { Directive, ElementRef, Input, Optional, Inject } from '@angular/core';
import { LaddaConfig, LaddaConfigArgs, configAttributes } from './ladda-config';
import { create as createLadda } from 'ladda';
var LaddaDirective = /** @class */ (function () {
    function LaddaDirective(el, config) {
        this.el = el.nativeElement;
        if (!config) {
            return;
        }
        // apply default styles if they aren't overwritten by an attribute
        for (var attribute in configAttributes) {
            var configValue = config[configAttributes[attribute]];
            if (!configValue) {
                continue; // don't waste time reading the attribute
            }
            if (!this.el.getAttribute(attribute)) {
                // attribute isn't set - apply the default config value
                var value = (typeof configValue === "number") ? configValue.toString() : configValue;
                this.el.setAttribute(attribute, value);
            }
        }
    }
    LaddaDirective.prototype.ngOnChanges = function (changes) {
        if (!this._ladda) {
            return; // needed since ngOnChanges is called before ngOnInit
        }
        if (changes['loading']) {
            this.updateLadda(changes['loading'].previousValue);
        }
        if (changes['disabled']) {
            this.updateDisabled();
        }
    };
    LaddaDirective.prototype.ngOnInit = function () {
        var _this = this;
        this._ladda = createLadda(this.el);
        // if the initial loading value isn't false, a timeout of 0 ms
        // is necessary for the calculated spinner size to be correct.
        setTimeout(function () { _this.updateLadda(false); }, 0);
    };
    LaddaDirective.prototype.ngOnDestroy = function () {
        if (this._ladda) {
            this._ladda.remove();
        }
    };
    LaddaDirective.prototype.updateLadda = function (previousValue) {
        var loading = typeof this.loading === 'number' || !!this.loading;
        var wasLoading = typeof previousValue === 'number' || !!previousValue;
        if (!loading) {
            if (wasLoading) {
                this._ladda.stop();
            }
            return this.updateDisabled();
        }
        if (!wasLoading) {
            this._ladda.start();
        }
        if (typeof this.loading === 'number') {
            this._ladda.setProgress(this.loading);
        }
    };
    LaddaDirective.prototype.updateDisabled = function () {
        this.el.disabled = this.disabled;
    };
    LaddaDirective.decorators = [
        { type: Directive, args: [{
                    selector: '[ladda]'
                },] },
    ];
    /** @nocollapse */
    LaddaDirective.ctorParameters = function () { return [
        { type: ElementRef, },
        { type: LaddaConfigArgs, decorators: [{ type: Inject, args: [LaddaConfig,] }, { type: Optional },] },
    ]; };
    LaddaDirective.propDecorators = {
        "loading": [{ type: Input, args: ['ladda',] },],
        "disabled": [{ type: Input, args: ['disabled',] },],
    };
    return LaddaDirective;
}());
export { LaddaDirective };
//# sourceMappingURL=ladda.directive.js.map