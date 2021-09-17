import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {BaseComponent} from "../../../extentions/base.component";
import {InfoSystemService} from "../info-system.service";

/*信息系统修改组件*/
@Component({
    selector: 'app-info-system-update',
    templateUrl: './update.component.html',
    styleUrls: ['./update.component.css']
})
export class UpdateComponent extends BaseComponent implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly modalRef: NzModalRef;

    /*表单建造者*/
    private readonly formBuilder: FormBuilder;

    /*信息系统服务*/
    private readonly infoSystemService: InfoSystemService;

    /**
     * 创建信息系统修改组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, infoSystemService: InfoSystemService) {
        super();
        this.modalRef = modalRef;
        this.formBuilder = formBuilder;
        this.infoSystemService = infoSystemService;
    }

    //endregion

    //region # 属性

    /*信息系统Id*/
    @Input()
    public systemId: string = "";

    /*信息系统编号*/
    @Input()
    public systemNo: string = "";

    /*信息系统名称*/
    @Input()
    public systemName: string = "";

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
        this.formGroup = this.formBuilder.group({
            systemNo: [null, [Validators.required]],
            systemName: [null, [Validators.required]],
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

            let promise: Promise<void> = this.infoSystemService.updateInfoSystem(this.systemId, this.systemNo, this.systemName);
            promise.catch(_ => {
                this.idle();
            });
            await promise;

            this.idle();
            this.modalRef.close(true);
        }
    }
    //endregion

    //region 取消 —— cancel()
    /**
     * 取消
     * */
    public cancel(): void {
        this.modalRef.close(false);
    }
    //endregion

    //endregion
}
