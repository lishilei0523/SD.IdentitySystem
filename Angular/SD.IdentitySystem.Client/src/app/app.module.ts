//Angular内置模块
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AppRoutingModule} from './app-routing.module';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

//Angular本地化
import {registerLocaleData} from '@angular/common';
import zh from '@angular/common/locales/zh';

registerLocaleData(zh);

//Ng-Zorro模块
import {NZ_I18N} from 'ng-zorro-antd/i18n';
import {zh_CN} from 'ng-zorro-antd/i18n';
import {AppIconsModule} from './app-icons.module';
import {NzLayoutModule} from 'ng-zorro-antd/layout';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';

//PrimeNG模块
import {ButtonModule as PrimeButtonModule} from "primeng/button";
import {TabViewModule as PrimeTabViewModule} from "primeng/tabview";

//Angular组件
import {AppComponent} from './app.component';
import {HomeComponent} from './home/index/home.component';
import {LoginComponent} from './home/login/login.component';
import {ErrorComponent} from './home/error/error.component';
import {IndexComponent as InfoSystemIndexComponent} from './infoSystem/index/index.component';
import {IndexComponent as UserIndexComponent} from './user/index/index.component';
import {IndexComponent as RoleIndexComponent} from './role/index/index.component';
import {IndexComponent as MenuIndexComponent} from './menu/index/index.component';
import {IndexComponent as AuthorityIndexComponent} from './authority/index/index.component';
import {IndexComponent as LoginRecordIndexComponent} from './loginRecord/index/index.component';

//Angular服务
import {TabRouteReuseStrategy} from "./app-routing.strategy";
import {MenuService} from './menu/menu.service';

/*应用程序模块*/
@NgModule({
    //声明管道、指令、组件
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
        AppRoutingModule,
        FormsModule,
        HttpClientModule,
        AppIconsModule,
        NzLayoutModule,
        NzGridModule,
        NzMenuModule,
        NzDropDownModule,
        PrimeButtonModule,
        PrimeTabViewModule
    ],
    //提供服务
    providers: [{provide: NZ_I18N, useValue: zh_CN}, TabRouteReuseStrategy, MenuService],
    //启动页
    bootstrap: [AppComponent]
})
export class AppModule {

}
