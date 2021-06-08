import {Component, OnInit} from '@angular/core';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {MessageService} from "primeng/api";
import {BaseComponent} from "../../../extentions/base.component";
import {UserService} from "../user.service";

/*用户修改密码组件*/
@Component({
    selector: 'app-update-password',
    templateUrl: './update-password.component.html',
    styleUrls: ['./update-password.component.css']
})
export class UpdatePasswordComponent extends BaseComponent implements OnInit {

    /*对话框引用*/
    public readonly dialogRef: DynamicDialogRef;

    /*对话框配置*/
    public readonly dialogConfig: DynamicDialogConfig;

    /*消息服务*/
    private readonly messageService: MessageService;

    /*用户服务*/
    private readonly userService: UserService;

    /*用户名*/
    public loginId: string;

    /*旧密码*/
    public oldPassword: string;

    /*新密码*/
    public newPassword: string;

    /*确认密码*/
    public confirmedPassword: string;

    /**
     * 创建用户修改密码组件构造器
     * */
    public constructor(dialogRef: DynamicDialogRef, dialogConfig: DynamicDialogConfig, messageService: MessageService, userService: UserService) {
        super();
        this.dialogRef = dialogRef;
        this.dialogConfig = dialogConfig;
        this.messageService = messageService;
        this.userService = userService;
        this.loginId = "";
        this.oldPassword = "";
        this.newPassword = "";
        this.confirmedPassword = "";
    }

    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        this.loginId = this.dialogConfig.data.loginId;
    }

    /**
     * 提交
     * */
    public async submit(): Promise<void> {
        //region # 验证

        if (!this.oldPassword) {
            this.messageService.add({severity: "error", summary: "旧密码不可为空！"});
            return;
        }
        if (!this.newPassword) {
            this.messageService.add({severity: "error", summary: "新密码不可为空！"});
            return;
        }
        if (!this.confirmedPassword) {
            this.messageService.add({severity: "error", summary: "确认密码不可为空！"});
            return;
        }
        if (this.newPassword != this.confirmedPassword) {
            this.messageService.add({severity: "error", summary: "两次密码输入不一致！"});
            return;
        }

        //endregion

        this.busy();

        let promise: Promise<void> = this.userService.updatePassword(this.loginId, this.oldPassword, this.newPassword);
        promise.catch(_ => this.idle());
        await promise;

        this.idle();
        this.dialogRef.close(true);
    }

    /**
     * 取消
     * */
    public cancel(): void {
        this.dialogRef.close(false);
    }
}
