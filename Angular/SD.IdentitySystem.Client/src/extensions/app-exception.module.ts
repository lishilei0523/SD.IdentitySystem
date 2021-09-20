import {NgModule, Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {HTTP_INTERCEPTORS, HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {NzMessageService} from "ng-zorro-antd/message";

/*应用程序异常服务*/
@Injectable({
    providedIn: 'root'
})
export class AppExceptionService implements HttpInterceptor {

    /*路由器*/
    private readonly router: Router;

    /*消息服务*/
    private readonly messageService: NzMessageService;

    /**
     * 依赖注入构造器
     * */
    public constructor(router: Router, messageService: NzMessageService) {
        this.router = router;
        this.messageService = messageService;
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
                        this.messageService.create("error", error.error);
                        this.router.navigate(["/Login"]);
                        return throwError(error);
                    case 404:
                        this.messageService.create("error", error.error);
                        return throwError(error);
                    case 500:
                        this.messageService.create("error", error.error);
                        return throwError(error);
                    default:
                        this.messageService.create("error", error.message);
                        return throwError(error);
                }
            }));
    }
}

/*应用程序异常模块*/
@NgModule({
    providers: [
        AppExceptionService, {
            provide: HTTP_INTERCEPTORS,
            useClass: AppExceptionService,
            multi: true
        }
    ]
})
export class AppExceptionModule {

}
