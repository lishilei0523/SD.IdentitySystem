import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ComponentBase} from "sd-infrastructure";
import {HomeService} from "../../../services/home.service";

/*用户修改密码组件*/
@Component({
    selector: 'app-update-password',
    templateUrl: './update-password.component.html',
    styleUrls: ['./update-password.component.css']
})
export class UpdatePasswordComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*用户服务*/
    private readonly _homeService: HomeService;

    /**
     * 创建用户修改密码组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, homeService: HomeService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._homeService = homeService;
    }

    //endregion

    //region # 属性

    /*用户名*/
    @Input()
    public loginId: string = "";

    /*旧密码*/
    public oldPassword: string = "";

    /*新密码*/
    public newPassword: string = "";

    /*确认密码*/
    public confirmedPassword: string = "";

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
        //初始表单
        this.formGroup = this._formBuilder.group({
            oldPassword: [null, [Validators.required]],
            newPassword: [null, [Validators.required]],
            confirmedPassword: [null, [Validators.required]],
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

            let promise: Promise<void> = this._homeService.updatePassword(this.loginId, this.oldPassword, this.newPassword);
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
