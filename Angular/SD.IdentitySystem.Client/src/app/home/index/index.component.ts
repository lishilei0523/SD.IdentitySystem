import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {NzModalService} from "ng-zorro-antd/modal";
import {TabRouteReuseStrategy} from "../../../extentions/route-reuse.strategy";
import {Membership} from "../../../values/constants/membership";
import {Tab} from "../../../values/structs/tab";
import {LoginMenuInfo} from "../../../values/structs/login-menu-info";
import {LoginInfo} from "../../../values/structs/login-info";
import {UpdatePasswordComponent} from "../update-password/update-password.component";

/*首页组件*/
@Component({
    selector: 'app-home-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit{

    //region # 字段及构造器

    /*路由器*/
    private readonly router: Router;

    /*模态框服务*/
    private readonly modalService: NzModalService;

    /**
     * 创建首页组件构造器
     * */
    public constructor(router: Router, modalService: NzModalService) {
        this.router = router;
        this.modalService = modalService;
    }

    //endregion

    //region # 属性

    /*菜单是否折叠*/
    public menuCollapsed: boolean = false;

    /*Bing是否隐藏*/
    public bingHidden: boolean = false;

    /*登录信息*/
    public loginInfo: LoginInfo | null = Membership.loginInfo;

    /*菜单列表*/
    public menus: Array<LoginMenuInfo> = Membership.loginMenus;

    /*选项卡列表*/
    public tabs: Array<Tab> = new Array<any>();

    /*活动选项卡索引*/
    public activeTabIndex: number = 1;

    /*当前时间*/
    public currentTime: string = Date();

    //endregion

    //region # 方法
    
    //Initializations

    //region 初始化组件 —— ngOnInit()
    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        this.initTimer();
    }
    //endregion


    //Actions

    //region 导航至菜单 —— navigate(menu: LoginMenuInfo)
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
    //endregion

    //region 切换选项卡 —— async changeTab(tab: Tab)
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
    //endregion

    //region 关闭选项卡 —— async closeTab(tab: Tab)
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
            } else {
                await this.router.navigate(["/Home"]);
            }
        }

        //删除路由快照
        TabRouteReuseStrategy.removeSnapshot(tab.menuUrl);

        //刷新Bing可见性
        this.refreshBingVisibility();
    }
    //endregion

    //region 注销登录 —— async logout()
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
    //endregion

    //region 修改密码 —— updatePassword()
    /**
     * 修改密码
     * */
    public updatePassword(): void {
        this.modalService.create({
            nzTitle: "修改密码",
            nzWidth: "450px",
            nzBodyStyle: {
                height: "260px"
            },
            nzContent: UpdatePasswordComponent,
            nzFooter: null,
            nzComponentParams: {
                loginId: this.loginInfo?.loginId
            }
        });
    }
    //endregion


    //Private

    //region 刷新Bing可见性 —— refreshBingVisibility()
    /**
     * 刷新Bing可见性
     * */
    private refreshBingVisibility(): void {
        this.bingHidden = this.tabs.length != 0;
    }
    //endregion

    //region 清空选项卡选中 —— clearSelectedTabs()
    /**
     * 清空选项卡选中
     * */
    private clearSelectedTabs(): void {
        for (let tab of this.tabs) {
            tab.selected = false;
        }
    }
    //endregion

    //region 初始化计时器 —— initTimer()
    /**
     * 初始化计时器
     * */
    private initTimer(): void {
        setInterval(() => {
            this.currentTime = Date();
        }, 1000);
    }
    //endregion

    //endregion
}
