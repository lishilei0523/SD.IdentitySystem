//Angular本地化
import {registerLocaleData} from '@angular/common';
import zh from '@angular/common/locales/zh';

registerLocaleData(zh);

//Ng-Zorro模块
import {NgModule} from '@angular/core';
import {NZ_I18N} from 'ng-zorro-antd/i18n';
import {zh_CN} from 'ng-zorro-antd/i18n';
import {NZ_ICONS, NzIconModule} from 'ng-zorro-antd/icon';
import {MenuFoldOutline, MenuUnfoldOutline, FormOutline, DashboardOutline} from '@ant-design/icons-angular/icons';
import {NZ_CONFIG, NzConfig} from "ng-zorro-antd/core/config";
import {NzLayoutModule} from 'ng-zorro-antd/layout';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzSpaceModule} from "ng-zorro-antd/space";
import {NzSpinModule} from "ng-zorro-antd/spin";
import {NzModalModule} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {NzTabsModule} from "ng-zorro-antd/tabs";
import {NzCardModule} from "ng-zorro-antd/card";
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzFormModule} from "ng-zorro-antd/form";
import {NzInputModule} from "ng-zorro-antd/input";
import {NzButtonModule} from "ng-zorro-antd/button";
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';
import {NzTableModule} from "ng-zorro-antd/table";

//Ng-Zorro全局配置
const ngZorroConfig: NzConfig = {
    message: {nzTop: 300}
};

/*Ng-Zorro模块*/
@NgModule({
    imports: [
        NzIconModule,
        NzLayoutModule,
        NzGridModule,
        NzSpaceModule,
        NzSpinModule,
        NzModalModule,
        NzTabsModule,
        NzCardModule,
        NzMenuModule,
        NzFormModule,
        NzInputModule,
        NzButtonModule,
        NzDropDownModule,
        NzTableModule
    ],
    exports: [
        NzIconModule,
        NzLayoutModule,
        NzGridModule,
        NzSpaceModule,
        NzSpinModule,
        NzModalModule,
        NzTabsModule,
        NzCardModule,
        NzMenuModule,
        NzFormModule,
        NzInputModule,
        NzButtonModule,
        NzDropDownModule,
        NzTableModule
    ],
    providers: [
        {provide: NZ_ICONS, useValue: [MenuFoldOutline, MenuUnfoldOutline, DashboardOutline, FormOutline]},
        {provide: NZ_CONFIG, useValue: ngZorroConfig},
        {provide: NZ_I18N, useValue: zh_CN},
        NzMessageService
    ]
})
export class NgZorroModule {

}
