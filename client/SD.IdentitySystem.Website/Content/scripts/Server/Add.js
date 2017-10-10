//创建服务器
function createServer() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmCreateServer"));

    $.ajax({
        type: "POST",
        url: "/Server/CreateServer",
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