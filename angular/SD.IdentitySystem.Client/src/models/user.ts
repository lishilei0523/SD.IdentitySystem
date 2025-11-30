import {ModelBase} from "../base/model.base";

/*用户*/
export interface User extends ModelBase {

    /*私钥*/
    privateKey: string;

    /*状态*/
    enabled: boolean;
}
