import {Component, Input} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {BaseComponent} from "../../../extentions/base.component";
import {HomeService} from "../home.service";

/*用户修改密码组件*/
@Component({
    selector: 'app-update-password',
    templateUrl: './update-password.component.html',
    styleUrls: ['./update-password.component.css']
})
export class UpdatePasswordComponent extends BaseComponent {

    /*对话框引用*/
    private modalRef: NzModalRef;

    /*表单建造者*/
    private readonly formBuilder: FormBuilder;

    /*用户服务*/
    private readonly homeService: HomeService;

    /*用户名*/
    @Input()
    public loginId: string;

    /*旧密码*/
    public oldPassword: string;

    /*新密码*/
    public newPassword: string;

    /*确认密码*/
    public confirmedPassword: string;

    /*表单表单*/
    public formGroup!: FormGroup;

    /**
     * 创建用户修改密码组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, homeService: HomeService) {
        //基类构造器
        super();

        //依赖注入部分
        this.modalRef = modalRef;
        this.formBuilder = formBuilder;
        this.homeService = homeService;

        //默认值部分
        this.loginId = "";
        this.oldPassword = "";
        this.newPassword = "";
        this.confirmedPassword = "";
        this.formGroup = this.formBuilder.group({
            oldPassword: [null, [Validators.required]],
            newPassword: [null, [Validators.required]],
            confirmedPassword: [null, [Validators.required]],
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

            let promise: Promise<void> = this.homeService.updatePassword(this.loginId, this.oldPassword, this.newPassword);
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
