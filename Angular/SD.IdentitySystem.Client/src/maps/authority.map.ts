import {NzNode} from "sd-infrastructure";
import {Authority} from "../models/authority";

/*权限映射工具类*/
export class AuthorityMap {

    //region # 权限NZ树节点映射 —— static toNzNode(authority: Authority)
    /**
     * 权限NZ树节点映射
     * */
    public static toNzNode(authority: Authority): NzNode {
        return new NzNode(authority.id, authority.name, true);
    }
    //endregion
}
