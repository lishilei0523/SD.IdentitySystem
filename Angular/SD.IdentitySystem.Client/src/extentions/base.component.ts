/*组件基类*/
export abstract class BaseComponent {

    /*是否繁忙*/
    public isBusy: boolean;

    /**
     * 无参构造器
     * */
    protected constructor() {
        this.isBusy = false;
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
