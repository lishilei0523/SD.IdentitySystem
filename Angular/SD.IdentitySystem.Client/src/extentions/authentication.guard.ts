import {Injectable} from '@angular/core';
import {CanActivate, Router} from "@angular/router";
import {Constants} from "../values/constants/constants";

/*身份认证守卫器*/
@Injectable({
    providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

    /*路由*/
    private router: Router;

    /**
     * 创建身份认证守卫器构造器
     * */
    public constructor(router: Router) {
        this.router = router;
    }

    /**
     * 是否可激活
     * */
    public async canActivate(): Promise<boolean> {
        if (Constants.loginInfo) {
            return true;
        } else {
            await this.router.navigate(["/Login"])
            return false;
        }
    }
}
