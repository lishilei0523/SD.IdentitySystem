import {Component, OnInit} from '@angular/core';
import {NzModalService} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {ComponentBase} from "../../../base/component.base";
import {ApplicationType} from "../../../values/enums/application-type";
import {ApplicationTypeDescriptor} from "../../../values/enums/application-type.descriptor";
import {InfoSystem} from "../../../models/info-system";
import {InfoSystemService} from "../../../services/info-system.service";
import {Menu} from "../../../models/menu";
import {MenuMap} from "../../../maps/menu.map";
import {MenuService} from "../../../services/menu.service";
import {AddComponent} from "../add/add.component";
import {UpdateComponent} from "../update/update.component";
import {RelateAuthorityComponent} from "../relate-authority/relate-authority.component";

/*菜单首页组件*/
@Component({
    selector: 'app-menu-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css'],
    standalone: false
})
export class IndexComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*模态框服务*/
    private readonly _modalService: NzModalService;

    /*消息服务*/
    private readonly _messageService: NzMessageService;

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /*菜单服务*/
    private readonly _menuService: MenuService;

    /**
     * 创建菜单首页组件构造器
     * */
    public constructor(modalService: NzModalService, messageService: NzMessageService, infoSystemService: InfoSystemService, menuService: MenuService) {
        super();
        this._modalService = modalService;
        this._messageService = messageService;
        this._infoSystemService = infoSystemService;
        this._menuService = menuService;
    }

    //endregion

    //region # 属性

    /*菜单列表*/
    public menus: Array<Menu> = new Array<Menu>();

    /*菜单展开列表*/
    public expandedMenus: { [key: string]: Array<Menu> } = {};

    /*信息系统列表*/
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统*/
    public selectedInfoSystemNo: string | null = null;

    /*应用程序类型字典*/
    public applicationTypes: Set<{ key: ApplicationType, value: string }> = ApplicationTypeDescriptor.getEnumMembers();

    /*已选应用程序类型*/
    public selectedApplicationType: ApplicationType | null = null;

    /*选中项列表*/
    public checkedIds: Set<string> = new Set<string>();

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— async ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        await this.loadMenus();

        this.infoSystems = await this._infoSystemService.getInfoSystems();
    }
    //endregion


    //Actions

    //region 搜索 —— async search()
    /**
     * 搜索
     * */
    public async search(): Promise<void> {
        await this.loadMenus();
    }
    //endregion

    //region 重置搜索 —— resetSearch()
    /**
     * 重置搜索
     * */
    public resetSearch(): void {
        this.selectedInfoSystemNo = null;
        this.selectedApplicationType = null;
    }
    //endregion

    //region 勾选 —— checkItem(loginId: string, checked: boolean)
    /**
     * 勾选
     * @param loginId - 用户名
     * @param checked - 是否勾选
     * */
    public checkItem(loginId: string, checked: boolean): void {
        this.refreshChecked(loginId, checked);
    }
    //endregion

    //region 折叠 —— collapse(menus: Array<Menu>, menu: Menu...
    /**
     * 折叠
     * */
    public collapse(menus: Array<Menu>, menu: Menu, $event: boolean): void {
        if (!$event) {
            if (menu.children) {
                menu.children.forEach(subMenu => {
                    const target = menus.find(menu => menu.id == subMenu.id)!;
                    target.expand = false;
                    this.collapse(menus, target, false);
                });
            } else {
                return;
            }
        }
    }
    //endregion

    //region 创建菜单 —— async createMenu()
    /**
     * 创建菜单
     * */
    public async createMenu(): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "创建菜单",
            nzWidth: "900px",
            nzBodyStyle: {
                height: "650px"
            },
            nzContent: AddComponent,
            nzFooter: null,
            nzData: {
                infoSystems: this.infoSystems,
                applicationTypes: this.applicationTypes
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadMenus();
            }
        });
    }
    //endregion

    //region 修改菜单 —— async updateMenu(menu: Menu)
    /**
     * 修改菜单
     * @param menu - 菜单
     * */
    public async updateMenu(menu: Menu): Promise<void> {
        let applicationTypeDescriptor = new ApplicationTypeDescriptor();
        let modalRef = this._modalService.create({
            nzTitle: "修改菜单",
            nzWidth: "500px",
            nzBodyStyle: {
                height: "560px"
            },
            nzContent: UpdateComponent,
            nzFooter: null,
            nzData: {
                menuId: menu.id,
                infoSystemName: menu.infoSystemInfo?.name,
                applicationTypeName: applicationTypeDescriptor.transform(menu.applicationType),
                parentMenuName: menu.parent?.name,
                menuName: menu.name,
                url: menu.url,
                path: menu.path,
                icon: menu.icon,
                sort: menu.sort
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadMenus();
            }
        });
    }
    //endregion

    //region 删除菜单 —— async removeMenu(menuId: string)
    /**
     * 删除菜单
     * @param menuId - 菜单Id
     * */
    public async removeMenu(menuId: string): Promise<void> {
        this._modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要删除吗？",
            nzOnOk: async () => {
                await this._menuService.removeMenu(menuId);
                await this.loadMenus();
            }
        });
    }
    //endregion

    //region 关联权限 —— async relateAuthorities(menu: Menu)
    /**
     * 关联权限
     * @param menu - 菜单
     * */
    public async relateAuthorities(menu: Menu): Promise<void> {
        this._modalService.create({
            nzTitle: "关联权限",
            nzWidth: "550px",
            nzBodyStyle: {
                padding: "0",
                height: "580px"
            },
            nzContent: RelateAuthorityComponent,
            nzFooter: null,
            nzData: {
                menuId: menu.id,
                infoSystemNo: menu.infoSystemInfo?.number,
                applicationType: menu.applicationType
            }
        });
    }
    //endregion

    //region 批量删除菜单 —— async removeMenus()
    /**
     * 批量删除菜单
     * */
    public async removeMenus(): Promise<void> {
        if (this.checkedIds == null || this.checkedIds.size == 0) {
            this._messageService.error("请勾选要删除的菜单！");
        } else {
            this._modalService.confirm({
                nzTitle: "警告",
                nzContent: "确定要删除吗？",
                nzOnOk: async () => {
                    for (const loginId of this.checkedIds) {
                        await this._menuService.removeMenu(loginId);
                    }
                    await this.loadMenus();
                }
            });
        }
    }
    //endregion


    //Private

    //region 加载菜单列表 —— async loadMenus()
    /**
     * 加载菜单列表
     * */
    private async loadMenus(): Promise<void> {
        this.busy();

        let promise = this._menuService.getMenus(this.selectedInfoSystemNo, this.selectedApplicationType);
        promise.catch(_ => this.idle());

        let menus: Array<Menu> = await promise;
        this.menus = MenuMap.toTreeList(menus);

        this.expandedMenus = {};
        this.menus.forEach(menu => this.expandedMenus[menu.id] = this.convertTreeToArray(menu));

        this.idle();
    }
    //endregion

    //region 刷新勾选 —— refreshChecked(menuId: string, checked: boolean)
    /**
     * 刷新勾选
     * @param menuId - 菜单Id
     * @param checked - 是否勾选
     * */
    private refreshChecked(menuId: string, checked: boolean): void {
        if (checked) {
            this.checkedIds.add(menuId);
        } else {
            this.checkedIds.delete(menuId);
        }
    }
    //endregion

    //region 转换树为列表 —— convertTreeToArray(rootMenu: Menu)
    /**
     * 转换树为列表
     * @param rootMenu - 根级菜单
     * */
    private convertTreeToArray(rootMenu: Menu): Array<Menu> {
        const stack: Array<Menu> = new Array<Menu>();
        const array: Array<Menu> = new Array<Menu>();
        const hashMap = {};
        stack.push({...rootMenu, level: 0, expand: false});

        while (stack.length != 0) {
            const node = stack.pop()!;
            this.visitNode(node, array, hashMap);
            if (node.children) {
                for (let index = node.children.length - 1; index >= 0; index--) {
                    stack.push({...node.children[index], level: node.level! + 1, expand: false, parent: node});
                }
            }
        }

        return array;
    }
    //endregion

    //region 访问节点 —— visitNode(menu: Menu, menus: Array<Menu>, dictionary...
    /**
     * 访问节点
     * @param menu - 菜单
     * @param menus - 菜单列表
     * @param dictionary - 键：菜单Id，值：是否已存在
     * */
    private visitNode(menu: Menu, menus: Array<Menu>, dictionary: { [key: string]: boolean }): void {
        if (!dictionary[menu.id]) {
            menus.push(menu);
            dictionary[menu.id] = true;
        }
    }
    //endregion

    //endregion
}
