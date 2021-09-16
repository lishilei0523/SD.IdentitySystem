import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {PageModel} from "../../values/structs/page-model";
import {Constants} from "../../values/constants/constants";
import {ApplicationType} from "../../values/enums/application-type";
import {InfoSystem} from "./info-system.model";

/*信息系统服务*/
@Injectable({
    providedIn: 'root'
})
export class InfoSystemService {

    /*Http客户端*/
    private readonly httpClient: HttpClient;

    /**
     * 依赖注入构造器
     * */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    /**
     * 创建信息系统
     * @param systemNo - 信息系统编号
     * @param systemName - 信息系统名称
     * @param adminLoginId - 系统管理员账号
     * @param applicationType - 应用程序类型
     * */
    public async createInfoSystem(systemNo: string, systemName: string, adminLoginId: string, applicationType: ApplicationType)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/CreateInfoSystem`;
        let params = {
            systemNo: systemNo,
            systemName: systemName,
            adminLoginId: adminLoginId,
            applicationType: applicationType
        };

        await this.httpClient.post(url, params).toPromise();
    }

    /**
     * 修改信息系统
     * @param infoSystemId - 信息系统Id
     * @param systemName - 信息系统名称
     * */
    public async updateInfoSystem(infoSystemId: string, systemName: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/UpdateInfoSystem`;
        let params = {
            infoSystemId: infoSystemId,
            systemName: systemName
        };

        await this.httpClient.post(url, params).toPromise();
    }

    /**
     * 初始化信息系统
     * @param infoSystemId - 信息系统Id
     * @param host - 主机名称
     * @param port - 端口
     * @param index - 首页
     * */
    public async initInfoSystem(infoSystemId: string, host: string, port: number, index: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/InitInfoSystem`;
        let params = {
            infoSystemId: infoSystemId,
            host: host,
            port: port,
            index: index
        };

        await this.httpClient.post(url, params).toPromise();
    }

    /**
     * 分页获取信息系统列表
     * @param keywords - 关键字
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getInfoSystemsByPage(keywords: string, pageIndex: number, pageSize: number):
        Promise<PageModel<InfoSystem>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetInfoSystemsByPage`;
        let params = new HttpParams()
            .set("keywords", keywords)
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this.httpClient.get<PageModel<InfoSystem>>(url, {params}).toPromise();
    }
}
