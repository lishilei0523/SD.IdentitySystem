import {ModelBase} from "../base/model.base";

/*登录记录*/
export interface LoginRecord extends ModelBase {

    /*用户名*/
    loginId: string;

    /*真实姓名*/
    realName: string;

    /*IP地址*/
    ip: string;

    /*客户端Id*/
    clientId: string;
}
