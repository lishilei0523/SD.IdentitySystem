//修改菜单
function updateMenu() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmUpdateMenu"));

    $.ajax({
        type: "POST",
        url: "/Menu/UpdateMenu",
        data: form,
        success: function () {
            $.easyuiExt.messager.alert("OK", "修改成功！");

            $.easyuiExt.updateTreeGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}
