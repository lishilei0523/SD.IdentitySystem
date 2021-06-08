import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

//路由守卫器
import {AuthenticationGuard} from "../extentions/authentication.guard";

//Angular组件
import {IndexComponent as HomeComponent} from '../app/home/index/index.component';
import {LoginComponent} from '../app/home/login/login.component';
import {IndexComponent as InfoSystemIndexComponent} from '../app/info-system/index/index.component';
import {IndexComponent as UserIndexComponent} from '../app/user/index/index.component';
import {IndexComponent as RoleIndexComponent} from '../app/role/index/index.component';
import {IndexComponent as MenuIndexComponent} from '../app/menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from '../app/authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from '../app/login-record/index/index.component';

//路由配置
const routes: Routes = [
    {
        path: "Home", component: HomeComponent, canActivate: [AuthenticationGuard], children: [
            {path: "InfoSystem", component: InfoSystemIndexComponent, canActivate: [AuthenticationGuard]},
            {path: "User", component: UserIndexComponent, canActivate: [AuthenticationGuard]},
            {path: "Role", component: RoleIndexComponent, canActivate: [AuthenticationGuard]},
            {path: "Menu", component: MenuIndexComponent, canActivate: [AuthenticationGuard]},
            {path: "Authority", component: AuthorityIndexComponent, canActivate: [AuthenticationGuard]},
            {path: "LoginRecord", component: LoginRecordIndexComponent, canActivate: [AuthenticationGuard]},
        ]
    },
    {path: "Login", component: LoginComponent},
    {path: "**", component: HomeComponent, canActivate: [AuthenticationGuard]}
];

/*应用程序路由模块*/
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [AuthenticationGuard]
})
export class AppRoutingModule {

}
