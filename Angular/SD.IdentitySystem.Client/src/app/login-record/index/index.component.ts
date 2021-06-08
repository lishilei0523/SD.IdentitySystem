import {Component, OnInit} from '@angular/core';
import {LoginRecordService} from "../login-record.service";
import {LoginRecord} from "../login-record.model";

/*登录记录首页组件*/
@Component({
    selector: 'app-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

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

    /**
     * 创建登录记录首页组件构造器
     * */
    public constructor(loginRecordService: LoginRecordService) {
        this.loginRecordService = loginRecordService;
        this.loginRecords = new Array<LoginRecord>();
        this.keywords = "";
        this.startTime = "";
        this.endTime = "";
        this.pageIndex = 1;
        this.pageSize = 50;
        this.rowCount = 0;
        this.pageCount = 0
    }

    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        let pageModel = await this.loginRecordService.getLoginRecordsByPage(this.keywords, this.startTime, this.endTime, this.pageIndex, this.pageSize);
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.loginRecords = pageModel.datas;
    }
}
