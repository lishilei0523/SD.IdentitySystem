import {NgModule} from '@angular/core';

//Angular本地化
import {registerLocaleData} from '@angular/common';
import zh from '@angular/common/locales/zh';

registerLocaleData(zh);

//Ng-Zorro模块
import {NZ_I18N, zh_CN} from 'ng-zorro-antd/i18n';
import {NZ_ICONS, NzIconModule} from 'ng-zorro-antd/icon';
import {MenuFoldOutline, MenuUnfoldOutline, FormOutline, DashboardOutline} from '@ant-design/icons-angular/icons';
import {NZ_CONFIG, NzConfig} from "ng-zorro-antd/core/config";

//布局类
import {NzLayoutModule} from 'ng-zorro-antd/layout';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzCardModule} from "ng-zorro-antd/card";
import {NzSpaceModule} from "ng-zorro-antd/space";

//交互类
import {NzSpinModule} from "ng-zorro-antd/spin";
import {NzModalModule} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";

//菜单类
import {NzTabsModule} from "ng-zorro-antd/tabs";
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';

//表单类
import {NzFormModule} from "ng-zorro-antd/form";
import {NzButtonModule} from "ng-zorro-antd/button";
import {NzInputModule} from "ng-zorro-antd/input";
import {NzInputNumberModule} from 'ng-zorro-antd/input-number';
import {NzSelectModule} from 'ng-zorro-antd/select';
import {NzDatePickerModule} from "ng-zorro-antd/date-picker";

//数据类
import {NzTableModule} from "ng-zorro-antd/table";

//Ng-Zorro全局配置
const ngZorroConfig: NzConfig = {
    message: {nzTop: 300},
    table: {
        nzBordered: true,
        nzSize: "small",
        nzShowSizeChanger: true
    }
};

/*Ng-Zorro模块*/
@NgModule({
    imports: [
        NzIconModule,
        NzLayoutModule,
        NzGridModule,
        NzCardModule,
        NzSpaceModule,
        NzSpinModule,
        NzModalModule,
        NzTabsModule,
        NzMenuModule,
        NzDropDownModule,
        NzFormModule,
        NzButtonModule,
        NzInputModule,
        NzInputNumberModule,
        NzSelectModule,
        NzDatePickerModule,
        NzTableModule
    ],
    exports: [
        NzIconModule,
        NzLayoutModule,
        NzGridModule,
        NzCardModule,
        NzSpaceModule,
        NzSpinModule,
        NzModalModule,
        NzTabsModule,
        NzMenuModule,
        NzDropDownModule,
        NzFormModule,
        NzButtonModule,
        NzInputModule,
        NzInputNumberModule,
        NzSelectModule,
        NzDatePickerModule,
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
