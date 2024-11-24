import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ComponentBase} from "../../../base/component.base";
import {MenuService} from "../../../services/menu.service";

/*菜单修改组件*/
@Component({
    selector: 'app-menu-update',
    templateUrl: './update.component.html',
    styleUrls: ['./update.component.css']
})
export class UpdateComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*菜单服务*/
    private readonly _menuService: MenuService;

    /**
     * 创建菜单修改组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, menuService: MenuService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._menuService = menuService;
    }

    //endregion

    //region # 属性

    /*菜单Id*/
    @Input()
    public menuId: string = "";

    /*信息系统名称*/
    @Input()
    public infoSystemName: string = "";

    /*应用程序类型名称*/
    @Input()
    public applicationTypeName: string = "";

    /*上级菜单名称*/
    @Input()
    public parentMenuName: string | null = null;

    /*菜单名称*/
    @Input()
    public menuName: string = "";

    /*链接地址*/
    @Input()
    public url: string | null = null;

    /*路径*/
    @Input()
    public path: string | null = null;

    /*图标*/
    @Input()
    public icon: string | null = null;

    /*排序*/
    @Input()
    public sort: number | null = null;

    /*表单*/
    public formGroup!: FormGroup;

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
            infoSystemName: [null],
            applicationTypeName: [null],
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

            let promise: Promise<void> = this._menuService.updateMenu(this.menuId, this.menuName, this.sort!, this.url, this.path, this.icon);
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
