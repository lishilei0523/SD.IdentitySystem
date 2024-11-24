import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Constants} from "../values/constants/constants";
import {PageModel} from "../values/structs/page-model";
import {LoginRecord} from "../models/login-record";

/*登录记录服务*/
@Injectable({
    providedIn: 'root'
})
export class LoginRecordService {

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

    //region # 分页获取登录记录列表 —— getLoginRecordsByPage(keywords: string | null, startTime...
    /**
     * 分页获取登录记录列表
     * @param keywords - 关键字
     * @param startTime - 开始时间
     * @param endTime - 结束时间
     * @param pageIndex - 页码
     * @param pageSize - 页容量
     * */
    public getLoginRecordsByPage(keywords: string | null, startTime: string | null, endTime: string | null, pageIndex: number, pageSize: number)
        : Promise<PageModel<LoginRecord>> {
        let url: string = `${Constants.appConfig.webApiPrefix}/User/GetLoginRecordsByPage`;
        let params = new HttpParams()
            .set("keywords", keywords ? keywords : "")
            .set("startTime", startTime ? startTime : "")
            .set("endTime", endTime ? endTime : "")
            .set("pageIndex", pageIndex.toString())
            .set("pageSize", pageSize.toString());

        return this._httpClient.get<PageModel<LoginRecord>>(url, {params}).toPromise();
    }
    //endregion
}
