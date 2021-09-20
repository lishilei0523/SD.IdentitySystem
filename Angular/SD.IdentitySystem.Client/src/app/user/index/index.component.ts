import {Component, OnInit} from '@angular/core';
import {Constants, PageModel, ComponentBase} from "sd-infrastructure";
import {NzModalService} from "ng-zorro-antd/modal";
import {UserService} from "../../../services/user.service";
import {User} from "../../../models/user";
import {InfoSystem} from "../../../models/info-system";
import {InfoSystemService} from "../../../services/info-system.service";

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

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /*用户服务*/
    private readonly _userService: UserService;

    /**
     * 创建用户首页组件构造器
     * */
    public constructor(modalService: NzModalService, infoSystemService: InfoSystemService, userService: UserService) {
        super();
        this._modalService = modalService;
        this._infoSystemService = infoSystemService;
        this._userService = userService;
    }

    //endregion

    //region # 属性

    /*关键字*/
    public keywords: string = "";

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
        let infoSystemsPageModel = await this._infoSystemService.getInfoSystemsByPage(Constants.stringEmpty, 1, Constants.intMaxValue);
        this.infoSystems = infoSystemsPageModel.datas;

        await this.loadUsers();
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
        this.keywords = "";
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
