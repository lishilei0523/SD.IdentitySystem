$(function () {
    //初始化菜单树
    initMenuTree(null, null);
});

//初始化菜单树
function initMenuTree(infoSystemNo, applicationType) {
    $("#slMenu").combotree({
        url: "/Menu/GetMenuTree?infoSystemNo=" + infoSystemNo + "&applicationType=" + applicationType,
        animate: true,
        lines: true,
        checkbox: true,
        required: false,
        width: 210
    });
}

//信息系统选中事件
function infoSystemSelected() {
    //获取用户选中的信息系统编号与应用程序类型
    var infoSystemNo = $("#cbxInfoSystem").combobox("getValue");
    var applicationType = $("#slApplicationType").combobox("getValue");

    //初始化菜单树
    initMenuTree(infoSystemNo, applicationType);
}

//应用程序类型选中事件
function applicationTypeSelected() {
    //获取用户选中的信息系统编号与应用程序类型
    var infoSystemNo = $("#cbxInfoSystem").combobox("getValue");
    var applicationType = $("#slApplicationType").combobox("getValue");

    //初始化菜单树
    initMenuTree(infoSystemNo, applicationType);
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