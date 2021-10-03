import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {NzModalRef} from "ng-zorro-antd/modal";
import {NzTreeComponent, NzTreeNode} from "ng-zorro-antd/tree";
import {ComponentBase, NzNode} from "sd-infrastructure";
import {UserService} from "../../../services/user.service";
import {Role} from "../../../models/role";
import {RoleMap} from "../../../maps/role.map";
import {RoleService} from "../../../services/role.service";

/*用户分配角色组件*/
@Component({
    selector: 'app-user-relate-role',
    templateUrl: './relate-role.component.html',
    styleUrls: ['./relate-role.component.css']
})
export class RelateRoleComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*角色服务*/
    private readonly _roleService: RoleService;

    /*用户服务*/
    private readonly _userService: UserService;

    /**
     * 创建用户分配角色组件构造器
     * */
    public constructor(modalRef: NzModalRef, roleService: RoleService, userService: UserService) {
        super();
        this._modalRef = modalRef;
        this._roleService = roleService;
        this._userService = userService;
    }

    //endregion

    //region # 属性

    /*NZ树组件*/
    @ViewChild('nzTreeComponent', {static: false})
    public nzTreeComponent!: NzTreeComponent;

    /*用户名*/
    @Input()
    public loginId: string = "";

    /*角色树*/
    public roleTree: Array<NzNode> = new Array<NzNode>();

    /*勾选角色Id集*/
    public checkedRoleIds: Array<string> = new Array<string>();

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— async ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        //加载角色树
        await this.loadRoleTree();
    }
    //endregion


    //Actions

    //region 提交 —— async submit()
    /**
     * 提交
     * */
    public async submit(): Promise<void> {
        this.busy();

        //获取勾选节点
        let checkedNodes: Array<NzTreeNode> = this.nzTreeComponent.getCheckedNodeList();
        let checkedRoleIds: Array<string> = checkedNodes.map(x => x.key);

        let promise: Promise<void> = this._userService.relateRolesToUser(this.loginId, checkedRoleIds);
        promise.catch(_ => {
            this.idle();
        });
        await promise;

        this.idle();
        this._modalRef.close(true);
    }
    //endregion

    //region 取消 —— cancel()
    /**
     * 取消
     * */
    public cancel(): void {
        this._modalRef.close(false);
    }
    //endregion


    //Private

    //region 加载角色树 —— async loadRoleTree()
    /**
     * 加载角色树
     * */
    private async loadRoleTree(): Promise<void> {
        let roles: Array<Role> = await this._roleService.getRoles(null, null, null);
        let userRoles: Array<Role> = await this._roleService.getRoles(null, this.loginId, null);

        let roleTree: Array<NzNode> = RoleMap.toNzTree(roles);
        let checkedKeys: Array<string> = new Array<string>();
        for (let infoSystemNode of roleTree) {
            for (const roleNode of infoSystemNode.children) {
                let exists: boolean = userRoles.some(x => x.id == roleNode.key);
                if (exists) {
                    checkedKeys.push(roleNode.key);
                }
            }
        }

        this.roleTree = roleTree;
        this.checkedRoleIds = checkedKeys;
    }
    //endregion

    //endregion
}
