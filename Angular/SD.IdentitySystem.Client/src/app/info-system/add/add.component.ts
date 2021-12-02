import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ApplicationType, ApplicationTypeDescriptor, ComponentBase} from "sd-infrastructure";
import {InfoSystemService} from "../../../services/info-system.service";

/*信息系统创建组件*/
@Component({
    selector: 'app-info-system-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*信息系统服务*/
    private readonly _infoSystemService: InfoSystemService;

    /**
     * 创建信息系统创建组件构造器
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
    public infoSystemNo: string = "";

    /*信息系统名称*/
    public infoSystemName: string = "";

    /*系统管理员账号*/
    public adminLoginId: string = "";

    /*应用程序类型字典*/
    public applicationTypes: Set<{ key: ApplicationType, value: string }> = ApplicationTypeDescriptor.getEnumMembers();

    /*已选应用程序类型*/
    public selectedApplicationType: ApplicationType | null = null;

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
            infoSystemNo: [null, [Validators.required]],
            infoSystemName: [null, [Validators.required]],
            adminLoginId: [null, [Validators.required]],
            applicationType: [null, [Validators.required]],
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

            let promise: Promise<void> = this._infoSystemService.createInfoSystem(this.infoSystemNo, this.infoSystemName, this.adminLoginId, this.selectedApplicationType!);
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
