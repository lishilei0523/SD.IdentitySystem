import {NgModule} from '@angular/core';
import {NZ_I18N} from 'ng-zorro-antd/i18n';
import {zh_CN} from 'ng-zorro-antd/i18n';
import {NZ_ICONS, NzIconModule} from 'ng-zorro-antd/icon';
import {MenuFoldOutline, MenuUnfoldOutline, FormOutline, DashboardOutline} from '@ant-design/icons-angular/icons';
import {NzLayoutModule} from 'ng-zorro-antd/layout';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzDropDownModule} from 'ng-zorro-antd/dropdown';
import {NzSpinModule} from "ng-zorro-antd/spin";

//Angular本地化
import {registerLocaleData} from '@angular/common';
import zh from '@angular/common/locales/zh';

registerLocaleData(zh);

/*Ng-Zorro模块*/
@NgModule({
    imports: [NzIconModule, NzLayoutModule, NzGridModule, NzMenuModule, NzDropDownModule, NzSpinModule],
    exports: [NzIconModule, NzLayoutModule, NzGridModule, NzMenuModule, NzDropDownModule, NzSpinModule],
    providers: [
        {provide: NZ_ICONS, useValue: [MenuFoldOutline, MenuUnfoldOutline, DashboardOutline, FormOutline]},
        {provide: NZ_I18N, useValue: zh_CN}]
})
export class UiZorroModule {

}
