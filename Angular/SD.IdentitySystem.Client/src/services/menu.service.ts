import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ApplicationType, Constants} from "sd-infrastructure";
import {Menu} from "../models/menu";

/*菜单服务*/
@Injectable({
    providedIn: 'root'
})
export class MenuService {

    //region # 字段及构造器

    /*Http客户端*/
    private readonly _httpClient: HttpClient;

    /**
     * 依赖注入构造器
     * */
    public constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    //endregion

    //region # 创建菜单 —— async createMenu(infoSystemNo: string, applicationType...
    /**
     * 创建菜单
     * @param infoSystemNo - 信息系统编号
     * @param applicationType - 应用程序类型
     * @param menuName - 菜单名称
     * @param sort - 排序
     * @param url - 链接地址
     * @param path - 路径
     * @param icon - 图标
     * @param parentNodeId - 上级节点Id
     * */
    public async createMenu(infoSystemNo: string, applicationType: ApplicationType, menuName: string, sort: number, url: string | null, path: string | null, icon: string | null, parentNodeId: string | null)
        : Promise<void> {
        let requestUrl: string = `${Constants.appConfig.webApiPrefix}/Authorization/CreateMenu`;
        let params = {
            infoSystemNo: infoSystemNo,
            applicationType: applicationType,
            menuName: menuName,
            sort: sort,
            url: url,
            path: path,
            icon: icon,
            parentNodeId: parentNodeId
        };

        await this._httpClient.post(requestUrl, params).toPromise();
    }
    //endregion

    //region # 修改菜单 —— async updateMenu(menuId: string, menuName: string...
    /**
     * 修改菜单
     * @param menuId - 菜单Id
     * @param menuName - 菜单名称
     * @param sort - 排序
     * @param url - 链接地址
     * @param path - 路径
     * @param icon - 图标
     * */
    public async updateMenu(menuId: string, menuName: string, sort: number, url: string | null, path: string | null, icon: string | null)
        : Promise<void> {
        let requestUrl: string = `${Constants.appConfig.webApiPrefix}/Authorization/UpdateMenu`;
        let params = {
            menuId: menuId,
            menuName: menuName,
            sort: sort,
            url: url,
            path: path,
            icon: icon
        };

        await this._httpClient.post(requestUrl, params).toPromise();
    }
    //endregion

    //region # 删除菜单 —— async removeMenu(menuId: string)
    /**
     * 删除菜单
     * @param menuId - 菜单Id
     * */
    public async removeMenu(menuId: string)
        : Promise<void> {
        let requestUrl: string = `${Constants.appConfig.webApiPrefix}/Authorization/RemoveMenu`;
        let params = {
            menuId: menuId
        };

        await this._httpClient.post(requestUrl, params).toPromise();
    }
    //endregion

    //region # 关联权限到菜单 —— async relateAuthoritiesToMenu(menuId: string, authorityIds...
    /**
     * 关联权限到菜单
     * @param menuId - 菜单Id
     * @param authorityIds - 权限Id集
     * */
    public async relateAuthoritiesToMenu(menuId: string, authorityIds: Array<string>)
        : Promise<void> {
        let requestUrl: string = `${Constants.appConfig.webApiPrefix}/Authorization/RelateAuthoritiesToMenu`;
        let params = {
            menuId: menuId,
            authorityIds: authorityIds
        };

        await this._httpClient.post(requestUrl, params).toPromise();
    }
    //endregion

    //region # 获取菜单列表 —— getMenus(infoSystemNo: string | null, applicationType...
    /**
     * 获取菜单列表
     * @param infoSystemNo - 信息系统编号
     * @param applicationType - 应用程序类型
     * */
    public getMenus(infoSystemNo: string | null, applicationType: ApplicationType | null)
        : Promise<Array<Menu>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetMenus`;
        let params = new HttpParams()
            .set("infoSystemNo", infoSystemNo ? infoSystemNo : "")
            .set("applicationType", applicationType == null ? "" : applicationType.toString());

        return this._httpClient.get<Array<Menu>>(url, {params}).toPromise();
    }
    //endregion
}
