//修改服务器
function updateServer() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmUpdateServer"));

    $.ajax({
        type: "POST",
        url: "/Server/UpdateServer",
        data: form,
        success: function () {
            $.easyuiExt.messager.alert("OK", "修改成功！");

            $.easyuiExt.updateGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}