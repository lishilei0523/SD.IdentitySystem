import {Component, OnInit} from '@angular/core';
import {BaseComponent} from "../../../extentions/base.component";
import {PageModel} from "../../../values/structs/page-model";
import {InfoSystem} from "../info-system.model";
import {NzModalService} from "ng-zorro-antd/modal";
import {InfoSystemService} from "../info-system.service";
import {AddComponent} from "../add/add.component";

/*信息系统首页组件*/
@Component({
    selector: 'app-info-system-index',
    templateUrl: './index.component.html',
    styleUrls: ['./index.component.css']
})
export class IndexComponent extends BaseComponent implements OnInit {

    /*模态框服务*/
    private readonly modalService: NzModalService;

    /*信息系统服务*/
    private readonly infoSystemService: InfoSystemService;

    /*关键字*/
    public keywords: string;

    /*页码*/
    public pageIndex: number;

    /*页容量*/
    public pageSize: number;

    /*总记录数*/
    public rowCount: number;

    /*总页数*/
    public pageCount: number;

    /*信息系统列表*/
    public infoSystems: Array<InfoSystem>;

    /*是否全选*/
    public checkedAll = false;

    /*选中项列表*/
    public checkedIds: Set<string>;

    /**
     * 创建信息系统首页组件构造器
     * */
    public constructor(modalService: NzModalService, infoSystemService: InfoSystemService) {
        super();
        this.modalService = modalService;
        this.infoSystemService = infoSystemService;
        this.keywords = "";
        this.pageIndex = 1;
        this.pageSize = 20;
        this.rowCount = 0;
        this.pageCount = 0
        this.infoSystems = new Array<InfoSystem>();
        this.checkedIds = new Set<string>();
    }

    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        await this.loadInfoSystems();
    }

    /**
     * 搜索
     * */
    public async search(): Promise<void> {
        this.pageIndex = 1;
        await this.loadInfoSystems();
    }

    /**
     * 重置搜索
     * */
    public resetSearch(): void {
        console.log(this.checkedIds);
        this.keywords = "";
    }

    /**
     * 创建信息系统
     * */
    public async createInfoSystem(): Promise<void> {
        let modalRef = this.modalService.create({
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

    /**
     * 页码改变事件
     * */
    public async pageIndexChange(pageIndex: number): Promise<void> {
        this.pageIndex = pageIndex;
        await this.loadInfoSystems();
    }

    /**
     * 页容量改变事件
     * */
    public async pageSizeChange(pageSize: number): Promise<void> {
        this.pageSize = pageSize;
        await this.loadInfoSystems();
    }

    /**
     * 勾选全部
     * @param checked - 是否勾选
     * */
    public checkAll(checked: boolean): void {
        this.infoSystems.forEach(infoSystem => this.refreshChecked(infoSystem.id, checked));
    }

    /**
     * 勾选
     * @param id - 标识Id
     * @param checked - 是否勾选
     * */
    public checkItem(id: string, checked: boolean): void {
        this.refreshChecked(id, checked);
    }

    /**
     * 加载信息系统列表
     * */
    private async loadInfoSystems(): Promise<void> {
        this.busy();

        let promise = this.infoSystemService.getInfoSystemsByPage(this.keywords, this.pageIndex, this.pageSize);
        promise.catch(_ => this.idle());

        let pageModel: PageModel<InfoSystem> = await promise;
        this.pageIndex = pageModel.pageIndex;
        this.pageSize = pageModel.pageSize;
        this.rowCount = pageModel.rowCount;
        this.pageCount = pageModel.pageCount;
        this.infoSystems = pageModel.datas;

        this.idle();
    }

    /**
     * 刷新勾选
     * @param id - 标识Id
     * @param checked - 是否勾选
     * */
    private refreshChecked(id: string, checked: boolean): void {
        if (checked) {
            this.checkedIds.add(id);
        } else {
            this.checkedIds.delete(id);
        }
        this.checkedAll = this.infoSystems.every(infoSystem => this.checkedIds.has(infoSystem.id));
    }
}
