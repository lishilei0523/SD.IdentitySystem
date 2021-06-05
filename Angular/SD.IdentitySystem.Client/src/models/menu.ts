import {ApplicationType} from "../enums/applicationType";

/*菜单*/
export class Menu {
    /*信息系统编号*/
    public systemNo: string;

    /*信息系统名称*/
    public systemName: string;

    /*应用程序类型*/
    public applicationType;

    /*菜单Id*/
    public id: string;

    /*菜单名称*/
    public name: string;

    /*链接地址*/
    public url: string;

    /*路径*/
    public path: string;

    /*图标*/
    public icon: string;

    /*排序*/
    public sort: number;

    /*层次*/
    public level: number;

    /*是否是根级节点*/
    public isRoot: boolean;

    /*是否是叶子级节点*/
    public isLeaf: boolean;

    /*上级菜单Id*/
    public parentMenuId: string;

    /*上级菜单*/
    public parentNode: Menu | null;

    /*下级菜单集*/
    public subNodes: Array<Menu>;

    /**
     * 创建菜单构造器
     * */
    public constructor(systemNo: string, systemName: string, applicationType: ApplicationType, menuId: string, menuName: string, url: string, path: string, icon: string, sort: number, level: number, isRoot: boolean, isLeaf: boolean, parentMenuId: string, parentNode: Menu | null) {
        this.systemNo = systemNo;
        this.systemName = systemName;
        this.applicationType = applicationType;
        this.id = menuId;
        this.name = menuName;
        this.url = url;
        this.path = path;
        this.icon = icon;
        this.sort = sort;
        this.level = level;
        this.isRoot = isRoot;
        this.isLeaf = isLeaf;
        this.parentMenuId = parentMenuId;
        this.parentNode = parentNode;
        this.subNodes = new Array<Menu>();
        if (parentNode != null) {
            parentNode.subNodes.push(this);
        }
    }
}
