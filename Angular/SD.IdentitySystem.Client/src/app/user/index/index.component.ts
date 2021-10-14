import {Component, OnInit} from '@angular/core';
import {PageModel, ComponentBase} from "sd-infrastructure";
import {NzModalService} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {User} from "../../../models/user";
import {InfoSystem} from "../../../models/info-system";
import {UserService} from "../../../services/user.service";
import {InfoSystemService} from "../../../services/info-system.service";
import {AddComponent} from "../add/add.component";
import {ResetPasswordComponent} from "../reset-password/reset-password.component";
import {ResetPrivateKeyComponent} from "../reset-private-key/reset-private-key.component";
import {RelateRoleComponent} from "../relate-role/relate-role.component";

/*用户首页组件*/
@Component({
    selector: 'app-user-index',
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

    /*用户服务*/
    private readonly _userService: UserService;

    /**
     * 创建用户首页组件构造器
     * */
    public constructor(modalService: NzModalService, messageService: NzMessageService, infoSystemService: InfoSystemService, userService: UserService) {
        super();
        this._modalService = modalService;
        this._messageService = messageService;
        this._infoSystemService = infoSystemService;
        this._userService = userService;
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

    /*用户列表*/
    public users: Array<User> = new Array<User>();

    /*信息系统列表*/
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统编号*/
    public selectedInfoSystemNo: string | null = null;

    /*是否全选*/
    public checkedAll = false;

    /*选中项列表*/
    public checkedLoginIds: Set<string> = new Set<string>();

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— async ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        await this.loadUsers();

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
        await this.loadUsers();
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
        await this.loadUsers();
    }
    //endregion

    //region 页容量改变事件 —— async onPageSizeChange(pageSize: number)
    /**
     * 页容量改变事件
     * @param pageSize - 页容量
     * */
    public async onPageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadUsers();
    }
    //endregion

    //region 创建用户 —— async createUser()
    /**
     * 创建用户
     * */
    public async createUser(): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "创建用户",
            nzWidth: "460px",
            nzBodyStyle: {
                height: "320px"
            },
            nzContent: AddComponent,
            nzFooter: null
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadUsers();
            }
        });
    }
    //endregion

    //region 重置密码 —— async resetPassword(user: User)
    /**
     * 重置密码
     * @param user - 用户
     * */
    public async resetPassword(user: User): Promise<void> {
        this._modalService.create({
            nzTitle: "重置密码",
            nzWidth: "460px",
            nzBodyStyle: {
                height: "220px"
            },
            nzContent: ResetPasswordComponent,
            nzFooter: null,
            nzComponentParams: {
                loginId: user.number
            }
        });
    }
    //endregion

    //region 重置私钥 —— async resetPrivateKey(user: User)
    /**
     * 重置私钥
     * @param user - 用户
     * */
    public async resetPrivateKey(user: User): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "重置私钥",
            nzWidth: "460px",
            nzBodyStyle: {
                height: "200px"
            },
            nzContent: ResetPrivateKeyComponent,
            nzFooter: null,
            nzComponentParams: {
                loginId: user.number,
                privateKey: user.privateKey
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadUsers();
            }
        });
    }
    //endregion

    //region 分配角色 —— async relateRoles(user: User)
    /**
     * 分配角色
     * @param user - 用户
     * */
    public async relateRoles(user: User): Promise<void> {
        this._modalService.create({
            nzTitle: "分配角色",
            nzWidth: "460px",
            nzBodyStyle: {
                padding: "0",
                height: "550px"
            },
            nzContent: RelateRoleComponent,
            nzFooter: null,
            nzComponentParams: {
                loginId: user.number
            }
        });
    }
    //endregion

    //region 删除用户 —— async removeUser(loginId: string)
    /**
     * 删除用户
     * @param loginId - 用户名
     * */
    public async removeUser(loginId: string): Promise<void> {
        this._modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要删除吗？",
            nzOnOk: async () => {
                await this._userService.removeUser(loginId);
                await this.loadUsers();
            }
        });
    }
    //endregion

    //region 批量删除用户 —— async removeUsers()
    /**
     * 批量删除用户
     * */
    public async removeUsers(): Promise<void> {
        if (this.checkedLoginIds == null || this.checkedLoginIds.size == 0) {
            this._messageService.error("请勾选要删除的用户！");
        } else {
            this._modalService.confirm({
                nzTitle: "警告",
                nzContent: "确定要删除吗？",
                nzOnOk: async () => {
                    for (const loginId of this.checkedLoginIds) {
                        await this._userService.removeUser(loginId);
                    }
                    await this.loadUsers();
                }
            });
        }
    }
    //endregion

    //region 启用用户 —— async enableUser(loginId: string)
    /**
     * 启用用户
     * @param loginId - 用户名
     * */
    public async enableUser(loginId: string) {
        this._modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要启用吗？",
            nzOnOk: async () => {
                await this._userService.enableUser(loginId);
                await this.loadUsers();
            }
        });
    }
    //endregion

    //region 停用用户 —— async disableUser(loginId: string)
    /**
     * 停用用户
     * @param loginId - 用户名
     * */
    public async disableUser(loginId: string) {
        this._modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要停用吗？",
            nzOnOk: async () => {
                await this._userService.disableUser(loginId);
                await this.loadUsers();
            }
        });
    }
    //endregion

    //region 勾选全部 —— checkAll(checked: boolean)
    /**
     * 勾选全部
     * @param checked - 是否勾选
     * */
    public checkAll(checked: boolean): void {
        this.users.forEach(user => this.refreshChecked(user.number, checked));
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


    //Private

    //region 加载用户列表 —— async loadUsers()
    /**
     * 加载用户列表
     * */
    private async loadUsers(): Promise<void> {
        this.busy();

        let promise = this._userService.getUsersByPage(this.keywords, this.selectedInfoSystemNo, "", this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<User> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.users = pageModel.datas;

        this.idle();
    }
    //endregion

    //region 刷新勾选 —— refreshChecked(loginId: string, checked: boolean)
    /**
     * 刷新勾选
     * @param loginId - 用户名
     * @param checked - 是否勾选
     * */
    private refreshChecked(loginId: string, checked: boolean): void {
        if (checked) {
            this.checkedLoginIds.add(loginId);
        } else {
            this.checkedLoginIds.delete(loginId);
        }
        this.checkedAll = this.users.every(user => this.checkedLoginIds.has(user.number));
    }
    //endregion

    //endregion
}
