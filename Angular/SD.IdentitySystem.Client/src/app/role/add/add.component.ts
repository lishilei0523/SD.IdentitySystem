import {Component, inject, Input, OnInit, ViewChild} from '@angular/core';
import {UntypedFormBuilder, UntypedFormGroup, Validators} from "@angular/forms";
import {NzModalRef, NZ_MODAL_DATA} from "ng-zorro-antd/modal";
import {NzTreeComponent, NzTreeNode} from "ng-zorro-antd/tree";
import {ComponentBase} from "../../../base/component.base";
import {NzNode} from "../../../values/structs/nz-node";
import {InfoSystem} from "../../../models/info-system";
import {Authority} from "../../../models/authority";
import {AuthorityMap} from "../../../maps/authority.map";
import {AuthorityService} from "../../../services/authority.service";
import {RoleService} from "../../../services/role.service";

/*角色创建组件*/
@Component({
    selector: 'app-role-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css'],
    standalone: false
})
export class AddComponent extends ComponentBase implements OnInit {

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
     * 创建角色创建组件构造器
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

    /*角色名称*/
    public roleName: string = "";

    /*描述*/
    public description: string | null = null;

    /*信息系统列表*/
    @Input()
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统*/
    public selectedInfoSystemNo: string | null = null;

    /*权限树*/
    public authorityTree: Array<NzNode> = new Array<NzNode>();

    /*表单*/
    public formGroup!: UntypedFormGroup;

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— ngOnInit()
    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        //初始化表单
        this.infoSystems = this._modalData.infoSystems;
        this.formGroup = this._formBuilder.group({
            selectedInfoSystemNo: [null, [Validators.required]],
            roleName: [null, [Validators.required]],
            description: [null]
        });
    }
    //endregion


    //Actions

    //region 加载权限树 —— async loadAuthorityTree()
    /**
     * 加载权限树
     * */
    public async loadAuthorityTree(): Promise<void> {
        if (!this.selectedInfoSystemNo) {
            this.authorityTree = new Array<NzNode>();
            return;
        }

        this.busy();

        let authorities: Array<Authority> = await this._authorityService.getAuthorities(null, this.selectedInfoSystemNo, null, null, null);
        this.authorityTree = AuthorityMap.toNzTree(authorities);

        this.idle();
    }
    //endregion

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

            let promise: Promise<void> = this._roleService.createRole(this.selectedInfoSystemNo!, this.roleName, this.description, checkedAuthorityIds);
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

    //endregion
}
