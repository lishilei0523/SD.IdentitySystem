import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ComponentBase} from "sd-infrastructure";
import {InfoSystemService} from "../../../services/info-system.service";

/*信息系统初始化组件*/
@Component({
    selector: 'app-info-system-init',
    templateUrl: './init.component.html',
    styleUrls: ['./init.component.css']
})
export class InitComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /**
     * 创建信息系统初始化组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, infoSystemService: InfoSystemService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._infoSystemService = infoSystemService;
    }

    //endregion

    //region # 属性

    /*信息系统编号*/
    @Input()
    public systemNo: string = "";

    /*主机名*/
    @Input()
    public host: string | null = "";

    /*端口*/
    @Input()
    public port: number | null = null;

    /*首页*/
    @Input()
    public index: string | null = "";

    /*表单表单*/
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
            host: [null, [Validators.required]],
            port: [null, [Validators.required]],
            index: [null, [Validators.required]],
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

            let promise: Promise<void> = this._infoSystemService.initInfoSystem(this.systemNo, this.host!, this.port!, this.index!);
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
