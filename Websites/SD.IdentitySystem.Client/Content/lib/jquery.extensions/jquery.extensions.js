(function (object) {
    object.extend(object, {
        global: {
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
