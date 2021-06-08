import {Component} from '@angular/core';
import {environment} from "../../../environments/environment";
import {Router} from "@angular/router";
import {MessageService} from 'primeng/api';
import {Constants} from "../../../values/constants/constants";
import {LoginInfo} from "../../../values/structs/login-info";
import {BaseComponent} from "../../../extentions/base.component";
import {UserService} from "../../user/user.service";

/*登录组件*/
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

    /*路由器*/
    private readonly router: Router;

    /*消息服务*/
    private readonly messageService: MessageService;

    /*用户服务*/
    private readonly userService: UserService;

    /*用户名*/
    public loginId: string;

    /*密码*/
    public password: string;

    /**
     * 创建登录组件构造器
     * */
    public constructor(router: Router, messageService: MessageService, userService: UserService) {
        //基类构造器
        super();

        //依赖注入部分
        this.router = router;
        this.messageService = messageService;
        this.userService = userService;

        //默认值部分
        this.loginId = "";
        this.password = "";

        //自动登录
        this.autoLogin();
    }

    /**
     * 登录
     * */
    public async login(): Promise<void> {
        this.busy();

        let promise: Promise<LoginInfo> = this.userService.login(this.loginId, this.password);
        promise.catch(_ => this.idle());
        Constants.loginInfo = await promise;
        await this.router.navigate(["/Home"]);

        this.idle();
        console.log(Constants.loginAuthorityPaths);
    }

    /**
     * 自动登录
     * */
    private async autoLogin(): Promise<void> {
        if (!environment.production) {
            this.loginId = "admin";
            this.password = "888888";
            await this.login();
        }
    }
}
