import {Component, OnInit} from '@angular/core';
import {environment} from "../../../environments/environment";
import {Router} from "@angular/router";
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {ComponentBase} from "../../../base/component.base";
import {Membership} from "../../../values/constants/membership";
import {LoginInfo} from "../../../values/structs/login-info";
import {LoginMenuInfo} from "../../../values/structs/login-menu-info";
import {ApplicationType} from "../../../values/enums/application-type";
import {HomeService} from "../../../services/home.service";

/*登录组件*/
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    standalone: false
})
export class LoginComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*路由器*/
    private readonly _router: Router;

    /*表单建造者*/
    private readonly _formBuilder: UntypedFormBuilder;

    /*首页服务*/
    private readonly _homeService: HomeService;

    /**
     * 创建登录组件构造器
     * */
    public constructor(router: Router, formBuilder: UntypedFormBuilder, homeService: HomeService) {
        super();
        this._router = router;
        this._formBuilder = formBuilder;
        this._homeService = homeService;
    }

    //endregion

    //region # 属性

    /*用户名*/
    public loginId: string = "";

    /*密码*/
    public password: string = "";

    /*表单组*/
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
        this.formGroup = this._formBuilder.group({
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

            let promise: Promise<LoginInfo> = this._homeService.login(this.loginId, this.password);
            promise.catch(_ => this.idle());

            let loginInfo: LoginInfo = await promise;
            Membership.loginInfo = loginInfo;
            this.initMenus(loginInfo);
            this.initAuthorityPaths(loginInfo);

            await this._router.navigate(["/Home"]);

            this.idle();
        }
    }
    //endregion


    //Private

    //region 初始化用户菜单列表 —— initMenus(loginInfo: LoginInfo)
    /**
     * 初始化用户菜单列表
     * @param loginInfo - 登录信息
     * */
    private initMenus(loginInfo: LoginInfo): void {
        let loginMenus = loginInfo.loginMenuInfos.filter(x => x.systemNo == "00" && x.applicationType == ApplicationType.IOS);
        this.filterLoginMenu(loginMenus, 1);

        Membership.loginMenus = loginMenus;
    }
    //endregion

    //region 初始化用户权限路径列表 —— initAuthorityPaths(loginInfo: LoginInfo)
    /**
     * 初始化用户权限路径列表
     * @param loginInfo - 登录信息
     * */
    private initAuthorityPaths(loginInfo: LoginInfo): void {
        Membership.loginAuthorityPaths = loginInfo.loginAuthorityInfos.filter(x => x.systemNo == "00" && x.applicationType == ApplicationType.IOS).map(x => x.path);
    }
    //endregion

    //region 过滤用户菜单 —— filterLoginMenu(loginMenus: LoginMenuInfo[], level...
    /**
     * 过滤用户菜单
     * @param loginMenus - 登录菜单列表
     * @param level - 节点层级
     * */
    private filterLoginMenu(loginMenus: LoginMenuInfo[], level: number): void {
        for (let loginMenu of loginMenus) {
            loginMenu.level = level;
            if (loginMenu.subMenuInfos.length > 0) {
                loginMenu.isLeaf = false;
                this.filterLoginMenu(loginMenu.subMenuInfos, level + 1);
            } else {
                loginMenu.isLeaf = true;
            }
        }
    }
    //endregion

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
