import {Component, OnInit} from '@angular/core';
import {NzModalService} from "ng-zorro-antd/modal";
import {PageModel, ComponentBase} from "sd-infrastructure";
import {InfoSystem} from "../../../models/info-system";
import {InfoSystemService} from "../../../services/info-system.service";
import {AddComponent} from "../add/add.component";
import {UpdateComponent} from "../update/update.component";
import {InitComponent} from "../init/init.component";

/*信息系统首页组件*/
@Component({
    selector: 'app-info-system-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*模态框服务*/
    private readonly _modalService: NzModalService;

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /**
     * 创建信息系统首页组件构造器
     * */
    public constructor(modalService: NzModalService, infoSystemService: InfoSystemService) {
        super();
        this._modalService = modalService;
        this._infoSystemService = infoSystemService;
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

    /*信息系统列表*/
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— async ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        await this.loadInfoSystems();
    }
    //endregion


    //Actions

    //region 搜索 —— async search()
    /**
     * 搜索
     * */
    public async search(): Promise<void> {
        this.pageIndex = 1;
        await this.loadInfoSystems();
    }
    //endregion

    //region 重置搜索 —— resetSearch()
    /**
     * 重置搜索
     * */
    public resetSearch(): void {
        this.keywords = "";
    }
    //endregion

    //region 创建信息系统 —— async createInfoSystem()
    /**
     * 创建信息系统
     * */
    public async createInfoSystem(): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "创建信息系统",
            nzWidth: "460px",
            nzBodyStyle: {
                height: "320px"
            },
            nzContent: AddComponent,
            nzFooter: null
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadInfoSystems();
            }
        });
    }
    //endregion

    //region 修改信息系统 —— async updateInfoSystem(infoSystem: InfoSystem)
    /**
     * 修改信息系统
     * @param infoSystem - 信息系统
     * */
    public async updateInfoSystem(infoSystem: InfoSystem): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "修改信息系统",
            nzWidth: "460px",
            nzBodyStyle: {
                height: "220px"
            },
            nzContent: UpdateComponent,
            nzFooter: null,
            nzComponentParams: {
                systemId: infoSystem.id,
                systemNo: infoSystem.number,
                systemName: infoSystem.name
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadInfoSystems();
            }
        });
    }
    //endregion

    //region 初始化信息系统 —— async initInfoSystem(infoSystem: InfoSystem)
    /**
     * 初始化信息系统
     * @param infoSystem - 信息系统
     * */
    public async initInfoSystem(infoSystem: InfoSystem): Promise<void> {
        let modalRef = this._modalService.create({
            nzTitle: "初始化信息系统",
            nzWidth: "460px",
            nzBodyStyle: {
                height: "260px"
            },
            nzContent: InitComponent,
            nzFooter: null,
            nzComponentParams: {
                systemNo: infoSystem.number,
                host: infoSystem.host,
                port: infoSystem.port,
                index: infoSystem.index
            }
        });

        modalRef.afterClose.subscribe((result) => {
            if (result == true) {
                this.loadInfoSystems();
            }
        });
    }
    //endregion

    //region 页码改变事件 —— async onPageIndexChange(pageIndex: number)
    /**
     * 页码改变事件
     * @param pageIndex - 页码
     * */
    public async onPageIndexChange(pageIndex: number): Promise<void> {
        this.pageIndex = pageIndex;
        await this.loadInfoSystems();
    }
    //endregion

    //region 页容量改变事件 —— async onPageSizeChange(pageSize: number)
    /**
     * 页容量改变事件
     * @param pageSize - 页容量
     * */
    public async onPageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadInfoSystems();
    }
    //endregion


    //Private

    //region 加载信息系统列表 —— async loadInfoSystems()
    /**
     * 加载信息系统列表
     * */
    private async loadInfoSystems(): Promise<void> {
        this.busy();

        let promise = this._infoSystemService.getInfoSystemsByPage(this.keywords, this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<InfoSystem> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.infoSystems = pageModel.datas;

        this.idle();
    }
    //endregion

    //endregion
}
