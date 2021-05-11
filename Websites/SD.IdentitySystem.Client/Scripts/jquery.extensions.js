(function (object) {
    object.extend(object, {
        global: {
            //JSON时间格式化
            formatDate: function (timestamp, format) {
                //去除前缀得到毫秒数
                var milliseconds = parseInt(timestamp.replace(/\D/igm, ""));

                //实例化一个新的日期格式，使用1970 年 1 月 1 日至今的毫秒数为参数
                var date = new Date(milliseconds);

                var dateMember = {
                    "M+": date.getMonth() + 1, //月份 
                    "d+": date.getDate(), //日 
                    "h+": date.getHours(), //小时 
                    "m+": date.getMinutes(), //分 
                    "s+": date.getSeconds(), //秒 
                    "q+": Math.floor((date.getMonth() + 3) / 3), //季度 
                    "S": date.getMilliseconds() //毫秒 
                };

                if (/(y+)/.test(format)) {
                    format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
                }

                for (var member in dateMember)
                    if (dateMember.hasOwnProperty(member)) {
                        if (new RegExp("(" + member + ")").test(format)) {
                            format =
                                format.replace(RegExp.$1, (RegExp.$1.length === 1) ?
                                (dateMember[member]) :
                                (("00" + dateMember[member]).substr(("" + dateMember[member]).length)));
                        }
                    }

                return format;
            },

            //计算相对宽度
            getRelativeWidth: function (percent, ctrl) {
                return ctrl.width() * percent / 100;
            },

            //格式化表单
            formatForm: function (form) {
                var formArray = form.serializeArray();
                var formJson = {};

                for (var i = 0; i < formArray.length; i++) {
                    formJson[formArray[i].name] = formArray[i].value;
                }

                return formJson;
            },

            //追加数组至JSON对象
            appendArray: function (json, array, name) {
                var object = json == null ? {} : json;

                for (var i = 0; i < array.length; i++) {
                    object[name + "[" + i + "]"] = array[i];
                }

                return object;
            }
        }
    });
})($);
