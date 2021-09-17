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

//Angular组件
import {AppComponent} from './app.component';
import {IndexComponent as HomeComponent} from '../app/home/index/index.component';
import {LoginComponent} from '../app/home/login/login.component';
import {UpdatePasswordComponent} from '../app/home/update-password/update-password.component';
import {IndexComponent as InfoSystemIndexComponent} from '../app/info-system/index/index.component';
import {AddComponent as InfoSystemAddComponent} from '../app/info-system/add/add.component';
import {UpdateComponent as InfoSystemUpdateComponent} from '../app/info-system/update/update.component';
import {InitComponent as InfoSystemInitComponent} from '../app/info-system/init/init.component';
import {IndexComponent as UserIndexComponent} from '../app/user/index/index.component';
import {IndexComponent as RoleIndexComponent} from '../app/role/index/index.component';
import {IndexComponent as MenuIndexComponent} from '../app/menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from '../app/authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from '../app/login-record/index/index.component';

//Angular服务
import {HomeService} from "../app/home/home.service";
import {InfoSystemService} from "../app/info-system/info-system.service";
import {UserService} from "../app/user/user.service";
import {MenuService} from '../app/menu/menu.service';
import {LoginRecordService} from "../app/login-record/login-record.service";


/*应用程序模块*/
@NgModule({
    //声明组件
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        UpdatePasswordComponent,
        InfoSystemIndexComponent,
        InfoSystemAddComponent,
        InfoSystemUpdateComponent,
        InfoSystemInitComponent,
        UserIndexComponent,
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
        AppAuthenticationModule
    ],
    //提供服务
    providers: [HomeService, InfoSystemService, UserService, MenuService, LoginRecordService],
    //启动页
    bootstrap: [AppComponent]
})
export class AppModule {

}
