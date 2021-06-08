import {LoginInfo} from "../structs/login-info";
import {AppConfig} from "../structs/app-config";
import {HttpHeaders} from "@angular/common/http";
import {LoginMenuInfo} from "../structs/login-menu-info";
import {ApplicationType} from "../enums/application-type";

/*常量*/
export class Constants {

    /*当前公钥键*/
    public static readonly keyOfPublicKey: string = "CurrentPublicKey";

    /*应用程序配置*/
    public static appConfig: AppConfig;

    /*登录信息*/
    public static loginInfo: LoginInfo | null = null;

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

    /*HTTP GET请求选项*/
    public static get httpGetOptions() {
        let httpOptions = {};
        let publicKey: string = Constants.loginInfo == null ? "" : Constants.loginInfo.publicKey;
        if (publicKey) {
            httpOptions = {
                headers: new HttpHeaders({
                    "CurrentPublicKey": publicKey
                })
            };
        }

        return httpOptions;
    };

    /*HTTP POST请求选项*/
    public static get httpPostOptions() {
        let httpOptions;
        let publicKey: string = Constants.loginInfo == null ? "" : Constants.loginInfo.publicKey;
        if (publicKey) {
            httpOptions = {
                headers: new HttpHeaders({
                    "Content-Type": "application/json",
                    "CurrentPublicKey": publicKey
                })
            };
        } else {
            httpOptions = {
                headers: new HttpHeaders({
                    "Content-Type": "application/json"
                })
            };
        }

        return httpOptions;
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
