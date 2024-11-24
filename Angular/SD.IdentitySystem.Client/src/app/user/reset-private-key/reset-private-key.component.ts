import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ComponentBase} from "../../../base/component.base";
import {UserService} from "../../../services/user.service";

/*用户重置私钥组件*/
@Component({
    selector: 'app-user-reset-private-key',
    templateUrl: './reset-private-key.component.html',
    styleUrls: ['./reset-private-key.component.css']
})
export class ResetPrivateKeyComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*用户服务*/
    private readonly _userService: UserService;

    /**
     * 创建用户重置私钥组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, userService: UserService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._userService = userService;
    }

    //endregion

    //region # 属性

    /*用户名*/
    @Input()
    public loginId: string = "";

    /*私钥*/
    @Input()
    public privateKey: string = "";

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
            privateKey: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(36)]]
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

            let promise: Promise<void> = this._userService.setPrivateKey(this.loginId, this.privateKey);
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
