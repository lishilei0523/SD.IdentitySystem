import {Component, Input, OnInit} from '@angular/core';
import {NzModalRef} from "ng-zorro-antd/modal";
import {ApplicationType, ComponentBase} from "sd-infrastructure";
import {Authority} from "../../../models/authority";
import {AuthorityService} from "../../../services/authority.service";
import {MenuService} from "../../../services/menu.service";

/*菜单关联权限组件*/
@Component({
    selector: 'app-menu-relate-authority',
    templateUrl: './relate-authority.component.html',
    styleUrls: ['./relate-authority.component.css']
})
export class RelateAuthorityComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*权限服务*/
    private readonly _authorityService: AuthorityService;

    /*菜单服务*/
    private readonly _menuService: MenuService;

    /**
     * 创建菜单关联权限组件构造器
     * */
    public constructor(modalRef: NzModalRef, authorityService: AuthorityService, menuService: MenuService) {
        super();
        this._modalRef = modalRef;
        this._authorityService = authorityService;
        this._menuService = menuService;
    }

    //endregion

    //region # 属性

    /*菜单Id*/
    @Input()
    public menuId: string = "";

    /*信息系统编号*/
    @Input()
    public infoSystemNo: string = "";

    /*应用程序类型*/
    @Input()
    public applicationType: ApplicationType | null = null;

    /*权限列表*/
    public authorities: Array<Authority> = new Array<Authority>();

    /*选中权限Id列表*/
    public checkedAuthorityIds: Set<string> = new Set<string>();

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        //加载权限列表
        await this.loadAuthorities();
    }
    //endregion


    //Actions

    //region 提交 —— async submit()
    /**
     * 提交
     * */
    public async submit(): Promise<void> {
        this.busy();

        let authorityIds: Array<string> = Array.from(this.checkedAuthorityIds);
        let promise: Promise<void> = this._menuService.relateAuthoritiesToMenu(this.menuId, authorityIds);
        promise.catch(_ => {
            this.idle();
        });
        await promise;

        this.idle();
        this._modalRef.close(true);
    }
    //endregion

    //region 取消 —— cancel()
    /**
     * 取消
     * */
    public cancel(): void {
        this._modalRef.close(false);
    }
    //endregion

    //region 勾选 —— checkItem(authorityId: string, checked: boolean)
    /**
     * 勾选
     * @param authorityId - 权限Id
     * @param checked - 是否勾选
     * */
    public checkItem(authorityId: string, checked: boolean): void {
        this.refreshChecked(authorityId, checked);
    }
    //endregion


    //Private

    //region 加载权限列表 —— async loadAuthorities()
    /**
     * 加载权限列表
     * */
    private async loadAuthorities(): Promise<void> {
        let authorities: Array<Authority> = await this._authorityService.getAuthorities(null, this.infoSystemNo, this.applicationType, null, null);
        let menuAuthorities: Array<Authority> = await this._authorityService.getAuthorities(null, this.infoSystemNo, this.applicationType, this.menuId, null);

        this.authorities = authorities;
        this.checkedAuthorityIds = new Set<string>(menuAuthorities.map(x => x.id));
    }
    //endregion

    //region 刷新勾选 —— refreshChecked(authorityId: string, checked: boolean)
    /**
     * 刷新勾选
     * @param authorityId - 权限Id
     * @param checked - 是否勾选
     * */
    private refreshChecked(authorityId: string, checked: boolean): void {
        if (checked) {
            this.checkedAuthorityIds.add(authorityId);
        } else {
            this.checkedAuthorityIds.delete(authorityId);
        }
    }
    //endregion

    //endregion
}
