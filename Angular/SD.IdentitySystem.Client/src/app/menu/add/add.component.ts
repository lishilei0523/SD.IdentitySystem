import {Component, Input, OnInit} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {ComponentBase} from "../../../base/component.base";
import {ApplicationType} from "../../../values/enums/application-type";
import {ApplicationTypeDescriptor} from "../../../values/enums/application-type.descriptor";
import {NzNode} from "../../../values/structs/nz-node";
import {InfoSystem} from "../../../models/info-system";
import {Menu} from "../../../models/menu";
import {MenuService} from "../../../services/menu.service";
import {MenuMap} from "../../../maps/menu.map";

/*菜单创建组件*/
@Component({
    selector: 'app-menu-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*消息服务*/
    private readonly _messageService: NzMessageService;

    /*表单建造者*/
    private readonly _formBuilder: UntypedFormBuilder;

    /*菜单服务*/
    private readonly _menuService: MenuService;

    /**
     * 创建菜单创建组件构造器
     * */
    public constructor(modalRef: NzModalRef, messageService: NzMessageService, formBuilder: UntypedFormBuilder, menuService: MenuService) {
        super();
        this._modalRef = modalRef;
        this._messageService = messageService;
        this._formBuilder = formBuilder;
        this._menuService = menuService;
    }

    //endregion

    //region # 属性

    /*上级菜单名称*/
    public parentMenuName: string | null = null;

    /*菜单名称*/
    public menuName: string = "";

    /*链接地址*/
    public url: string | null = null;

    /*路径*/
    public path: string | null = null;

    /*图标*/
    public icon: string | null = null;

    /*排序*/
    public sort: number | null = null;

    /*信息系统列表*/
    @Input()
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统*/
    public selectedInfoSystemNo: string | null = null;

    /*应用程序类型字典*/
    @Input()
    public applicationTypes: Set<{ key: ApplicationType, value: string }> = ApplicationTypeDescriptor.getEnumMembers();

    /*已选应用程序类型*/
    public selectedApplicationType: ApplicationType | null = null;

    /*菜单树*/
    public menuTree: Array<NzNode> = new Array<NzNode>();

    /*已选上级菜单*/
    public selectedParentMenu: NzNode | null = null;

    /*表单*/
    public formGroup!: UntypedFormGroup;

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— ngOnInit()
    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        //初始化表单
        this.formGroup = this._formBuilder.group({
            selectedInfoSystemNo: [null, [Validators.required]],
            selectedApplicationType: [null, [Validators.required]],
            parentMenuName: [null],
            menuName: [null, [Validators.required]],
            url: [null],
            path: [null],
            icon: [null],
            sort: [null, [Validators.required]],
        });
    }
    //endregion


    //Actions

    //region 加载菜单树 —— async loadMenuTree()
    /**
     * 加载菜单树
     * */
    public async loadMenuTree(): Promise<void> {
        if (!this.selectedInfoSystemNo || this.selectedApplicationType == null) {
            this.menuTree = new Array<NzNode>();
            return;
        }

        this.busy();

        let menus: Array<Menu> = await this._menuService.getMenus(this.selectedInfoSystemNo, this.selectedApplicationType);
        this.menuTree = MenuMap.toNzTree(menus, null);

        this.idle();
    }
    //endregion

    //region 选中上级菜单 —— selectParentMenu(eventArgs: any)
    /**
     * 选中上级菜单
     * @param eventArgs - 事件参数
     * */
    public selectParentMenu(eventArgs: any): void {
        let nzNode: NzNode = eventArgs.node;
        this.parentMenuName = nzNode.title;
        this.selectedParentMenu = nzNode;
    }
    //endregion

    //region 清空已选上级菜单 —— clearParentMenu()
    /**
     * 清空已选上级菜单
     * */
    public clearParentMenu(): void {
        this.parentMenuName = "";
        this.selectedParentMenu = null;
    }
    //endregion

    //region 提交 —— async submit()
    /**
     * 提交
     * */
    public async submit(): Promise<void> {
        for (let index in this.formGroup.controls) {
            this.formGroup.controls[index].markAsDirty();
            this.formGroup.controls[index].updateValueAndValidity();
        }

        if (this.formGroup.valid) {
            this.busy();

            let parentMenuId = this.selectedParentMenu ? this.selectedParentMenu.key : null;
            let promise: Promise<void> = this._menuService.createMenu(this.selectedInfoSystemNo!, this.selectedApplicationType!, this.menuName, this.sort!, this.url, this.path, this.icon, parentMenuId);
            promise.catch(_ => {
                this.idle();
            });
            await promise;

            this.idle();
            this._modalRef.close(true);
        }
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

    //endregion
}
