import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {InfrastructureModule} from "sd-infrastructure";
import {AppRoutingModule} from './modules/app-routing.module';
import {AppExceptionModule} from "./modules/app-exception.module";
import {NgZorroModule} from "./modules/ng-zorro.module";

//组件
import {AppComponent} from './app.component';
import {IndexComponent as HomeComponent} from './app/home/index/index.component';
import {LoginComponent} from './app/home/login/login.component';
import {UpdatePasswordComponent} from './app/home/update-password/update-password.component';
import {IndexComponent as InfoSystemIndexComponent} from './app/info-system/index/index.component';
import {AddComponent as InfoSystemAddComponent} from './app/info-system/add/add.component';
import {UpdateComponent as InfoSystemUpdateComponent} from './app/info-system/update/update.component';
import {InitComponent as InfoSystemInitComponent} from './app/info-system/init/init.component';
import {IndexComponent as UserIndexComponent} from './app/user/index/index.component';
import {AddComponent as UserAddComponent} from './app/user/add/add.component';
import {ResetPasswordComponent as UserResetPasswordComponent} from './app/user/reset-password/reset-password.component';
import {ResetPrivateKeyComponent as UserResetPrivateKeyComponent} from './app/user/reset-private-key/reset-private-key.component';
import {IndexComponent as RoleIndexComponent} from './app/role/index/index.component';
import {IndexComponent as MenuIndexComponent} from './app/menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from './app/authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from './app/login-record/index/index.component';

//服务
import {HomeService} from "./services/home.service";
import {InfoSystemService} from "./services/info-system.service";
import {UserService} from "./services/user.service";
import {MenuService} from './services/menu.service';
import {LoginRecordService} from "./services/login-record.service";


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
        UserAddComponent,
        UserResetPasswordComponent,
        UserResetPrivateKeyComponent,
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
        InfrastructureModule,
        AppRoutingModule,
        AppExceptionModule,
        NgZorroModule
    ],
    //提供服务
    providers: [
        HomeService,
        InfoSystemService,
        UserService,
        MenuService,
        LoginRecordService
    ],
    //启动项
    bootstrap: [AppComponent]
})
export class AppModule {

}
