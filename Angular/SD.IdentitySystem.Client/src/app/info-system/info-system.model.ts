import {ApplicationType} from "../../values/enums/application-type";

/*信息系统*/
export interface InfoSystem {

    /*编号*/
    number: string;

    /*名称*/
    name: string;

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

    /*创建时间*/
    addedTime: string;
}
