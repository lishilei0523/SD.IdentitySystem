import {NzNode} from "sd-infrastructure";
import {Menu} from "../models/menu";

/*菜单映射工具类*/
export class MenuMap {

    //region # 菜单树列表映射 —— static toTreeList(menus: Array<Menu>)
    /**
     * 菜单树列表映射
     * */
    public static toTreeList(menus: Array<Menu>): Array<Menu> {
        let allMenus: Array<Menu> = menus ? menus : new Array<Menu>();
        MenuMap.filterMenu(allMenus, 0);

        for (let menu of allMenus) {
            MenuMap.fillSubNodes(menu, allMenus);
        }

        return allMenus.filter(x => x.isRoot);
    }
    //endregion

    //region # 菜单NZ树节点映射 —— static toNzNode(menu: Menu)
    /**
     * 菜单NZ树节点映射
     * */
    public static toNzNode(menu: Menu): NzNode {
        return new NzNode(menu.id, menu.name, menu.isLeaf);
    }
    //endregion

    //region # 菜单NZ树映射 —— static toNzTree(menus: Array<Menu>, parentMenuId...
    /**
     * 菜单NZ树映射
     * */
    public static toNzTree(menus: Array<Menu>, parentMenuId: string | null): Array<NzNode> {
        let nzTree: Array<NzNode> = new Array<NzNode>();

        if (!parentMenuId) {
            let rootMenus = menus.filter(x => x.isRoot);
            for (let menu of rootMenus) {
                let nzNode: NzNode = MenuMap.toNzNode(menu);
                nzTree.push(nzNode);

                nzNode.children = MenuMap.toNzTree(menus, menu.id);
            }
        } else {
            let specMenus = menus.filter(x => x.parentMenuId == parentMenuId);
            for (let menu of specMenus) {
                let nzNode: NzNode = MenuMap.toNzNode(menu);
                nzTree.push(nzNode);

                nzNode.children = MenuMap.toNzTree(menus, menu.id);
            }
        }

        return nzTree;
    }
    //endregion


    //Private

    //region # 填充下级节点 —— static fillSubNodes(menu: Menu, allMenus: Array<Menu>)
    /**
     * 填充下级节点
     * */
    private static fillSubNodes(menu: Menu, allMenus: Array<Menu>): void {
        for (let subMenu of allMenus) {
            if (subMenu.parentMenuId && subMenu.parentMenuId == menu.id) {
                menu.children.push(subMenu);
                subMenu.parent = menu;

                MenuMap.fillSubNodes(subMenu, allMenus);
            }
        }
    }
    //endregion

    //region # 过滤菜单 —— static filterMenu(menus: Array<Menu>, level: number)
    /**
     * 过滤菜单
     * */
    private static filterMenu(menus: Array<Menu>, level: number): void {
        for (let menu of menus) {
            menu.parent = null;
            menu.children = new Array<Menu>();
            if (menu.children.length > 0) {
                MenuMap.filterMenu(menu.children, level + 1);
            }
        }
    }
    //endregion
}
