import {Component} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {NzModalService} from "ng-zorro-antd/modal";
import {Membership} from "../../../values/constants/membership";
import {Tab} from "../../../values/structs/tab";
import {LoginMenuInfo} from "../../../values/structs/login-menu-info";
import {LoginInfo} from "../../../values/structs/login-info";
import {UpdatePasswordComponent} from "../../user/update-password/update-password.component";

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

    /*模态框服务*/
    private modalService: NzModalService;

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

    /*当前时间*/
    public currentTime: string;

    /**
     * 创建首页组件构造器
     * */
    public constructor(router: Router, activatedRoute: ActivatedRoute, modalService: NzModalService) {

        //依赖注入部分
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.modalService = modalService;

        //默认值部分
        this.menuCollapsed = false;
        this.bingHidden = false;
        this.loginInfo = Membership.loginInfo;
        this.menus = Membership.loginMenus;
        this.tabs = new Array<any>();
        this.activeTabIndex = 1;
        this.currentTime = Date();
        this.initTimer();
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
        }

        //刷新Bing可见性
        this.refreshBingVisibility();
    }

    /**
     * 切换选项卡
     * @param tab - 选项卡
     * */
    public async changeTab(tab: Tab): Promise<void> {
        let index: number = this.tabs.findIndex(x => x.menuId == tab.menuId);

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
     * 注销登录
     * */
    public async logout(): Promise<void> {
        this.modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要注销吗？",
            nzOnOk: async () => {
                Membership.loginInfo = null;
                await this.router.navigate(["/Login"]);
            }
        });
    }

    /**
     * 修改密码
     * */
    public updatePassword(): void {
        this.modalService.create({
            nzTitle: "修改密码",
            nzWidth: "480px",
            nzBodyStyle: {
                height: "290px"
            },
            nzContent: UpdatePasswordComponent,
            nzFooter: null,
            nzComponentParams: {
                loginId: this.loginInfo?.loginId
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

    /**
     * 初始化计时器
     * */
    private initTimer(): void {
        setInterval(() => {
            this.currentTime = Date();
        }, 1000);
    }
}
