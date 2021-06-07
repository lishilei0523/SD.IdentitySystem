import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {Constants} from "../../../values/constants/constants";
import {HomeService} from "../home.service";
import {LoginInfo} from "../../../values/structs/loginInfo";
import {MessageService} from 'primeng/api';
import {BaseComponent} from "../../../extentions/base.component";

/*登录组件*/
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent {

    /*路由器*/
    private router: Router;

    /*消息服务*/
    private messageService: MessageService;

    /*首页服务*/
    private homeService: HomeService;

    /*用户名*/
    public loginId: string;

    /*密码*/
    public password: string;

    /**
     * 创建登录组件构造器
     * */
    public constructor(router: Router, messageService: MessageService, homeService: HomeService) {
        super();

        this.router = router;
        this.messageService = messageService;
        this.homeService = homeService;
        this.loginId = "";
        this.password = "";
    }

    /**
     * 登录
     * */
    @Busy
    public async login(): Promise<void> {
        //super.busy();

        let promise: Promise<LoginInfo> = this.homeService.login(this.loginId, this.password);
        promise.catch(_ => super.idle());

        let loginInfo = await promise;
        Constants.loginInfo = loginInfo;

        await this.router.navigate(["/Home"]);
        //super.idle()
    }
}


export function Busy(target: any, methodName: any) {
    let busy = target["busy"];
    let idle = target["busy"];

    let methodInfo = target[methodName];
    target[methodName] = function (...args: any[]) {

        busy.apply(this);
        let result = methodInfo.apply(this, args);
        idle.apply(this);

        return result
    }

    return target[methodName];
}
