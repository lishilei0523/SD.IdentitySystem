import {ActivatedRouteSnapshot, DetachedRouteHandle, RouteReuseStrategy} from '@angular/router';

/*选项卡路由重用策略*/
export class TabRouteReuseStrategy extends RouteReuseStrategy {
    // 路由快照
    // [key:string] 键为字符串类型
    // DetachedRouteHandle 值为路由处理器
    public static snapshots: { [key: string]: DetachedRouteHandle } = {};

    /**
     * 从缓存中获取快照
     * @param {ActivatedRouteSnapshot} route
     * @return {DetachedRouteHandle | null}
     */
    public retrieve(route: ActivatedRouteSnapshot): DetachedRouteHandle | null {
        if (!route.routeConfig) {
            return null;
        } else if (!route.routeConfig.path) {
            return null;
        } else {
            return TabRouteReuseStrategy.snapshots[route.routeConfig.path];
        }
    }

    /**
     * 是否允许还原
     * @param {ActivatedRouteSnapshot} route
     * @return {boolean} true-允许还原
     */
    public shouldAttach(route: ActivatedRouteSnapshot): boolean {
        if (!route.routeConfig) {
            return false;
        } else if (!route.routeConfig.path) {
            return false;
        } else {
            let snapshot = TabRouteReuseStrategy.snapshots[route.routeConfig.path]
            if (snapshot) {
                return true;
            } else {
                return false;
            }
        }
    }

    /**
     * 确定是否应该分离此路由（及其子树）以便以后重用
     * @param {ActivatedRouteSnapshot} route
     * @return {boolean}
     */
    public shouldDetach(route: ActivatedRouteSnapshot): boolean {
        if (route.routeConfig && route.routeConfig.data) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * 进入路由触发, 判断是否为同一路由
     * @param {ActivatedRouteSnapshot} future
     * @param {ActivatedRouteSnapshot} curr
     * @return {boolean}
     */
    public shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot): boolean {
        // future - 未来的(下一个)路由快照
        return future.routeConfig === curr.routeConfig;
    }

    /**
     * 保存路由
     * @param {ActivatedRouteSnapshot} route
     * @param {DetachedRouteHandle | null} handle
     */
    public store(route: ActivatedRouteSnapshot, handle: DetachedRouteHandle | null): void {
        // 通过 Route.path 映射路由快照, 一定要确保它的唯一性
        // 也可以通过 route.routeConfig.data.uid 或其他可以确定唯一性的数据作为映射key
        // 作者这里能够确保 path 的唯一性
        if (route.routeConfig && route.routeConfig.path && handle) {
            TabRouteReuseStrategy.snapshots[route.routeConfig.path] = handle;
        }
    }
}
