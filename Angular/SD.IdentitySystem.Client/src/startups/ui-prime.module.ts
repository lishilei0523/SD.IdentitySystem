import {NgModule} from '@angular/core';
import {InputTextModule} from 'primeng/inputtext';
import {PasswordModule} from 'primeng/password';
import {ButtonModule} from "primeng/button";
import {TabViewModule} from "primeng/tabview";
import {PanelModule} from "primeng/panel";
import {RippleModule} from "primeng/ripple";
import {CardModule} from "primeng/card";
import {ToastModule} from "primeng/toast";
import {MessageService} from "primeng/api";

/*PrimeNG模块*/
@NgModule({
    imports: [
        PanelModule,
        RippleModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        TabViewModule,
        CardModule,
        ToastModule
    ],
    exports: [
        PanelModule,
        RippleModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        TabViewModule,
        CardModule,
        ToastModule
    ],
    providers: [MessageService]
})
export class UiPrimeModule {

}
