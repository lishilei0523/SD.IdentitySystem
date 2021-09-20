import {ModelBase} from "sd-infrastructure";

/*用户*/
export interface User extends ModelBase {

    /*私钥*/
    privateKey: string;

    /*状态*/
    enabled: boolean;
}
