import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {MenuService} from "../../menu/menu.service";
import {Menu} from "../../../models/menu";
import {Tab} from "../../../models/tab";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    private router: Router;
    private activatedRoute: ActivatedRoute;
    private menuService: MenuService;
    public menuCollapsed: boolean;
    public bingHidden: boolean;
    public menus: Array<Menu>;
    public tabs: Array<Tab>;
    public activeIndex: number;

    public constructor(router: Router, activatedRoute: ActivatedRoute, menuService: MenuService) {
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.menuService = menuService;
        this.menuCollapsed = false;
        this.bingHidden = false;
        this.menus = new Array<Menu>();
        this.tabs = new Array<any>();
        this.activeIndex = 1;
    }

    public ngOnInit(): void {
        this.menus = this.menuService.getMenus();
    }

    public navigate(menu: Menu): void {
        this.clearSelectedTabs();

        if (this.tabs.some(x => x.menuId == menu.id)) {
            let index: number = this.tabs.findIndex(x => x.menuId == menu.id);
            this.tabs[index].selected = true;
            this.activeIndex = index;
        } else {
            let tab: Tab = new Tab(menu.id, menu.name, menu.url, this.tabs.length + 1, true);
            this.tabs.push(tab);
            this.activeIndex = this.tabs.length;
            console.log("新开");
        }

        this.refreshBing();
    }

    public changeTab(eventArgs: any): void {
        let index: number = eventArgs.index;
        let tab: Tab = this.tabs[index];

        this.clearSelectedTabs();

        tab.selected = true;
        this.activeIndex = index;

        this.router.navigate([tab.menuUrl]).then(_ => {
        });
    }

    public closeTab(tab: Tab): void {
        let index: number = this.tabs.findIndex(x => x.menuId == tab.menuId);
        this.tabs.splice(index, 1);

        if (tab.selected) {
            this.clearSelectedTabs();
            if (this.tabs.length > 0) {
                this.tabs[0].selected = true;
                this.router.navigate([this.tabs[0].menuUrl]);
            }
        }

        this.refreshBing();
    }

    private refreshBing(): void {
        this.bingHidden = this.tabs.length != 0;
    }

    private clearSelectedTabs(): void {
        for (let tab of this.tabs) {
            tab.selected = false;
        }
    }
}
