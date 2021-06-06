import {Injectable} from '@angular/core';
import {ApplicationType} from "../../values/enums/application-type";
import {Menu} from "./menu.model";

/*菜单服务*/
@Injectable({
    providedIn: 'root'
})
export class MenuService {

    /**
     * 获取菜单列表
     * @return - 菜单列表
     * */
    public getMenus(): Array<Menu> {
        let menus: Array<Menu> = new Array<Menu>();

        let root: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001", "身份认证系统", "", "身份认证系统", "bank", 1, 1, true, false, "", null);
        let systemManagement: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001-1", "信息系统管理", "/Home/InfoSystem", "信息系统管理", "", 2, 2, false, true, "", root);
        let userManagement: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001-2", "用户管理", "/Home/User", "用户管理", "", 3, 2, false, true, "", root);
        let roleManagement: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001-3", "角色管理", "/Home/Role", "角色管理", "", 4, 2, false, true, "", root);
        let menuManagement: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001-4", "菜单管理", "/Home/Menu", "菜单管理", "", 5, 2, false, true, "", root);
        let authorityManagement: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001-5", "权限管理", "/Home/Authority", "权限管理", "", 6, 2, false, true, "", root);
        let loginRecordManagement: Menu = new Menu("00", "身份认证系统", ApplicationType.IOS, "001-6", "登录记录", "/Home/LoginRecord", "登录记录", "", 7, 2, false, true, "", root);

        menus.push(root);

        return menus;
    }
}
