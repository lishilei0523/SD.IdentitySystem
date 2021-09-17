import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {PageModel} from "../../values/structs/page-model";
import {LoginRecord} from "./login-record.model";
import {Constants} from "../../values/constants/constants";

/*登录记录服务*/
@Injectable({
    providedIn: 'root'
})
export class LoginRecordService {

    //region # 字段及构造器

    /*Http客户端*/
    private readonly httpClient: HttpClient;

    /**
     * 依赖注入构造器
     * */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    //endregion

    //region # 分页获取登录记录列表 —— getLoginRecordsByPage(keywords: string, startTime...
    /**
     * 分页获取登录记录列表
     * @param keywords - 关键字
     * @param startTime - 开始时间
     * @param endTime - 结束时间
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getLoginRecordsByPage(keywords: string, startTime: string, endTime: string, pageIndex: number, pageSize: number)
        : Promise<PageModel<LoginRecord>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/GetLoginRecordsByPage`;
        let params = new HttpParams()
            .set("keywords", keywords)
            .set("startTime", startTime)
            .set("endTime", endTime)
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this.httpClient.get<PageModel<LoginRecord>>(url, {params}).toPromise();
    }
    //endregion
}
