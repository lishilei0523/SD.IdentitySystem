import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Constants} from "../values/constants/constants";
import {PageModel} from "../values/structs/page-model";
import {User} from "../models/user";

/*用户服务*/
@Injectable({
    providedIn: 'root'
})
export class UserService {

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

    //region # 创建用户 —— async createUser(loginId: string...
    /**
     * 创建用户
     * @param loginId - 用户名
     * @param realName - 真实姓名
     * @param password - 密码
     * */
    public async createUser(loginId: string, realName: string, password: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/CreateUser`;
        let params = {
            loginId: loginId,
            realName: realName,
            password: password
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 重置密码 —— async resetPassword(loginId: string...
    /**
     * 重置密码
     * @param loginId - 用户名
     * @param password - 密码
     * */
    public async resetPassword(loginId: string, password: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/ResetPassword`;
        let params = {
            loginId: loginId,
            password: password
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 设置私钥 —— async setPrivateKey(loginId: string...
    /**
     * 设置私钥
     * @param loginId - 用户名
     * @param privateKey - 私钥
     * */
    public async setPrivateKey(loginId: string, privateKey: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/SetPrivateKey`;
        let params = {
            loginId: loginId,
            privateKey: privateKey
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 启用用户 —— async enableUser(loginId: string)
    /**
     * 启用用户
     * @param loginId - 用户名
     * */
    public async enableUser(loginId: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/EnableUser`;
        let params = {
            loginId: loginId
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 停用用户 —— async disableUser(loginId: string)
    /**
     * 停用用户
     * @param loginId - 用户名
     * */
    public async disableUser(loginId: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/DisableUser`;
        let params = {
            loginId: loginId
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 删除用户 —— async removeUser(loginId: string)
    /**
     * 删除用户
     * @param loginId - 用户名
     * */
    public async removeUser(loginId: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/RemoveUser`;
        let params = {
            loginId: loginId
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 关联角色到用户 —— async relateRolesToUser(loginId: string...
    /**
     * 关联角色到用户
     * @param loginId - 用户名
     * @param roleIds - 角色Id集
     * */
    public async relateRolesToUser(loginId: string, roleIds: Array<string>)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/RelateRolesToUser`;
        let params = {
            loginId: loginId,
            roleIds: roleIds
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 分页获取用户列表 —— getUsersByPage(keywords: string, infoSystemNo...
    /**
     * 分页获取用户列表
     * @param keywords - 关键字
     * @param infoSystemNo - 信息系统编号
     * @param roleId - 角色Id
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getUsersByPage(keywords: string | null, infoSystemNo: string | null, roleId: string, pageIndex: number, pageSize: number)
        : Promise<PageModel<User>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/GetUsersByPage`;
        let params = new HttpParams()
            .set("keywords", keywords ? keywords : "")
            .set("infoSystemNo", infoSystemNo ? infoSystemNo : "")
            .set("roleId", roleId)
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this._httpClient.get<PageModel<User>>(url, {params}).toPromise();
    }
    //endregion
}
