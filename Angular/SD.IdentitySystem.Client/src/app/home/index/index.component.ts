import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {MenuService} from "../../menu/menu.service";
import {Menu} from "../../menu/menu.model";
import {Tab} from "../../../values/structs/tab";

/*首页组件*/
@Component({
    selector: 'app-home-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

    /*路由*/
    private router: Router;

    /*活动路由*/
    private activatedRoute: ActivatedRoute;

    /*菜单服务*/
    private menuService: MenuService;

    /*菜单是否折叠*/
    public menuCollapsed: boolean;

    /*Bing是否隐藏*/
    public bingHidden: boolean;

    /*菜单列表*/
    public menus: Array<Menu>;

    /*选项卡列表*/
    public tabs: Array<Tab>;

    /*活动选项卡索引*/
    public activeTabIndex: number;

    /**
     * 创建首页组件构造器
     * */
    public constructor(router: Router, activatedRoute: ActivatedRoute, menuService: MenuService) {
        //依赖注入部分
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.menuService = menuService;

        //默认值部分
        this.menuCollapsed = false;
        this.bingHidden = false;
        this.menus = new Array<Menu>();
        this.tabs = new Array<any>();
        this.activeTabIndex = 1;
    }

    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        this.menus = this.menuService.getMenus();
    }

    /**
     * 导航至菜单
     * @param menu - 菜单
     * */
    public navigate(menu: Menu): void {
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
