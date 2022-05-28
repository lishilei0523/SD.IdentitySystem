import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Constants, LoginInfo} from "sd-infrastructure";

/*首页服务*/
@Injectable({
    providedIn: 'root'
})
export class HomeService {

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

    //region # 登录 —— login(loginId: string, password: string)
    /**
     * 登录
     * @param loginId - 用户名
     * @param password - 密码
     * */
    public login(loginId: string, password: string)
        : Promise<LoginInfo> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authentication/Login`;
        let params = {
            loginId: loginId,
            password: password,
            clientId: null
        };

        return this._httpClient.post<LoginInfo>(url, params).toPromise();
    }
    //endregion

    //region # 修改密码 —— async updatePassword(loginId: string, oldPassword...
    /**
     * 修改密码
     * @param loginId - 用户名
     * @param oldPassword - 旧密码
     * @param newPassword - 新密码
     * */
    public async updatePassword(loginId: string, oldPassword: string, newPassword: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/UpdatePassword`;
        let params = {
            loginId: loginId,
            oldPassword: oldPassword,
            newPassword: newPassword
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion
}
