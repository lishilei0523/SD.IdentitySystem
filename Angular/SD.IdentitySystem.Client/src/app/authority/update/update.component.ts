import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ComponentBase} from "sd-infrastructure";
import {AuthorityService} from "../../../services/authority.service";

/*权限修改组件*/
@Component({
    selector: 'app-authority-update',
    templateUrl: './update.component.html',
    styleUrls: ['./update.component.css']
})
export class UpdateComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*权限服务*/
    private readonly _authorityService: AuthorityService;

    /**
     * 创建权限修改组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, authorityService: AuthorityService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._authorityService = authorityService;
    }

    //endregion

    //region # 属性

    /*权限Id*/
    @Input()
    public authorityId: string = "";

    /*信息系统名称*/
    @Input()
    public infoSystemName: string = "";

    /*应用程序类型名称*/
    @Input()
    public applicationTypeName: string = "";

    /*权限名称*/
    @Input()
    public authorityName: string = "";

    /*权限路径*/
    @Input()
    public authorityPath: string = "";

    /*描述*/
    @Input()
    public description: string | null = null;

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
            authorityName: [null, [Validators.required]],
            authorityPath: [null, [Validators.required]],
            description: [null]
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

            let promise: Promise<void> = this._authorityService.updateAuthority(this.authorityId, this.authorityName, this.authorityPath, this.description);
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
