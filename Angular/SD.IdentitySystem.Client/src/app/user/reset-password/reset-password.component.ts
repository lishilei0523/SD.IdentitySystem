import {Component, inject, Input, OnInit} from '@angular/core';
import {NzModalRef, NZ_MODAL_DATA} from "ng-zorro-antd/modal";
import {NzMessageService} from "ng-zorro-antd/message";
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {ComponentBase} from "../../../base/component.base";
import {UserService} from "../../../services/user.service";

/*用户重置密码组件*/
@Component({
    selector: 'app-user-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.css'],
    standalone: false
})
export class ResetPasswordComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*对话框数据*/
    private readonly _modalData = inject(NZ_MODAL_DATA);

    /*消息服务*/
    private readonly _messageService: NzMessageService;

    /*表单建造者*/
    private readonly _formBuilder: UntypedFormBuilder;

    /*用户服务*/
    private readonly _userService: UserService;

    /**
     * 创建用户重置密码组件构造器
     * */
    public constructor(modalRef: NzModalRef, messageService: NzMessageService, formBuilder: UntypedFormBuilder, userService: UserService) {
        super();
        this._modalRef = modalRef;
        this._messageService = messageService;
        this._formBuilder = formBuilder;
        this._userService = userService;
    }

    //endregion

    //region # 属性

    /*用户名*/
    @Input()
    public loginId: string = "";

    /*新密码*/
    public newPassword: string = "";

    /*确认密码*/
    public confirmedPassword = "";

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
        this.loginId = this._modalData.loginId;
        this.formGroup = this._formBuilder.group({
            newPassword: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
            confirmedPassword: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
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

        if (this.newPassword != this.confirmedPassword) {
            this._messageService.error("两次密码输入不一致！");
            return;
        }
        if (this.formGroup.valid) {
            this.busy();

            let promise: Promise<void> = this._userService.resetPassword(this.loginId, this.newPassword);
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
