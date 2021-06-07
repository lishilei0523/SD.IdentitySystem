import {NgModule} from '@angular/core';
import {Injectable} from "@angular/core";
import {
    HTTP_INTERCEPTORS,
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError, retry} from 'rxjs/operators';
import {MessageService} from "primeng/api";

/*应用程序异常服务*/
@Injectable({
    providedIn: 'root'
})
export class AppExceptionService implements HttpInterceptor {

    /*消息服务*/
    private messageService: MessageService;

    /**
     * 创建应用程序异常服务构造器
     * */
    public constructor(messageService: MessageService) {
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

                return throwError(error);
                //return new Observable<never>();
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
