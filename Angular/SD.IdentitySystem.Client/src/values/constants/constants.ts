import {AppConfig} from "../structs/app-config";

/*常量*/
export class Constants {

    /*当前登录用户键*/
    public static readonly keyOfCurrentUser: string = "CurrentUser";

    /*当前公钥键*/
    public static readonly keyOfPublicKey: string = "CurrentPublicKey";

    /*应用程序配置*/
    public static appConfig: AppConfig;
}
