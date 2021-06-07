import {Component} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ConfirmationService} from 'primeng/api';
import {Constants} from "../../../values/constants/constants";
import {Tab} from "../../../values/structs/tab";
import {LoginMenuInfo} from "../../../values/structs/loginMenuInfo";
import {LoginInfo} from "../../../values/structs/loginInfo";

/*首页组件*/
@Component({
    selector: 'app-home-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent {

    /*路由器*/
    private readonly router: Router;

    /*活动路由*/
    private readonly activatedRoute: ActivatedRoute;

    /*确认服务*/
    private readonly confirmService: ConfirmationService;

    /*菜单是否折叠*/
    public menuCollapsed: boolean;

    /*Bing是否隐藏*/
    public bingHidden: boolean;

    /*登录信息*/
    public loginInfo: LoginInfo | null;

    /*菜单列表*/
    public menus: Array<LoginMenuInfo>;

    /*选项卡列表*/
    public tabs: Array<Tab>;

    /*活动选项卡索引*/
    public activeTabIndex: number;

    /**
     * 创建首页组件构造器
     * */
    public constructor(router: Router, activatedRoute: ActivatedRoute, confirmService: ConfirmationService) {
        //依赖注入部分
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.confirmService = confirmService;

        //默认值部分
        this.menuCollapsed = false;
        this.bingHidden = false;
        this.loginInfo = Constants.loginInfo;
        this.menus = Constants.userMenus;
        this.tabs = new Array<any>();
        this.activeTabIndex = 1;
    }

    /**
     * 导航至菜单
     * @param menu - 菜单
     * */
    public navigate(menu: LoginMenuInfo): void {
        //清空选中
        this.clearSelectedTabs();

        if (this.tabs.some(x => x.menuId == menu.id)) {
            let index: number = this.tabs.findIndex(x => x.menuId == menu.id);
            this.tabs[index].selected = true;
            this.activeTabIndex = index;
        } else {
            let tab: Tab = new Tab(menu.id, menu.name, menu.url, this.tabs.length + 1, true);
            this.tabs.push(tab);
            this.activeTabIndex = this.tabs.length;
            console.log("新开");
        }

        //刷新Bing可见性
        this.refreshBingVisibility();
    }

    /**
     * 切换选项卡
     * @param eventArgs - 事件参数
     * */
    public async changeTab(eventArgs: any): Promise<void> {
        let index: number = eventArgs.index;
        let tab: Tab = this.tabs[index];

        //清空选中
        this.clearSelectedTabs();

        tab.selected = true;
        this.activeTabIndex = index;
        await this.router.navigate([tab.menuUrl]);
    }

    /**
     * 关闭选项卡
     * @param tab - 选项卡
     * */
    public async closeTab(tab: Tab): Promise<void> {
        let index: number = this.tabs.findIndex(x => x.menuId == tab.menuId);
        this.tabs.splice(index, 1);

        //关闭选中的选项卡处理
        if (tab.selected) {
            this.clearSelectedTabs();
            if (this.tabs.length > 0) {
                this.tabs[0].selected = true;
                await this.router.navigate([this.tabs[0].menuUrl]);
            }
        }

        //刷新Bing可见性
        this.refreshBingVisibility();
    }

    /**
     * 注销
     * */
    public async logout(): Promise<void> {
        this.confirmService.confirm({
            message: "确定要注销吗？",
            accept: async () => {
                Constants.loginInfo = null;
                await this.router.navigate(["/Login"]);
            }
        });
    }

    /**
     * 刷新Bing可见性
     * */
    private refreshBingVisibility(): void {
        this.bingHidden = this.tabs.length != 0;
    }

    /**
     * 清空选项卡选中
     * */
    private clearSelectedTabs(): void {
        for (let tab of this.tabs) {
            tab.selected = false;
        }
    }
}
