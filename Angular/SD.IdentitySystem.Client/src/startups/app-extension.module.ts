import {NgModule} from '@angular/core';
import {RouteReuseStrategy} from "@angular/router";
import {AuthorityDirective} from "../extentions/authority.directive";
import {TabRouteReuseStrategy} from "../extentions/route-reuse.strategy";
import {ApplicationTypeDescriptor} from '../values/enums/application-type.descriptor';

/*应用程序扩展模块*/
@NgModule({
    declarations: [AuthorityDirective, ApplicationTypeDescriptor],
    exports: [AuthorityDirective, ApplicationTypeDescriptor],
    providers: [
        {provide: RouteReuseStrategy, useClass: TabRouteReuseStrategy}
    ]
})
export class AppExtensionModule {

}
