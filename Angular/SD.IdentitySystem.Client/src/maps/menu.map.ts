import {Menu} from "../models/menu";

/*菜单映射工具类*/
export class MenuMap {

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
}
