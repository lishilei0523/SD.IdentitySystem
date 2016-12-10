$(function () {
    //获取菜单Id
    var menuId = $("#txtMenuId").val();

    //初始化权限树
    initAuthorityTree(menuId);
});

//初始化权限树
function initAuthorityTree(menuId) {
    $("#authorityTree").tree({
        url: "/Authority/GetAuthorityTreeByMenu/" + menuId,
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

//关联权限
function relateAuthorities() {
    //获取被选中的权限树节点
    var nodes = $("#authorityTree").tree("getChecked");
    var authorityIds = [];

    //填充至权限数组
    for (var i = 0; i < nodes.length; i++) {
        authorityIds.push(nodes[i].id);
    }

    //构造参数模型
    var menuId = $("#txtMenuId").val();
    var json = { menuId: menuId };
    var data = $.global.appendArray(json, authorityIds, "authorityIds");

    $.ajax({
        type: "POST",
        url: "/Menu/RelateAuthorities",
        data: data,
        success: function () {
            $.easyuiExt.messager.alert("OK", "关联成功！");

            $.easyuiExt.closeWindow();
        },
        error: function (error) {
            $.easyuiExt.messager.alert("Error", error.responseText);
        }
    });
}