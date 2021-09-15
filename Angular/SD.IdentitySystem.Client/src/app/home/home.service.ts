import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginInfo} from "../../values/structs/login-info";
import {Constants} from "../../values/constants/constants";

/*首页服务*/
@Injectable({
    providedIn: 'root'
})
export class HomeService {

    /*Http客户端*/
    private readonly httpClient: HttpClient;

    /**
     * 依赖注入构造器
     * */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    /**
     * 登录
     * @param loginId - 用户名
     * @param password - 密码
     * */
    public async login(loginId: string, password: string): Promise<LoginInfo> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authentication/Login`;
        let params = {
            loginId: loginId,
            password: password
        };

        return await this.httpClient.post<LoginInfo>(url, params).toPromise();
    }

    /**
     * 修改密码
     * @param loginId - 用户名
     * @param oldPassword - 旧密码
     * @param newPassword - 新密码
     * */
    public async updatePassword(loginId: string, oldPassword: string, newPassword: string): Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/UpdatePassword`;
        let params = {
            loginId: loginId,
            oldPassword: oldPassword,
            newPassword: newPassword
        };

        await this.httpClient.post(url, params).toPromise();
    }
}
