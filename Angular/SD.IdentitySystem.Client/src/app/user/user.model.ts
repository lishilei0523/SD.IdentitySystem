/*用户*/
export interface User {

    /*用户名*/
    number: string;

    /*真实姓名*/
    name: string;

    /*私钥*/
    privateKey: string;

    /*状态*/
    enabled: boolean;

    /*创建时间*/
    addedTime: string;
}
