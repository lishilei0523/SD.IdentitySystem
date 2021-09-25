import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Constants, PageModel} from "sd-infrastructure";
import {Role} from "../models/role";

/*角色服务*/
@Injectable({
    providedIn: 'root'
})
export class RoleService {

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

    //region # 创建角色 —— async createRole(systemNo: string, roleName...
    /**
     * 创建角色
     * @param systemNo - 信息系统编号
     * @param roleName - 角色名称
     * @param description - 描述
     * @param authorityIds - 权限Id集
     * */
    public async createRole(systemNo: string, roleName: string, description: string, authorityIds: Set<string>)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/CreateRole`;
        let params = {
            systemNo: systemNo,
            roleName: roleName,
            authorityIds: authorityIds
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 修改角色 —— async updateRole(roleId: string, systemNo...
    /**
     * 修改角色
     * @param roleId - 角色Id
     * @param systemNo - 信息系统编号
     * @param roleName - 角色名称
     * @param description - 描述
     * @param authorityIds - 权限Id集
     * */
    public async updateRole(roleId: string, systemNo: string, roleName: string, description: string, authorityIds: Set<string>)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/UpdateRole`;
        let params = {
            roleId: roleId,
            systemNo: systemNo,
            roleName: roleName,
            authorityIds: authorityIds
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 删除角色 —— async removeRole(roleId: string)
    /**
     * 删除角色
     * @param roleId - 角色Id
     * */
    public async removeRole(roleId: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/RemoveRole`;
        let params = {
            roleId: roleId
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 分页获取角色列表 —— getRolesByPage(keywords: string, systemNo...
    /**
     * 分页获取角色列表
     * @param keywords - 关键字
     * @param systemNo - 信息系统编号
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getRolesByPage(keywords: string, systemNo: string, pageIndex: number, pageSize: number)
        : Promise<PageModel<Role>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetRolesByPage`;
        let params = new HttpParams()
            .set("keywords", keywords)
            .set("systemNo", systemNo ? systemNo : "")
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this._httpClient.get<PageModel<Role>>(url, {params}).toPromise();
    }
    //endregion
}
