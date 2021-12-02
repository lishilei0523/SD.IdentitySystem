import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Constants, ApplicationType, PageModel} from "sd-infrastructure";
import {InfoSystem} from "../models/info-system";

/*信息系统服务*/
@Injectable({
    providedIn: 'root'
})
export class InfoSystemService {

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

    //region # 创建信息系统 —— async createInfoSystem(infoSystemNo: string...
    /**
     * 创建信息系统
     * @param infoSystemNo - 信息系统编号
     * @param infoSystemName - 信息系统名称
     * @param adminLoginId - 系统管理员账号
     * @param applicationType - 应用程序类型
     * */
    public async createInfoSystem(infoSystemNo: string, infoSystemName: string, adminLoginId: string, applicationType: ApplicationType)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/CreateInfoSystem`;
        let params = {
            infoSystemNo: infoSystemNo,
            infoSystemName: infoSystemName,
            adminLoginId: adminLoginId,
            applicationType: applicationType
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 修改信息系统 —— async updateInfoSystem(infoSystemNo: string...
    /**
     * 修改信息系统
     * @param infoSystemNo - 信息系统编号
     * @param infoSystemName - 信息系统名称
     * */
    public async updateInfoSystem(infoSystemNo: string, infoSystemName: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/UpdateInfoSystem`;
        let params = {
            infoSystemNo: infoSystemNo,
            infoSystemName: infoSystemName
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 初始化信息系统 —— async initInfoSystem(infoSystemNo: string...
    /**
     * 初始化信息系统
     * @param infoSystemNo - 信息系统编号
     * @param host - 主机名称
     * @param port - 端口
     * @param index - 首页
     * */
    public async initInfoSystem(infoSystemNo: string, host: string, port: number, index: string)
        : Promise<void> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/InitInfoSystem`;
        let params = {
            infoSystemNo: infoSystemNo,
            host: host,
            port: port,
            index: index
        };

        await this._httpClient.post(url, params).toPromise();
    }
    //endregion

    //region # 获取信息系统列表 —— getInfoSystems()
    /**
     * 获取信息系统列表
     * */
    public getInfoSystems():
        Promise<Array<InfoSystem>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetInfoSystems`;

        return this._httpClient.get<Array<InfoSystem>>(url).toPromise();
    }
    //endregion

    //region # 分页获取信息系统列表 —— getInfoSystemsByPage(keywords: string | null, pageIndex...
    /**
     * 分页获取信息系统列表
     * @param keywords - 关键字
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getInfoSystemsByPage(keywords: string | null, pageIndex: number, pageSize: number):
        Promise<PageModel<InfoSystem>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/Authorization/GetInfoSystemsByPage`;
        let params = new HttpParams()
            .set("keywords", keywords ? keywords : "")
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this._httpClient.get<PageModel<InfoSystem>>(url, {params}).toPromise();
    }
    //endregion
}
