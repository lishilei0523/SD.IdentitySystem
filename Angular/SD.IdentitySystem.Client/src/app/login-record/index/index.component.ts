import {Component, OnInit} from '@angular/core';
import {formatDate} from "@angular/common";
import {ComponentBase} from "../../../base/component.base";
import {Constants} from "../../../values/constants/constants";
import {PageModel} from "../../../values/structs/page-model";
import {LoginRecord} from "../../../models/login-record";
import {LoginRecordService} from "../../../services/login-record.service";

/*登录记录首页组件*/
@Component({
    selector: 'app-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css'],
    standalone: false
})
export class IndexComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*登录记录服务*/
    private readonly _loginRecordService: LoginRecordService;

    /**
     * 创建登录记录首页组件构造器
     * */
    public constructor(loginRecordService: LoginRecordService) {
        super();
        this._loginRecordService = loginRecordService;
    }

    //endregion

    //region # 属性

    /*关键字*/
    public keywords: string | null = null;

    /*开始时间*/
    public startTime: string | null = null;

    /*结束时间*/
    public endTime: string | null = null;

    /*页码*/
    public pageIndex: number = 1;

    /*页容量*/
    public pageSize: number = 20;

    /*总记录数*/
    public rowCount: number = 0;

    /*总页数*/
    public pageCount: number = 0;

    /*登录记录列表*/
    public loginRecords: Array<LoginRecord> = new Array<LoginRecord>();

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— async ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        await this.loadLoginRecords();
    }
    //endregion


    //Actions

    //region 搜索 —— async search()
    /**
     * 搜索
     * */
    public async search(): Promise<void> {
        this.pageIndex = 1;
        await this.loadLoginRecords();
    }
    //endregion

    //region 重置搜索 —— resetSearch()
    /**
     * 重置搜索
     * */
    public resetSearch(): void {
        this.keywords = null;
        this.startTime = null;
        this.endTime = null;
    }
    //endregion

    //region 页码改变事件 —— async onPageIndexChange(pageIndex: number)
    /**
     * 页码改变事件
     * @param pageIndex - 页码
     * */
    public async onPageIndexChange(pageIndex: number): Promise<void> {
        this.pageIndex = pageIndex;
        await this.loadLoginRecords();
    }
    //endregion

    //region 页容量改变事件 —— async onPageSizeChange(pageSize: number)
    /**
     * 页容量改变事件
     * @param pageSize - 页容量
     * */
    public async onPageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadLoginRecords();
    }
    //endregion


    //Private

    //region 加载登录记录列表 —— async loadLoginRecords()
    /**
     * 加载登录记录列表
     * */
    private async loadLoginRecords(): Promise<void> {
        this.busy();

        let startTime = this.startTime ? formatDate(this.startTime, Constants.dateTimeFormat, Constants.locale) : null;
        let endTime = this.endTime ? formatDate(this.endTime, Constants.dateTimeFormat, Constants.locale) : null;
        let promise = this._loginRecordService.getLoginRecordsByPage(this.keywords, startTime, endTime, this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<LoginRecord> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.loginRecords = pageModel.datas;

        this.idle();
    }
    //endregion

    //endregion
}
