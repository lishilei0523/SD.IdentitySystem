import {Directive, Input, TemplateRef, ViewContainerRef} from '@angular/core';
import {Constants} from "../values/constants/constants";

/*Angular授权指令*/
@Directive({
    selector: '[authorityPath]'
})
export class AuthorityDirective {
    /*是否已授权*/
    private authorized: boolean;

    /*模板引用*/
    private template: TemplateRef<any>;

    /*视图容器引用*/
    private viewContainer: ViewContainerRef;

    /**
     * 创建Angular授权指令构造器
     * */
    public constructor(template: TemplateRef<any>, viewContainer: ViewContainerRef) {
        this.authorized = false;
        this.template = template;
        this.viewContainer = viewContainer;
    }

    /**
     * 设置权限路径
     * @param authorityPath - 权限路径
     * */
    @Input()
    public set authorityPath(authorityPath: string) {
        if (Constants.allowedAuthorityPaths.includes(authorityPath)) {
            this.authorized = true;
        }

        if (this.authorized) {
            this.viewContainer.createEmbeddedView(this.template);
        } else if (!this.authorized) {
            this.viewContainer.clear();
        }
    }
}
