//修改权限
function updateAuthority() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmUpdateAuthority"));

    $.ajax({
        type: "POST",
        url: "/Authority/UpdateAuthority",
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
