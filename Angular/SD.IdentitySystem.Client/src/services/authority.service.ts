import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {ApplicationType, Constants, PageModel} from "sd-infrastructure";
import {Authority} from "../models/authority";

/*权限服务*/
@Injectable({
    providedIn: 'root'
})
export class AuthorityService {

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

    //region # 创建权限 —— async createAuthority(systemNo: string, applicationType...
    /**
     * 创建权限
     * @param systemNo - 信息系统编号
     * @param applicationType - 应用程序类型
     * @param authorityName - 权限名称
     * @param authorityPath - 权限路径
     * @param englishName - 英文名称
     * @param assemblyName - 程序集名称
     * @param namespace - 命名空间
     * @param className - 类名
     * @param methodName - 方法名
     * @param description - 描述
     * */
    public async createAuthority(systemNo: string, applicationType: ApplicationType, authorityName: string, authorityPath: string, englishName: string | null, assemblyName: string | null, namespace: string | null, className: string | null, methodName: string | null, description: string | null): Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/CreateAuthority`;
        let params = {
            systemNo: systemNo,
            applicationType: applicationType,
            authorityName: authorityName,
            authorityPath: authorityPath,
            englishName: englishName,
            assemblyName: assemblyName,
            namespace: namespace,
            className: className,
            methodName: methodName,
            description: description,
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 修改权限 —— async updateAuthority(authorityId: string, authorityName...
    /**
     * 修改权限
     * @param authorityId - 权限Id
     * @param authorityName - 权限名称
     * @param authorityPath - 权限路径
     * @param englishName - 英文名称
     * @param assemblyName - 程序集名称
     * @param namespace - 命名空间
     * @param className - 类名
     * @param methodName - 方法名
     * @param description - 描述
     * */
    public async updateAuthority(authorityId: string, authorityName: string, authorityPath: string, englishName: string | null, assemblyName: string | null, namespace: string | null, className: string | null, methodName: string | null, description: string | null)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/UpdateAuthority`;
        let params = {
            authorityId: authorityId,
            authorityName: authorityName,
            authorityPath: authorityPath,
            englishName: englishName,
            assemblyName: assemblyName,
            namespace: namespace,
            className: className,
            methodName: methodName,
            description: description,
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 删除权限 —— async removeAuthority(authorityId: string)
    /**
     * 删除权限
     * @param authorityId - 权限Id
     * */
    public async removeAuthority(authorityId: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/RemoveAuthority`;
        let params = {
            authorityId: authorityId
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 获取权限列表 —— getAuthorities(keywords: string | null, systemNo: string...
    /**
     * 获取权限列表
     * @param keywords - 关键字
     * @param systemNo - 信息系统编号
     * @param applicationType - 应用程序类型
     * @param menuId - 菜单Id
     * @param roleId - 角色Id
     * */
    public getAuthorities(keywords: string | null, systemNo: string | null, applicationType: ApplicationType | null, menuId: string | null, roleId: string | null)
        : Promise<Array<Authority>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetAuthorities`;
        let params = new HttpParams()
            .set("keywords", keywords ? keywords : "")
            .set("systemNo", systemNo ? systemNo : "")
            .set("applicationType", applicationType ? applicationType.toString() : "")
            .set("menuId", menuId ? menuId : "")
            .set("roleId", roleId ? roleId : "");

        return this._httpClient.get<Array<Authority>>(url, {params}).toPromise();
    }
    //endregion

    //region # 分页获取权限列表 —— getAuthoritiesByPage(keywords: string | null, systemNo: string...
    /**
     * 分页获取权限列表
     * @param keywords - 关键字
     * @param systemNo - 信息系统编号
     * @param applicationType - 应用程序类型
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getAuthoritiesByPage(keywords: string | null, systemNo: string | null, applicationType: ApplicationType | null, pageIndex: number, pageSize: number)
        : Promise<PageModel<Authority>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetAuthoritiesByPage`;
        let params = new HttpParams()
            .set("keywords", keywords ? keywords : "")
            .set("systemNo", systemNo ? systemNo : "")
            .set("applicationType", applicationType ? applicationType.toString() : "")
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this._httpClient.get<PageModel<Authority>>(url, {params}).toPromise();
    }
    //endregion
}
