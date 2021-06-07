import {NgModule} from '@angular/core';
import {MessageService, ConfirmationService} from "primeng/api";
import {RippleModule} from "primeng/ripple";
import {InputTextModule} from 'primeng/inputtext';
import {PasswordModule} from 'primeng/password';
import {ButtonModule} from "primeng/button";
import {PanelModule} from "primeng/panel";
import {TabViewModule} from "primeng/tabview";
import {ToastModule} from "primeng/toast";
import {ConfirmDialogModule} from "primeng/confirmdialog";

/*PrimeNG模块*/
@NgModule({
    imports: [
        RippleModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        PanelModule,
        TabViewModule,
        ToastModule,
        ConfirmDialogModule
    ],
    exports: [
        PanelModule,
        RippleModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        TabViewModule,
        ToastModule,
        ConfirmDialogModule
    ],
    providers: [MessageService, ConfirmationService]
})
export class UiPrimeModule {

}
