$(function () {
    //获取用户登录名
    var loginId = $("#txtLoginId").val();

    //初始化角色树
    initRoleTree(loginId);
});

//初始化信息系统/角色树
function initRoleTree(loginId) {
    $("#roleTree").tree({
        url: "/Role/GetRoleTree/" + loginId,
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

//分配角色
function setRoles() {
    //获取被选中的角色树节点
    var nodes = $("#roleTree").tree("getChecked");
    var roleIds = [];

    //填充至角色Id数组
    for (var i = 0; i < nodes.length; i++) {
        roleIds.push(nodes[i].id);
    }

    ////构造参数模型
    var loginId = $("#txtLoginId").val();
    var form = { loginId: loginId };
    var data = $.global.appendArray(form, roleIds, "roleIds");

    $.ajax({
        type: "POST",
        url: "/User/SetRoles",
        data: data,
        success: function () {
            $.easyuiExt.messager.alert("OK", "分配成功！");

            $.easyuiExt.updateGridInTab();
            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}