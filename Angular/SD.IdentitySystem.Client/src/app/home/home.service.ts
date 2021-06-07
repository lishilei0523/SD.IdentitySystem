import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginInfo} from "../../values/structs/loginInfo";
import {Constants} from "../../values/constants/constants";

/*首页服务*/
@Injectable({
    providedIn: 'root'
})
export class HomeService {

    /*Http客户端*/
    private httpClient: HttpClient;

    /**
     * 创建首页服务构造器
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
        let loginInfo: LoginInfo = await this.httpClient.post<LoginInfo>(url, params, Constants.httpPostOptions).toPromise();

        return loginInfo;
    }
}
