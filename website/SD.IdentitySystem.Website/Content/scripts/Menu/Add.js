$(function () {
    //获取用户选中的信息系统编号
    var systemNo = $("#slSystem").val();
    var applicationType = $("#slApplicationType").val();

    //初始化菜单树
    initMenuTree(systemNo, applicationType);

});

//初始化菜单树
function initMenuTree(systemNo, applicationType) {
    $("#slMenu").combotree({
        url: "/Menu/GetMenuTree?systemNo=" + systemNo + "&applicationType=" + applicationType,
        animate: true,
        lines: true,
        checkbox: true,
        required: true,
        width: 210
    });
}

//信息系统选中事件
function systemSelected(record) {
    //重新初始化菜单树
    initMenuTree(record.value);
}

//创建菜单
function createMenu() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmCreateMenu"));

    $.ajax({
        type: "POST",
        url: "/Menu/CreateMenu",
        data: form,
        success: function () {
            $.easyuiExt.messager.alert("OK", "创建成功！");

            $.easyuiExt.updateTreeGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}