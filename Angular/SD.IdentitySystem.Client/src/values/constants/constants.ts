import {LoginInfo} from "../structs/loginInfo";
import {AppConfig} from "../structs/app-config";
import {HttpHeaders} from "@angular/common/http";
import {LoginMenuInfo} from "../structs/loginMenuInfo";
import {ApplicationType} from "../enums/application-type";

/*常量*/
export class Constants {

    /*当前公钥键*/
    public static readonly keyOfPublicKey: string = "CurrentPublicKey";

    /*应用程序配置*/
    public static appConfig: AppConfig;

    /*登录信息*/
    public static loginInfo: LoginInfo | null = null;

    /*用户菜单列表*/
    public static get userMenus(): Array<LoginMenuInfo> {
        if (Constants.loginInfo) {
            let userMenus = Constants.loginInfo.loginMenuInfos.filter(x => x.systemNo == "00" && x.applicationType == ApplicationType.IOS);
            Constants.filterLoginMenu(userMenus, 1);

            return userMenus;
        }
        return new Array<LoginMenuInfo>();
    }

    /*用户权限列表*/
    public static get userAuthorityPaths(): Array<string> {
        if (Constants.loginInfo) {
            return Constants.loginInfo.loginAuthorityInfos.map(x => x.path);
        }
        return new Array<string>();
    }

    /*HTTP POST请求选项*/
    public static readonly httpPostOptions = {
        headers: new HttpHeaders({
            "Content-Type": "application/json"
        })
    };

    /*HTTP POST认证请求选项*/
    public static readonly httpPostAuthOptions = {
        headers: new HttpHeaders({
            "Content-Type": "application/json",
            "CurrentPublicKey": Constants.loginInfo == null ? "" : Constants.loginInfo.publicKey
        })
    };

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
