import {Pipe, PipeTransform} from '@angular/core';
import {ApplicationType} from './application-type';

/*应用程序类型枚举描述器*/
@Pipe({
    name: 'applicationTypeDescriptor'
})
export class ApplicationTypeDescriptor implements PipeTransform {

    /**
     * 转换枚举描述
     * */
    public transform(applicationType: ApplicationType): string {
        switch (applicationType) {
            case ApplicationType.Web:
                return "Web应用程序";
            case ApplicationType.Windows:
                return "Windows应用程序";
            case ApplicationType.Android:
                return "Android应用程序";
            case ApplicationType.IOS:
                return "IOS应用程序";
            case ApplicationType.WindowsPhone:
                return "Windows Phone应用程序";
            case ApplicationType.Complex:
                return "复合应用程序";
        }
    }
}
