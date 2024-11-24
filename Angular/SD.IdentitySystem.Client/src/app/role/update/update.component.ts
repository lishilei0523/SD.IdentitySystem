import {Component, inject, Input, OnInit, ViewChild} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {NzModalRef, NZ_MODAL_DATA} from "ng-zorro-antd/modal";
import {NzTreeComponent, NzTreeNode} from "ng-zorro-antd/tree";
import {ComponentBase} from "../../../base/component.base";
import {NzNode} from "../../../values/structs/nz-node";
import {Authority} from "../../../models/authority";
import {AuthorityMap} from "../../../maps/authority.map";
import {AuthorityService} from "../../../services/authority.service";
import {RoleService} from "../../../services/role.service";

/*角色修改组件*/
@Component({
    selector: 'app-role-update',
    templateUrl: './update.component.html',
    styleUrls: ['./update.component.css']
})
export class UpdateComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*对话框数据*/
    private readonly _modalData = inject(NZ_MODAL_DATA);

    /*表单建造者*/
    private readonly _formBuilder: UntypedFormBuilder;

    /*权限服务*/
    private readonly _authorityService: AuthorityService;

    /*角色服务*/
    private readonly _roleService: RoleService;

    /**
     * 创建角色修改组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: UntypedFormBuilder, authorityService: AuthorityService, roleService: RoleService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._authorityService = authorityService;
        this._roleService = roleService;
    }

    //endregion

    //region # 属性

    /*NZ树组件*/
    @ViewChild('nzTreeComponent', {static: false})
    public nzTreeComponent!: NzTreeComponent;

    /*角色Id*/
    @Input()
    public roleId: string = "";

    /*信息系统编号*/
    @Input()
    public infoSystemNo: string = "";

    /*信息系统名称*/
    @Input()
    public infoSystemName: string = "";

    /*角色名称*/
    @Input()
    public roleName: string = "";

    /*描述*/
    @Input()
    public description: string | null = null;

    /*权限树*/
    public authorityTree: Array<NzNode> = new Array<NzNode>();

    /*勾选权限Id集*/
    public checkedAuthorityIds: Array<string> = new Array<string>();

    /*表单*/
    public formGroup!: UntypedFormGroup;

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— async ngOnInit()
    /**
     * 初始化组件
     * */
    public async ngOnInit(): Promise<void> {
        //初始化表单
        this.roleId = this._modalData.roleId;
        this.infoSystemNo = this._modalData.infoSystemNo;
        this.infoSystemName = this._modalData.infoSystemName;
        this.roleName = this._modalData.roleName;
        this.description = this._modalData.description;
        this.formGroup = this._formBuilder.group({
            infoSystemName: [null],
            roleName: [null, [Validators.required]],
            description: [null]
        });

        //加载权限树
        await this.loadAuthorityTree();
    }
    //endregion


    //Actions

    //region 提交 —— async submit()
    /**
     * 提交
     * */
    public async submit(): Promise<void> {
        for (let index in this.formGroup.controls) {
            this.formGroup.controls[index].markAsDirty();
            this.formGroup.controls[index].updateValueAndValidity();
        }

        if (this.formGroup.valid) {
            this.busy();

            //获取勾选节点
            let checkedNodes: Array<NzTreeNode> = this.nzTreeComponent.getCheckedNodeList();
            let checkedAuthorityIds: Array<string> = checkedNodes.map(x => x.key);

            let promise: Promise<void> = this._roleService.updateRole(this.roleId, this.roleName, this.description, checkedAuthorityIds);
            promise.catch(_ => {
                this.idle();
            });
            await promise;

            this.idle();
            this._modalRef.close(true);
        }
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

    //region 加载权限树 —— async loadAuthorityTree()
    /**
     * 加载权限树
     * */
    private async loadAuthorityTree(): Promise<void> {
        let authorities: Array<Authority> = await this._authorityService.getAuthorities(null, this.infoSystemNo, null, null, null);
        let roleAuthorities: Array<Authority> = await this._authorityService.getAuthorities(null, this.infoSystemNo, null, null, this.roleId);

        let authorityTree = AuthorityMap.toNzTree(authorities);
        let checkedKeys: Array<string> = new Array<string>();
        for (let applicationTypeNode of authorityTree) {
            for (const authorityNode of applicationTypeNode.children) {
                let exists: boolean = roleAuthorities.some(x => x.id == authorityNode.key);
                if (exists) {
                    checkedKeys.push(authorityNode.key);
                }
            }
        }

        this.authorityTree = authorityTree;
        this.checkedAuthorityIds = checkedKeys;
    }
    //endregion

    //endregion
}
