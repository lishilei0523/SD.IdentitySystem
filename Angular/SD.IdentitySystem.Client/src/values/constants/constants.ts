import {LoginInfo} from "../structs/loginInfo";

/*常量*/
export class Constants {

    /*当前公钥键*/
    public static readonly keyOfPublicKey: string = "CurrentPublicKey";

    /*登录信息*/
    public static loginInfo: LoginInfo;

    /*允许权限路径列表*/
    public static allowedAuthorityPaths: Array<string> = new Array<string>();
}
