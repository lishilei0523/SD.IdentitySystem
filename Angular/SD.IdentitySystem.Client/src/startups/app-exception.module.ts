import {NgModule, Injectable} from '@angular/core';
import {Router} from "@angular/router";
import {HTTP_INTERCEPTORS, HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {MessageService} from "primeng/api";

/*应用程序异常服务*/
@Injectable({
    providedIn: 'root'
})
export class AppExceptionService implements HttpInterceptor {

    /*路由器*/
    private readonly router: Router;

    /*消息服务*/
    private readonly messageService: MessageService;

    /**
     * 依赖注入构造器
     * */
    public constructor(router: Router, messageService: MessageService) {
        this.router = router;
        this.messageService = messageService;
    }

    /**
     * 拦截请求
     * */
    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError(error => {
                switch (error.status) {
                    case 401:
                        this.messageService.add({severity: "error", summary: error.error});
                        this.router.navigate(["/Login"]);
                        return throwError(error);
                    case 404:
                        this.messageService.add({severity: "error", summary: error.error});
                        return throwError(error);
                    case 500:
                        this.messageService.add({severity: "error", summary: error.error});
                        return throwError(error);
                    default:
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
