//Angular内置模块
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

//Ng-Zorro模块
import {NgZorroModule} from './ng-zorro.module';

//路由模块
import {AppRoutingModule} from './app-routing.module';

//扩展模块
import {AppConfigModule} from "./app-config.module";
import {AppExceptionModule} from "./app-exception.module";
import {AppAuthenticationModule} from "./app-authentication.module";
import {AppExtensionModule} from './app-extension.module';

//Angular组件
import {AppComponent} from './app.component';
import {IndexComponent as HomeComponent} from '../app/home/index/index.component';
import {LoginComponent} from '../app/home/login/login.component';
import {IndexComponent as InfoSystemIndexComponent} from '../app/info-system/index/index.component';
import {IndexComponent as UserIndexComponent} from '../app/user/index/index.component';
import {UpdatePasswordComponent as UserUpdatePasswordComponent} from '../app/user/update-password/update-password.component';
import {IndexComponent as RoleIndexComponent} from '../app/role/index/index.component';
import {IndexComponent as MenuIndexComponent} from '../app/menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from '../app/authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from '../app/login-record/index/index.component';

//Angular服务
import {MenuService} from '../app/menu/menu.service';
import {LoginRecordService} from "../app/login-record/login-record.service";


/*应用程序模块*/
@NgModule({
    //声明组件
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        InfoSystemIndexComponent,
        UserIndexComponent,
        UserUpdatePasswordComponent,
        RoleIndexComponent,
        MenuIndexComponent,
        AuthorityIndexComponent,
        LoginRecordIndexComponent
    ],
    //导入模块
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgZorroModule,
        AppRoutingModule,
        AppConfigModule,
        AppExceptionModule,
        AppAuthenticationModule,
        AppExtensionModule
    ],
    //提供服务
    providers: [MenuService, LoginRecordService],
    //启动页
    bootstrap: [AppComponent]
})
export class AppModule {

}
