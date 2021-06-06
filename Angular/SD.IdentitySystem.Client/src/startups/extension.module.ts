import {NgModule} from '@angular/core';
import {AuthorityDirective} from "../extentions/authority.directive";
import {TabRouteReuseStrategy} from "../extentions/tab-route-reuse.strategy";
import {ApplicationTypeDescriptor} from '../values/enums/application-type.descriptor';

/*扩展模块*/
@NgModule({
    declarations: [AuthorityDirective, ApplicationTypeDescriptor],
    exports: [AuthorityDirective, ApplicationTypeDescriptor],
    providers: [TabRouteReuseStrategy]
})
export class ExtensionModule {

}
