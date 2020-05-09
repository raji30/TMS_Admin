import { NgModule } from '@angular/core';
import { LaddaDirective } from './ladda.directive';
import { LaddaConfig } from './ladda-config';
var LaddaModule = /** @class */ (function () {
    function LaddaModule() {
    }
    LaddaModule.forRoot = function (config) {
        return {
            ngModule: LaddaModule,
            providers: [
                { provide: LaddaConfig, useValue: config }
            ]
        };
    };
    LaddaModule.decorators = [
        { type: NgModule, args: [{
                    declarations: [LaddaDirective],
                    exports: [LaddaDirective],
                },] },
    ];
    return LaddaModule;
}());
export { LaddaModule };
//# sourceMappingURL=module.js.map