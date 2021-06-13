import {LoginInfo} from "../structs/login-info";
import {AppConfig} from "../structs/app-config";
import {LoginMenuInfo} from "../structs/login-menu-info";
import {ApplicationType} from "../enums/application-type";

/*常量*/
export class Constants {

    /*当前登录用户键*/
    private static readonly keyOfCurrentUser: string = "CurrentUser";

    /*当前公钥键*/
    public static readonly keyOfPublicKey: string = "CurrentPublicKey";

    /*应用程序配置*/
    public static appConfig: AppConfig;

    /**
     * 获取登录信息
     * @return 登录信息
     * */
    public static get loginInfo(): LoginInfo | null {
        let json = localStorage.getItem(Constants.keyOfCurrentUser);
        if (json) {
            return JSON.parse(json);
        } else {
            return null;
        }
    }

    /**
     * 设置登录信息
     * @param value - 登录信息
     * */
    public static set loginInfo(value: LoginInfo | null) {
        if (value) {
            localStorage.setItem(Constants.keyOfCurrentUser, JSON.stringify(value));
        } else {
            localStorage.removeItem(Constants.keyOfCurrentUser);
        }
    }


    /*登录菜单列表*/
    public static get loginMenus(): Array<LoginMenuInfo> {
        if (Constants.loginInfo) {
            let loginMenus = Constants.loginInfo.loginMenuInfos.filter(x => x.systemNo == "00" && x.applicationType == ApplicationType.IOS);
            Constants.filterLoginMenu(loginMenus, 1);

            return loginMenus;
        }
        return new Array<LoginMenuInfo>();
    }

    /*登录权限列表*/
    public static get loginAuthorityPaths(): Array<string> {
        if (Constants.loginInfo) {
            return Constants.loginInfo.loginAuthorityInfos.filter(x => x.systemNo == "00" && x.applicationType == ApplicationType.IOS).map(x => x.path);
        }
        return new Array<string>();
    }

    /**
     * 过滤用户菜单
     * */
    private static filterLoginMenu(loginMenus: LoginMenuInfo[], level: number): void {
        for (let loginMenu of loginMenus) {
            loginMenu.level = level;
            if (loginMenu.subMenuInfos.length > 0) {
                loginMenu.isLeaf = false;
                Constants.filterLoginMenu(loginMenu.subMenuInfos, level + 1);
            } else {
                loginMenu.isLeaf = true;
            }
        }
    }
}
