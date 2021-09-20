import {ApplicationType, ModelBase} from "sd-infrastructure";

/*信息系统*/
export interface InfoSystem extends ModelBase {

    /*应用程序类型*/
    applicationType: ApplicationType;

    /*管理员账号*/
    adminLoginId: string;

    /*主机名*/
    host: string | null;

    /*端口*/
    port: number | null;

    /*首页*/
    index: string | null;
}
