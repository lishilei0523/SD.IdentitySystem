//Angular内置模块
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

//UI库模块
import {UiZorroModule} from './ui-zorro.module';
import {UiPrimeModule} from './ui-prime.module';

//扩展模块
import {ExtensionModule} from './extension.module';

//路由模块
import {AppRoutingModule} from './app-routing.module';

//Angular组件
import {AppComponent} from './app.component';
import {IndexComponent as HomeComponent} from '../app/home/index/index.component';
import {LoginComponent} from '../app/home/login/login.component';
import {ErrorComponent} from '../app/home/error/error.component';
import {IndexComponent as InfoSystemIndexComponent} from '../app/infoSystem/index/index.component';
import {IndexComponent as UserIndexComponent} from '../app/user/index/index.component';
import {IndexComponent as RoleIndexComponent} from '../app/role/index/index.component';
import {IndexComponent as MenuIndexComponent} from '../app/menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from '../app/authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from '../app/loginRecord/index/index.component';

//Angular服务
import {MenuService} from '../app/menu/menu.service';

/*应用程序模块*/
@NgModule({
    //声明组件
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        ErrorComponent,
        InfoSystemIndexComponent,
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
        HttpClientModule,
        UiZorroModule,
        UiPrimeModule,
        ExtensionModule,
        AppRoutingModule
    ],
    //提供服务
    providers: [MenuService],
    //启动页
    bootstrap: [AppComponent]
})
export class AppModule {

}
