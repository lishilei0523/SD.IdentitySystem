import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

//路由守卫器
import {AuthenticationGuard} from "../extentions/authentication.guard";

//Angular组件
import {IndexComponent as HomeComponent} from '../app/home/index/index.component';
import {LoginComponent} from '../app/home/login/login.component';
import {ErrorComponent} from '../app/home/error/error.component';
import {IndexComponent as InfoSystemIndexComponent} from '../app/infoSystem/index/index.component';
import {IndexComponent as UserIndexComponent} from '../app/user/index/index.component';
import {IndexComponent as RoleIndexComponent} from '../app/role/index/index.component';
import {IndexComponent as MenuIndexComponent} from '../app/menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from '../app/authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from '../app/loginRecord/index/index.component';

//路由配置
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
    exports: [RouterModule],
    providers: [AuthenticationGuard]
})
export class AppRoutingModule {

}
