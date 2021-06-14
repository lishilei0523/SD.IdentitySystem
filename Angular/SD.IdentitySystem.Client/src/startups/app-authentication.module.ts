import {NgModule, Injectable} from '@angular/core';
import {HTTP_INTERCEPTORS, HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Membership} from "../values/constants/membership";

/*应用程序身份认证服务*/
@Injectable({
    providedIn: 'root'
})
export class AppAuthenticationService implements HttpInterceptor {

    /**
     * 拦截请求
     * */
    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let publicKey: string = Membership.loginInfo == null ? "" : Membership.loginInfo.publicKey;
        if (request.method == "GET" && publicKey) {
            let headers = {
                "CurrentPublicKey": publicKey
            };
            return next.handle(request.clone({setHeaders: headers}));
        }
        if (request.method == "POST") {
            let contentType: string = "application/json";
            if (publicKey) {
                let headers = {
                    "Content-Type": contentType,
                    "CurrentPublicKey": publicKey
                };
                return next.handle(request.clone({setHeaders: headers}));
            } else {
                let headers = {
                    "Content-Type": contentType
                };
                return next.handle(request.clone({setHeaders: headers}));
            }
        }

        return next.handle(request);
    }
}

/*应用程序身份认证模块*/
@NgModule({
    providers: [
        AppAuthenticationService, {
            provide: HTTP_INTERCEPTORS,
            useClass: AppAuthenticationService,
            multi: true
        }
    ]
})
export class AppAuthenticationModule {

}
