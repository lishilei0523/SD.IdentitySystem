import {Component, inject, Input, OnInit} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {NzModalRef, NZ_MODAL_DATA} from "ng-zorro-antd/modal";
import {ComponentBase} from "../../../base/component.base";
import {InfoSystemService} from "../../../services/info-system.service";

/*信息系统修改组件*/
@Component({
    selector: 'app-info-system-update',
    templateUrl: './update.component.html',
    styleUrls: ['./update.component.css']
})
export class UpdateComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*对话框数据*/
    private readonly _modalData = inject(NZ_MODAL_DATA);

    /*表单建造者*/
    private readonly _formBuilder: UntypedFormBuilder;

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /**
     * 创建信息系统修改组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: UntypedFormBuilder, infoSystemService: InfoSystemService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._infoSystemService = infoSystemService;
    }

    //endregion

    //region # 属性

    /*信息系统编号*/
    @Input()
    public infoSystemNo: string = "";

    /*信息系统名称*/
    @Input()
    public infoSystemName: string = "";

    /*表单*/
    public formGroup!: UntypedFormGroup;

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— ngOnInit()
    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        //初始化表单
        this.infoSystemNo = this._modalData.infoSystemNo;
        this.infoSystemName = this._modalData.infoSystemName;
        this.formGroup = this._formBuilder.group({
            infoSystemNo: [null, [Validators.required]],
            infoSystemName: [null, [Validators.required]],
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

            let promise: Promise<void> = this._infoSystemService.updateInfoSystem(this.infoSystemNo, this.infoSystemName);
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
