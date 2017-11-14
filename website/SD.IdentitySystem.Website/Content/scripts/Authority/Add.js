//创建权限
function createAuthority() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmCreateAuthority"));

    $.ajax({
        type: "POST",
        url: "/Authority/CreateAuthority",
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