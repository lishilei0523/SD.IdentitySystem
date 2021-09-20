import {Component, OnInit} from '@angular/core';
import {environment} from "../../../environments/environment";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Membership, LoginInfo, ComponentBase} from "sd-infrastructure";
import {HomeService} from "../../../services/home.service";

/*登录组件*/
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*路由器*/
    private readonly router: Router;

    /*表单建造者*/
    private readonly formBuilder: FormBuilder;

    /*首页服务*/
    private readonly homeService: HomeService;

    /**
     * 创建登录组件构造器
     * */
    public constructor(router: Router, formBuilder: FormBuilder, homeService: HomeService) {
        super();
        this.router = router;
        this.formBuilder = formBuilder;
        this.homeService = homeService;
    }

    //endregion

    //region # 属性

    /*用户名*/
    public loginId: string = "";

    /*密码*/
    public password: string = "";

    /*表单组*/
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
            loginId: [null, [Validators.required]],
            password: [null, [Validators.required]]
        });

        //自动登录
        if (!environment.production) {
            this.autoLogin();
        }
    }
    //endregion


    //Actions

    //region 登录 —— async login()
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

            let promise: Promise<LoginInfo> = this.homeService.login(this.loginId, this.password);
            promise.catch(_ => this.idle());
            Membership.loginInfo = await promise;
            await this.router.navigate(["/Home"]);

            this.idle();
        }
    }
    //endregion


    //Private

    //region 自动登录 —— async autoLogin()
    /**
     * 自动登录
     * */
    private async autoLogin(): Promise<void> {
        this.loginId = "admin";
        this.password = "888888";
        await this.login();
    }
    //endregion

    //endregion
}
