import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {NzModalRef} from "ng-zorro-antd/modal";
import {ApplicationType, ApplicationTypeDescriptor, ComponentBase} from "sd-infrastructure";
import {InfoSystem} from "../../../models/info-system";
import {AuthorityService} from "../../../services/authority.service";

/*权限创建组件*/
@Component({
    selector: 'app-authority-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent extends ComponentBase implements OnInit {

    //region # 字段及构造器

    /*对话框引用*/
    private readonly _modalRef: NzModalRef;

    /*表单建造者*/
    private readonly _formBuilder: FormBuilder;

    /*权限服务*/
    private readonly _authorityService: AuthorityService;

    /**
     * 创建权限创建组件构造器
     * */
    public constructor(modalRef: NzModalRef, formBuilder: FormBuilder, authorityService: AuthorityService) {
        super();
        this._modalRef = modalRef;
        this._formBuilder = formBuilder;
        this._authorityService = authorityService;
    }

    //endregion

    //region # 属性

    /*权限名称*/
    public authorityName: string = "";

    /*权限路径*/
    public authorityPath: string = "";

    /*描述*/
    public description: string | null = null;

    /*信息系统列表*/
    @Input()
    public infoSystems: Array<InfoSystem> = new Array<InfoSystem>();

    /*已选信息系统*/
    public selectedInfoSystemNo: string | null = null;

    /*应用程序类型字典*/
    @Input()
    public applicationTypes: Set<{ key: ApplicationType, value: string }> = ApplicationTypeDescriptor.getEnumMembers();

    /*已选应用程序类型*/
    public selectedApplicationType: ApplicationType | null = null;

    /*表单*/
    public formGroup!: FormGroup;

    //endregion

    //region # 方法

    //Initializations

    //region 初始化组件 —— ngOnInit()
    /**
     * 初始化组件
     * */
    public ngOnInit(): void {
        //初始化表单
        this.formGroup = this._formBuilder.group({
            authorityName: [null, [Validators.required]],
            authorityPath: [null, [Validators.required]],
            description: [null],
            selectedInfoSystemNo: [null, [Validators.required]],
            selectedApplicationType: [null, [Validators.required]],
        });
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

            let promise: Promise<void> = this._authorityService.createAuthority(this.selectedInfoSystemNo!, this.selectedApplicationType!, this.authorityName, this.authorityPath, this.description);
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
