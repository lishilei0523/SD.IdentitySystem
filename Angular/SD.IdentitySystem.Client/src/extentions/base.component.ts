/*组件基类*/
export abstract class BaseComponent {
    /*是否繁忙*/
    public isBusy: boolean;

    protected constructor() {
        this.isBusy = false;
    }

    public busy(): void {
        this.isBusy = true;
    }

    public idle(): void {
        this.isBusy = false;
    }
}
