import {NgModule, Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {HTTP_INTERCEPTORS, HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {NzMessageService} from "ng-zorro-antd/message";

/*应用程序异常过滤器*/
@Injectable({
    providedIn: 'root'
})
export class AppExceptionInterceptor implements HttpInterceptor {

    /*路由器*/
    private readonly _router: Router;

    /*消息服务*/
    private readonly _messageService: NzMessageService;

    /**
     * 依赖注入构造器
     * */
    public constructor(router: Router, messageService: NzMessageService) {
        this._router = router;
        this._messageService = messageService;
    }

    /**
     * 拦截请求
     * */
    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError(error => {
                console.log(error);
                switch (error.status) {
                    case 401:
                        this._messageService.error(error.error);
                        this._router.navigate(["/Login"]);
                        return throwError(error);
                    case 404:
                        this._messageService.error(error.error);
                        return throwError(error);
                    case 500:
                        this._messageService.error(error.error);
                        return throwError(error);
                    default:
                        this._messageService.error(error.message);
                        return throwError(error);
                }
            }));
    }
}

/*应用程序异常模块*/
@NgModule({
    providers: [
        AppExceptionInterceptor,
        {provide: HTTP_INTERCEPTORS, useClass: AppExceptionInterceptor, multi: true}
    ]
})
export class AppExceptionModule {

}
