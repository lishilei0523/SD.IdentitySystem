import {Component, OnInit} from '@angular/core';
import {ApplicationType, ApplicationTypeDescriptor, PageModel, ComponentBase} from "sd-infrastructure";
import {NzModalService} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {InfoSystemService} from "../../../services/info-system.service";
import {AuthorityService} from "../../../services/authority.service";
import {Authority} from "../../../models/authority";
import {InfoSystem} from "../../../models/info-system";
import {AddComponent} from "../add/add.component";
import {UpdateComponent} from "../update/update.component";

/*权限首页组件*/
@Component({
    selector: 'app-authority-index',
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

    /*权限服务*/
    private readonly _authorityService: AuthorityService;

    /**
     * 创建权限首页组件构造器
     * */
    public constructor(modalService: NzModalService, messageService: NzMessageService, infoSystemService: InfoSystemService, authorityService: AuthorityService) {
        super();
        this._modalService = modalService;
        this._messageService = messageService;
        this._infoSystemService = infoSystemService;
        this._authorityService = authorityService;
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

    /*权限列表*/
    public authorities: Array<Authority> = new Array<Authority>();

    /*信息系统列表*/
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统编号*/
    public selectedInfoSystemNo: string | null = null;

    /*应用程序类型字典*/
    public applicationTypes: Set<{ key: ApplicationType, value: string }> = ApplicationTypeDescriptor.getEnumMembers();

    /*已选应用程序类型*/
    public selectedApplicationType: ApplicationType | null = null;

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
        await this.loadAuthorities();

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
        await this.loadAuthorities();
    }
    //endregion

    //region 重置搜索 —— resetSearch()
    /**
     * 重置搜索
     * */
    public resetSearch(): void {
        this.keywords = null;
        this.selectedInfoSystemNo = null;
        this.selectedApplicationType = null;
    }
    //endregion

    //region 页码改变事件 —— async onPageIndexChange(pageIndex: number)
    /**
     * 页码改变事件
     * @param pageIndex - 页码
     * */
    public async onPageIndexChange(pageIndex: number): Promise<void> {
        this.pageIndex = pageIndex;
        await this.loadAuthorities();
    }
    //endregion

    //region 页容量改变事件 —— async onPageSizeChange(pageSize: number)
    /**
     * 页容量改变事件
     * @param pageSize - 页容量
     * */
    public async onPageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadAuthorities();
    }
    //endregion

    //region 创建权限 —— async createAuthority()
    /**
     * 创建权限
     * */
    public async createAuthority(): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "创建权限",
            nzWidth: "500px",
            nzBodyStyle: {
                height: "450px"
            },
            nzContent: AddComponent,
            nzFooter: null,
            nzComponentParams: {
                infoSystems: this.infoSystems,
                applicationTypes: this.applicationTypes
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadAuthorities();
            }
        });
    }
    //endregion

    //region 修改权限 —— async updateAuthority(authority: Authority)
    /**
     * 修改权限
     * @param authority - 权限
     * */
    public async updateAuthority(authority: Authority): Promise<void> {
        let applicationTypeDescripttor = new ApplicationTypeDescriptor();
        let modalRef = this._modalService.create({
            nzTitle: "修改权限",
            nzWidth: "500px",
            nzBodyStyle: {
                height: "450px"
            },
            nzContent: UpdateComponent,
            nzFooter: null,
            nzComponentParams: {
                authorityId: authority.id,
                infoSystemName: authority.infoSystemInfo?.name,
                applicationTypeName: applicationTypeDescripttor.transform(authority.applicationType),
                authorityName: authority.name,
                authorityPath: authority.authorityPath,
                description: authority.description
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadAuthorities();
            }
        });
    }
    //endregion

    //region 删除权限 —— async removeAuthority(authorityId: string)
    /**
     * 删除权限
     * @param authorityId - 权限Id
     * */
    public async removeAuthority(authorityId: string): Promise<void> {
        this._modalService.confirm({
            nzTitle: "警告",
            nzContent: "确定要删除吗？",
            nzOnOk: async () => {
                await this._authorityService.removeAuthority(authorityId);
                await this.loadAuthorities();
            }
        });
    }
    //endregion

    //region 批量删除权限 —— async removeAuthorities()
    /**
     * 批量删除权限
     * */
    public async removeAuthorities(): Promise<void> {
        if (this.checkedIds == null || this.checkedIds.size == 0) {
            this._messageService.error("请勾选要删除的权限！");
        } else {
            this._modalService.confirm({
                nzTitle: "警告",
                nzContent: "确定要删除吗？",
                nzOnOk: async () => {
                    for (const authorityId of this.checkedIds) {
                        await this._authorityService.removeAuthority(authorityId);
                    }
                    await this.loadAuthorities();
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
        this.authorities.forEach(authority => this.refreshChecked(authority.id, checked));
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
        this.busy();

        let promise = this._authorityService.getAuthoritiesByPage(this.keywords, this.selectedInfoSystemNo, this.selectedApplicationType, this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<Authority> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.authorities = pageModel.datas;

        this.idle();
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
            this.checkedIds.add(authorityId);
        } else {
            this.checkedIds.delete(authorityId);
        }
        this.checkedAll = this.authorities.every(authority => this.checkedIds.has(authority.id));
    }
    //endregion

    //endregion
}
