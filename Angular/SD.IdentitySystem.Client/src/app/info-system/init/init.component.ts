import {Component, Input, OnInit} from '@angular/core';
import {BaseComponent} from "../../../extentions/base.component";
import {NzModalRef} from "ng-zorro-antd/modal";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {InfoSystemService} from "../info-system.service";

/*信息系统初始化组件*/
@Component({
    selector: 'app-info-system-init',
    templateUrl: './init.component.html',
    styleUrls: ['./init.component.css']
})
export class InitComponent extends BaseComponent {

    /*对话框引用*/
    private modalRef: NzModalRef;

    /*表单建造者*/
    private readonly formBuilder: FormBuilder;

    /*信息系统服务*/
    private readonly infoSystemService: InfoSystemService;

    /*信息系统编号*/
    @Input()
    public systemNo: string;

    /*主机名*/
    @Input()
    public host: string | null;

    /*端口*/
    @Input()
    public port: number | null;

    /*首页*/
    @Input()
    public index: string| null;

    /*表单表单*/
    public formGroup!: FormGroup;

    /**
     * 创建信息系统初始化组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, infoSystemService: InfoSystemService) {
        //基类构造器
        super();

        //依赖注入部分
        this.modalRef = modalRef;
        this.formBuilder = formBuilder;
        this.infoSystemService = infoSystemService;

        //默认值部分
        this.systemNo = "";
        this.host = "";
        this.port = null;
        this.index = "";
        this.formGroup = this.formBuilder.group({
            host: [null, [Validators.required]],
            port: [null, [Validators.required]],
            index: [null, [Validators.required]],
        });
    }

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

            let promise: Promise<void> = this.infoSystemService.initInfoSystem(this.systemNo, this.host!, this.port!, this.index!);
            promise.catch(_ => {
                this.idle();
            });
            await promise;

            this.idle();
            this.modalRef.close(true);
        }
    }

    /**
     * 取消
     * */
    public cancel(): void {
        this.modalRef.close(false);
    }
}
