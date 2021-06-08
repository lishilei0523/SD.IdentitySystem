import {NgModule} from '@angular/core';
import {MessageService, ConfirmationService} from "primeng/api";
import {DialogService} from 'primeng/dynamicdialog';
import {RippleModule} from "primeng/ripple";
import {InputTextModule} from 'primeng/inputtext';
import {PasswordModule} from 'primeng/password';
import {ButtonModule} from "primeng/button";
import {PanelModule} from "primeng/panel";
import {CardModule} from 'primeng/card';
import {ToolbarModule} from 'primeng/toolbar';
import {TabViewModule} from "primeng/tabview";
import {ToastModule} from "primeng/toast";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {DynamicDialogModule} from 'primeng/dynamicdialog';

/*PrimeNG模块*/
@NgModule({
    imports: [
        RippleModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        PanelModule,
        CardModule,
        ToolbarModule,
        TabViewModule,
        ToastModule,
        ConfirmDialogModule,
        DynamicDialogModule
    ],
    exports: [
        PanelModule,
        RippleModule,
        InputTextModule,
        PasswordModule,
        ButtonModule,
        PanelModule,
        CardModule,
        ToolbarModule,
        TabViewModule,
        ToastModule,
        ConfirmDialogModule,
        DynamicDialogModule,

    ],
    providers: [MessageService, ConfirmationService, DialogService]
})
export class UiPrimeModule {

}
