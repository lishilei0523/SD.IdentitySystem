//初始化信息系统
function initInfoSystem() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmInitInfoSystem"));

    $.ajax({
        type: "POST",
        url: "/InfoSystem/InitInfoSystem",
        data: form,
        success: function () {
            $.easyuiExt.messager.alert("OK", "初始化成功！");

            $.easyuiExt.updateGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}
