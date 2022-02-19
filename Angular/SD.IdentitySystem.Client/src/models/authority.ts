import {ApplicationType, ModelBase} from "sd-infrastructure";
import {InfoSystem} from "./info-system";

/*权限*/
export interface Authority extends ModelBase {

    /*信息系统编号*/
    infoSystemNo: string;

    /*应用程序类型*/
    applicationType: ApplicationType;

    /*权限路径*/
    authorityPath: string;

    /*描述*/
    description: string | null;

    /*导航属性 - 信息系统*/
    infoSystemInfo: InfoSystem | null;
}
