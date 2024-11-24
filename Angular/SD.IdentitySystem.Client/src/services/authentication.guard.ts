import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {Membership} from "../values/constants/membership";

/*身份认证守卫器*/
@Injectable({
    providedIn: 'root'
})
export class AuthenticationGuard {

    /*路由器*/
    private readonly router: Router;

    /**
     * 依赖注入构造器
     * */
    public constructor(router: Router) {
        this.router = router;
    }

    /**
     * 是否可激活
     * */
    public async canActivate(): Promise<boolean> {
        if (Membership.loginInfo) {
            return true;
        } else {
            await this.router.navigate(["/Login"])
            return false;
        }
    }
}
