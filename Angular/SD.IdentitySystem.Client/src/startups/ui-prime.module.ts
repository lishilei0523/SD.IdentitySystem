import {NgModule} from '@angular/core';
import {ButtonModule as PrimeButtonModule} from "primeng/button";
import {TabViewModule as PrimeTabViewModule} from "primeng/tabview";

/*PrimeNG模块*/
@NgModule({
    imports: [PrimeButtonModule, PrimeTabViewModule],
    exports: [PrimeButtonModule, PrimeTabViewModule]
})
export class UiPrimeModule {

}
