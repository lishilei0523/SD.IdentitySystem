import {NgModule} from '@angular/core';
import {MessageService, ConfirmationService} from "primeng/api";
import {DialogService} from 'primeng/dynamicdialog';
import {RippleModule} from "primeng/ripple";
import {InputTextModule} from 'primeng/inputtext';
import {PasswordModule} from 'primeng/password';
import {CalendarModule} from "primeng/calendar";
import {DropdownModule} from "primeng/dropdown";
import {ButtonModule} from "primeng/button";
import {PanelModule} from "primeng/panel";
import {CardModule} from 'primeng/card';
import {ToolbarModule} from 'primeng/toolbar';
import {TabViewModule} from "primeng/tabview";
import {TableModule} from "primeng/table";
import {PaginatorModule} from "primeng/paginator";
import {ToastModule} from "primeng/toast";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {DynamicDialogModule} from 'primeng/dynamicdialog';

/*PrimeNG模块*/
@NgModule({
    imports: [
        RippleModule,
        InputTextModule,
        PasswordModule,
        CalendarModule,
        DropdownModule,
        ButtonModule,
        PanelModule,
        CardModule,
        ToolbarModule,
        TabViewModule,
        TableModule,
        PaginatorModule,
        ToastModule,
        ConfirmDialogModule,
        DynamicDialogModule
    ],
    exports: [
        PanelModule,
        RippleModule,
        InputTextModule,
        PasswordModule,
        CalendarModule,
        DropdownModule,
        ButtonModule,
        PanelModule,
        CardModule,
        ToolbarModule,
        TabViewModule,
        TableModule,
        PaginatorModule,
        ToastModule,
        ConfirmDialogModule,
        DynamicDialogModule,

    ],
    providers: [MessageService, ConfirmationService, DialogService]
})
export class UiPrimeModule {

}
