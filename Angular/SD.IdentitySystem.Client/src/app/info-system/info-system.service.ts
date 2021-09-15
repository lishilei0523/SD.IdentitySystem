import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";

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

    
}
