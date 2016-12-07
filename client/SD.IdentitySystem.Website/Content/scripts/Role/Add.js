$(function () {
    //获取用户选中的信息系统编号
    var systemNo = $("#slSystem").val();

    //初始化权限树
    initAuthorityTree(systemNo);
});

//初始化权限树
function initAuthorityTree(systemNo) {
    $("#authorityTree").tree({
        url: "/Authority/GetAuthorityTree/" + systemNo,
        animate: true,
        lines: true,
        checkbox: function (node) {
            if (node.attributes.type === "infoSystem") {
                return false;
            }
            else {
                return true;
            }
        }
    });
}

//信息系统选中事件
function systemSelected(record) {
    //重新初始化权限树
    initAuthorityTree(record.value);
}

//创建角色
function createRole() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmCreateRole"));

    //获取被选中的权限树节点
    var nodes = $("#authorityTree").tree("getChecked");
    var authorityIds = [];

    //填充至权限数组
    for (var i = 0; i < nodes.length; i++) {
        authorityIds.push(nodes[i].id);
    }

    //构造参数模型
    var data = $.global.appendArray(form, authorityIds, "authorityIds");

    $.ajax({
        type: "POST",
        url: "/Role/CreateRole",
        data: data,
        success: function () {
            $.easyuiExt.messager.alert("OK", "创建成功！");

            $.easyuiExt.updateGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.messager.alert("Error", error.responseText);
        }
    });
}