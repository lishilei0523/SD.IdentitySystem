//创建信息系统
function createSystem() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmCreateSystem"));

    $.ajax({
        type: "POST",
        url: "/InfoSystem/CreateInfoSystem",
        data: form,
        success: function () {
            $.easyuiExt.messager.alert("OK", "创建成功！");

            $.easyuiExt.updateGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}