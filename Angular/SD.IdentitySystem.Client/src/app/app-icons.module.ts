import {NgModule} from '@angular/core';
import {NZ_ICONS, NzIconModule} from 'ng-zorro-antd/icon';
import {MenuFoldOutline, MenuUnfoldOutline, FormOutline, DashboardOutline} from '@ant-design/icons-angular/icons';

/*Ng-Zorro图标模块*/
@NgModule({
    imports: [NzIconModule],
    exports: [NzIconModule],
    providers: [
        {provide: NZ_ICONS, useValue: [MenuFoldOutline, MenuUnfoldOutline, DashboardOutline, FormOutline]}
    ]
})
export class AppIconsModule {

}
