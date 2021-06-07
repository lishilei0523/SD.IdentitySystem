import {LoginInfo} from "../structs/loginInfo";
import {AppConfig} from "../structs/app-config";
import {HttpHeaders} from "@angular/common/http";

/*常量*/
export class Constants {

    /*当前公钥键*/
    public static readonly keyOfPublicKey: string = "CurrentPublicKey";

    /*应用程序配置*/
    public static appConfig: AppConfig;

    /*登录信息*/
    public static loginInfo: LoginInfo | null = null;

    /*允许权限路径列表*/
    public static allowedAuthorityPaths: Array<string> = new Array<string>();

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
}
