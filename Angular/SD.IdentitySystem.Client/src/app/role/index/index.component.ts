import {Component, OnInit} from '@angular/core';
import {NzModalService} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {ComponentBase} from "../../../base/component.base";
import {PageModel} from "../../../values/structs/page-model";
import {InfoSystem} from "../../../models/info-system";
import {InfoSystemService} from "../../../services/info-system.service";
import {Role} from "../../../models/role";
import {RoleService} from "../../../services/role.service";
import {AddComponent} from "../add/add.component";
import {UpdateComponent} from "../update/update.component";

/*角色首页组件*/
@Component({
    selector: 'app-role-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*模态框服务*/
    private readonly _modalService: NzModalService;

    /*消息服务*/
    private readonly _messageService: NzMessageService;

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /*角色服务*/
    private readonly _roleService: RoleService;

    /**
     * 创建角色首页组件构造器
     * */
    public constructor(modalService: NzModalService, messageService: NzMessageService, infoSystemService: InfoSystemService, roleService: RoleService) {
        super();
        this._modalService = modalService;
        this._messageService = messageService;
        this._infoSystemService = infoSystemService;
        this._roleService = roleService;
    }

    //endregion

    //region # 属性

    /*关键字*/
    public keywords: string | null = null;

    /*页码*/
    public pageIndex: number = 1;

    /*页容量*/
    public pageSize: number = 20;

    /*总记录数*/
    public rowCount: number = 0;

    /*总页数*/
    public pageCount: number = 0;

    /*角色列表*/
    public roles: Array<Role> = new Array<Role>();

    /*信息系统列表*/
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统编号*/
    public selectedInfoSystemNo: string | null = null;

    /*是否全选*/
    public checkedAll = false;

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
        await this.loadRoles();

        this.infoSystems = await this._infoSystemService.getInfoSystems();
    }
    //endregion


    //Actions

    //region 搜索 —— async search()
    /**
     * 搜索
     * */
    public async search(): Promise<void> {
        this.pageIndex = 1;
        await this.loadRoles();
    }
    //endregion

    //region 重置搜索 —— resetSearch()
    /**
     * 重置搜索
     * */
    public resetSearch(): void {
        this.keywords = null;
        this.selectedInfoSystemNo = null;
    }
    //endregion

    //region 页码改变事件 —— async onPageIndexChange(pageIndex: number)
    /**
     * 页码改变事件
     * @param pageIndex - 页码
     * */
    public async onPageIndexChange(pageIndex: number): Promise<void> {
        this.pageIndex = pageIndex;
        await this.loadRoles();
    }
    //endregion

    //region 页容量改变事件 —— async onPageSizeChange(pageSize: number)
    /**
     * 页容量改变事件
     * @param pageSize - 页容量
     * */
    public async onPageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadRoles();
    }
    //endregion

    //region 创建角色 —— async createRole()
    /**
     * 创建角色
     * */
    public async createRole(): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "创建角色",
            nzWidth: "900px",
            nzBodyStyle: {
                height: "650px"
            },
            nzContent: AddComponent,
            nzFooter: null,
            nzComponentParams: {
                infoSystems: this.infoSystems
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadRoles();
            }
        });
    }
    //endregion

    //region 修改角色 —— async updateRole(role: Role)
    /**
     * 修改角色
     * */
    public async updateRole(role: Role): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "修改角色",
            nzWidth: "900px",
            nzBodyStyle: {
                height: "650px"
            },
            nzContent: UpdateComponent,
            nzFooter: null,
            nzComponentParams: {
                roleId: role.id,
                infoSystemNo: role.infoSystemInfo?.number,
                infoSystemName: role.infoSystemInfo?.name,
                roleName: role.name,
                description: role.description
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadRoles();
            }
        });
    }
    //endregion

    //region 删除角色 —— async removeRole(roleId: string)
    /**
     * 删除角色
     * @param roleId - 角色Id
     * */
    public async removeRole(roleId: string): Promise<void> {
        this._modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要删除吗？",
            nzOnOk: async () => {
                await this._roleService.removeRole(roleId);
                await this.loadRoles();
            }
        });
    }
    //endregion

    //region 批量删除角色 —— async removeRoles()
    /**
     * 批量删除角色
     * */
    public async removeRoles(): Promise<void> {
        if (this.checkedIds == null || this.checkedIds.size == 0) {
            this._messageService.error("请勾选要删除的角色！");
        } else {
            this._modalService.confirm({
                nzTitle: "警告",
                nzContent: "确定要删除吗？",
                nzOnOk: async () => {
                    for (const roleId of this.checkedIds) {
                        await this._roleService.removeRole(roleId);
                    }
                    await this.loadRoles();
                }
            });
        }
    }
    //endregion

    //region 勾选全部 —— checkAll(checked: boolean)
    /**
     * 勾选全部
     * @param checked - 是否勾选
     * */
    public checkAll(checked: boolean): void {
        this.roles.forEach(role => this.refreshChecked(role.id, checked));
    }
    //endregion

    //region 勾选 —— checkItem(roleId: string, checked: boolean)
    /**
     * 勾选
     * @param roleId - 角色Id
     * @param checked - 是否勾选
     * */
    public checkItem(roleId: string, checked: boolean): void {
        this.refreshChecked(roleId, checked);
    }
    //endregion


    //Private

    //region 加载角色列表 —— async loadRoles()
    /**
     * 加载角色列表
     * */
    private async loadRoles(): Promise<void> {
        this.busy();

        let promise = this._roleService.getRolesByPage(this.keywords, this.selectedInfoSystemNo, this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<Role> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.roles = pageModel.datas;

        this.idle();
    }
    //endregion

    //region 刷新勾选 —— refreshChecked(roleId: string, checked: boolean)
    /**
     * 刷新勾选
     * @param roleId - 角色Id
     * @param checked - 是否勾选
     * */
    private refreshChecked(roleId: string, checked: boolean): void {
        if (checked) {
            this.checkedIds.add(roleId);
        } else {
            this.checkedIds.delete(roleId);
        }
        this.checkedAll = this.roles.every(role => this.checkedIds.has(role.id));
    }
    //endregion

    //endregion
}
