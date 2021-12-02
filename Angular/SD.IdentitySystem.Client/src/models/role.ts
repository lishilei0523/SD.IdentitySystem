import {ModelBase} from "sd-infrastructure";
import {InfoSystem} from "./info-system";

/*角色*/
export interface Role extends ModelBase {

    /*信息系统编号*/
    infoSystemNo: string;

    /*描述*/
    description: string | null;

    /*导航属性 - 信息系统*/
    infoSystemInfo: InfoSystem | null;
}
