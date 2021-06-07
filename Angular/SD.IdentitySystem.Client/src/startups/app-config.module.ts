import {NgModule, Injectable, APP_INITIALIZER} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Constants} from "../values/constants/constants";
import {AppConfig} from "../values/structs/app-config";

/*应用程序配置服务*/
@Injectable({
    providedIn: 'root'
})
export class AppConfigService {

    /*Http客户端*/
    private httpClient: HttpClient;

    /**
     * 创建应用程序配置服务构造器
     * */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    /**
     * 加载应用程序配置
     * */
    public async loadAppConfig(): Promise<void> {
        let configUrl: string = "assets/app.config.json";
        Constants.appConfig = await this.httpClient.get<AppConfig>(configUrl).toPromise();
    }
}

/*应用程序配置模块*/
@NgModule({
    providers: [
        AppConfigService, {
            provide: APP_INITIALIZER,
            useFactory: InitializeAppConfig,
            deps: [AppConfigService],
            multi: true
        }
    ]
})
export class AppConfigModule {

}

/**
 * 初始化应用程序配置函数
 * @param appConfigService - 应用程序配置服务
 * */
export function InitializeAppConfig(appConfigService: AppConfigService) {
    return () => appConfigService.loadAppConfig();
}
