/*组件基类*/
export abstract class BaseComponent {

    /*是否繁忙*/
    public isBusy: boolean;

    /*是否全选*/
    public checkedAll = false;

    /*选中项列表*/
    public checkedIds: Set<string>;

    /**
     * 无参构造器
     * */
    protected constructor() {
        this.isBusy = false;
        this.checkedIds = new Set<string>();
    }

    /**
     * 繁忙
     * */
    public busy(): void {
        this.isBusy = true;
    }

    /**
     * 空闲
     * */
    public idle(): void {
        this.isBusy = false;
    }
}
