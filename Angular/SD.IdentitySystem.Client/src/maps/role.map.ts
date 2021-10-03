import {NzNode} from "sd-infrastructure";
import {InfoSystem} from "../models/info-system";
import {Role} from "../models/role";

/*角色映射工具类*/
export class RoleMap {

    //region # 角色NZ树节点映射 —— static toNzNode(role: Role)
    /**
     * 角色NZ树节点映射
     * */
    public static toNzNode(role: Role): NzNode {
        return new NzNode(role.id, role.name, true);
    }
    //endregion

    //region # 信息系统/角色NZ树映射 —— static toNzTree(roles: Array<Role>)
    /**
     * 信息系统/角色NZ树映射
     * */
    public static toNzTree(roles: Array<Role>): Array<NzNode> {
        let nzTree: Array<NzNode> = new Array<NzNode>();

        let infoSystems: Array<InfoSystem> = roles.filter(x => x.infoSystemInfo != null).map(x => x.infoSystemInfo!);
        let distinctedInfoSystems: Array<InfoSystem> = new Array<InfoSystem>();
        for (let infoSystem of infoSystems) {
            let exists = distinctedInfoSystems.some(x => x.id == infoSystem.id);
            if (!exists) {
                distinctedInfoSystems.push(infoSystem);
            }
        }
        for (let infoSystem of distinctedInfoSystems) {
            let infoSytemNode: NzNode = new NzNode(infoSystem.id, infoSystem.name, false);
            infoSytemNode.disabled = true;
            nzTree.push(infoSytemNode);

            let specRoles: Array<Role> = roles.filter(x => x.infoSystemInfo?.id == infoSystem.id);
            for (let role of specRoles) {
                let roleNode: NzNode = RoleMap.toNzNode(role);
                infoSytemNode.children.push(roleNode);
            }
        }

        return nzTree;
    }
    //endregion
}
