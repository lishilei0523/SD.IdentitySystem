import {ApplicationType, ApplicationTypeDescriptor, NzNode} from "sd-infrastructure";
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

    //region # 应用程序类型/权限NZ树映射 —— static toNzTree(authorities: Array<Authority>)
    /**
     * 应用程序类型/权限NZ树映射
     * */
    public static toNzTree(authorities: Array<Authority>): Array<NzNode> {
        let nzTree: Array<NzNode> = new Array<NzNode>();

        let applicationTypeDescriptor = new ApplicationTypeDescriptor();
        let applicationTypes: Array<ApplicationType> = authorities.map(x => x.applicationType);
        let distinctedApplicationTypes: Array<ApplicationType> = new Array<ApplicationType>();
        for (let applicationType of applicationTypes) {
            let exists = distinctedApplicationTypes.some(x => x == applicationType);
            if (!exists) {
                distinctedApplicationTypes.push(applicationType);
            }
        }
        for (let applicationType of distinctedApplicationTypes) {
            let applicationTypeName = applicationTypeDescriptor.transform(applicationType);
            let applicationTypeNode: NzNode = new NzNode(applicationTypeName, applicationTypeName, false);
            applicationTypeNode.disabled = true;
            nzTree.push(applicationTypeNode);

            let specAuthorities: Array<Authority> = authorities.filter(x => x.applicationType == applicationType);
            for (let authority of specAuthorities) {
                let authorityNode: NzNode = AuthorityMap.toNzNode(authority);
                applicationTypeNode.children.push(authorityNode);
            }
        }

        return nzTree;
    }
    //endregion
}
