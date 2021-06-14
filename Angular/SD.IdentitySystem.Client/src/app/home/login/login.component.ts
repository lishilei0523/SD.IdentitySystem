import {Component} from '@angular/core';
import {environment} from "../../../environments/environment";
import {Router} from "@angular/router";
import {Membership} from "../../../values/constants/membership";
import {LoginInfo} from "../../../values/structs/login-info";
import {BaseComponent} from "../../../extentions/base.component";
import {UserService} from "../../user/user.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

/*登录组件*/
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

    /*路由器*/
    private readonly router: Router;

    /*表单建造者*/
    private readonly formBuilder: FormBuilder;

    /*用户服务*/
    private readonly userService: UserService;

    /*用户名*/
    public loginId: string;

    /*密码*/
    public password: string;

    /*表单组*/
    public formGroup: FormGroup;

    /**
     * 创建登录组件构造器
     * */
    public constructor(router: Router, formBuilder: FormBuilder, userService: UserService) {
        //基类构造器
        super();

        //依赖注入部分
        this.router = router;
        this.formBuilder = formBuilder;
        this.userService = userService;

        //默认值部分
        this.loginId = "";
        this.password = "";
        this.formGroup = this.formBuilder.group({
            loginId: [null, [Validators.required]],
            password: [null, [Validators.required]]
        });

        //自动登录
        if (!environment.production) {
            this.autoLogin();
        }
    }

    /**
     * 登录
     * */
    public async login(): Promise<void> {
        for (let index in this.formGroup.controls) {
            this.formGroup.controls[index].markAsDirty();
            this.formGroup.controls[index].updateValueAndValidity();
        }

        if (this.formGroup.valid) {
            this.busy();

            let promise: Promise<LoginInfo> = this.userService.login(this.loginId, this.password);
            promise.catch(_ => this.idle());
            Membership.loginInfo = await promise;
            await this.router.navigate(["/Home"]);

            this.idle();
        }
    }

    /**
     * 自动登录
     * */
    private async autoLogin(): Promise<void> {
        this.loginId = "admin";
        this.password = "888888";
        await this.login();
    }
}
