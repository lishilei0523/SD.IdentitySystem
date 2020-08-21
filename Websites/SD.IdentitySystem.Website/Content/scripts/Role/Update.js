$(function () {
    //获取当前角色Id
    var roleId = $("#txtRoleId").val();

    //初始化权限树
    initAuthorityTree(roleId);
});

//初始化权限树
function initAuthorityTree(roleId) {
    $("#authorityTree").tree({
        url: "/Authority/GetAuthorityTreeByRole/" + roleId,
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

//修改角色
function updateRole() {
    //获取表单JSON
    var form = $.global.formatForm($("#frmUpdateRole"));

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
        url: "/Role/UpdateRole",
        data: data,
        success: function () {
            $.easyuiExt.messager.alert("OK", "修改成功！");

            $.easyuiExt.updateGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.messager.alert("Error", error.responseText);
        }
    });
}