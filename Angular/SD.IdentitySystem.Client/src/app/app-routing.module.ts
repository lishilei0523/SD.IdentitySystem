import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {HomeComponent} from './home/index/home.component';
import {LoginComponent} from './home/login/login.component';
import {ErrorComponent} from './home/error/error.component';
import {IndexComponent as InfoSystemIndexComponent} from './infoSystem/index/index.component';
import {IndexComponent as UserIndexComponent} from './user/index/index.component';
import {IndexComponent as RoleIndexComponent} from './role/index/index.component';
import {IndexComponent as MenuIndexComponent} from './menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from './authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from './loginRecord/index/index.component';

const routes: Routes = [
    {
        path: "Home", component: HomeComponent, children: [
            {path: "InfoSystem", component: InfoSystemIndexComponent},
            {path: "User", component: UserIndexComponent},
            {path: "Role", component: RoleIndexComponent},
            {path: "Menu", component: MenuIndexComponent},
            {path: "Authority", component: AuthorityIndexComponent},
            {path: "LoginRecord", component: LoginRecordIndexComponent},
        ]
    },
    {path: "Login", component: LoginComponent},
    {path: "Error", component: ErrorComponent},
    {path: "**", component: HomeComponent}
];

/*应用程序路由模块*/
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {

}
