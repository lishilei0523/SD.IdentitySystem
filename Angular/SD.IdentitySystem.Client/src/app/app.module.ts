//Angular内置模块
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

//Angular本地化
import {registerLocaleData} from '@angular/common';
import zh from '@angular/common/locales/zh';

registerLocaleData(zh);

//Ng-Zorro模块
import {NZ_I18N} from 'ng-zorro-antd/i18n';
import {zh_CN} from 'ng-zorro-antd/i18n';
import {NzLayoutModule} from 'ng-zorro-antd/layout';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';

//Angular组件
import {HomeComponent} from './home/home.component';

@NgModule({
    declarations: [
        HomeComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        NzLayoutModule,
        NzGridModule,
        NzMenuModule,
        NzDropDownModule
    ],
    providers: [{provide: NZ_I18N, useValue: zh_CN}],
    bootstrap: [HomeComponent]
})
export class AppModule {

}
