import {Component, OnInit} from '@angular/core';
import {Common} from "../../../extentions/common";
import {BaseComponent} from "../../../extentions/base.component";
import {LoginRecord} from "../login-record.model";
import {LoginRecordService} from "../login-record.service";
import {PageModel} from "../../../values/structs/page-model";

/*登录记录首页组件*/
@Component({
    selector: 'app-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent extends BaseComponent implements OnInit {

    /*登录记录服务*/
    private readonly loginRecordService: LoginRecordService;

    /*关键字*/
    public keywords: string;

    /*开始时间*/
    public startTime: string;

    /*结束时间*/
    public endTime: string;

    /*页码*/
    public pageIndex: number;

    /*页容量*/
    public pageSize: number;

    /*总记录数*/
    public rowCount: number;

    /*总页数*/
    public pageCount: number;

    /*登录记录列表*/
    public loginRecords: Array<LoginRecord>;

    /*选中项列表*/
    public checkedItems: Array<LoginRecord>;

    /**
     * 创建登录记录首页组件构造器
     * */
    public constructor(loginRecordService: LoginRecordService) {
        super();
        this.loginRecordService = loginRecordService;
        this.keywords = "";
        this.startTime = "";
        this.endTime = "";
        this.pageIndex = 1;
        this.pageSize = 20;
        this.rowCount = 0;
        this.pageCount = 0
        this.loginRecords = new Array<LoginRecord>();
        this.checkedItems = new Array<LoginRecord>();
    }

    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        await this.loadLoginRecords();
    }

    /**
     * 搜索
     * */
    public async search(): Promise<void> {
        this.pageIndex = 1;
        await this.loadLoginRecords();
    }

    /**
     * 页码改变事件
     * */
    public async pageIndexChange(pageIndex: number): Promise<void> {
        this.pageIndex = pageIndex;
        await this.loadLoginRecords();
    }

    /**
     * 页容量改变事件
     * */
    public async pageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadLoginRecords();
    }

    /**
     * 重置表单
     * */
    public resetForm(): void {
        console.log(this.keywords);
        this.keywords = "";
        this.startTime = "";
        this.endTime = "";
    }

    /**
     * 加载登录记录列表
     * */
    private async loadLoginRecords(): Promise<void> {
        this.busy();

        let startTime = Common.formatDate(this.startTime);
        let endTime = Common.formatDate(this.endTime);

        console.log(this.startTime);
        console.log(this.endTime);
        console.log(startTime);
        console.log(endTime);

        let promise = this.loginRecordService.getLoginRecordsByPage(this.keywords, startTime, endTime, this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<LoginRecord> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.loginRecords = pageModel.datas;

        this.idle();
    }
}
