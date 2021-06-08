import {formatDate} from "@angular/common";

/*通用工具*/
export class Common {

    /**
     * 格式化日期时间
     * @param dateTime - 日期时间
     * @return 格式化日期时间
     * */
    public static formatDate(dateTime: string | null | undefined): string {
        if (dateTime) {
            return formatDate(dateTime, "yyyy-MM-dd HH:mm", "zh-cn");
        }
        return "";
    }
}
