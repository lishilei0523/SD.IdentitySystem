//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchRoles();
});

//加载角色列表
function getRoles(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/Role/GetRoles";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "ck", checkbox: true, halign: "center" },
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "Name", title: "角色名称", halign: "center", width: 120 },
            { field: "SystemName", title: "所属系统", halign: "center", width: 120 },
            { field: "Description", title: "角色描述", halign: "center", width: 200 },
            {
                field: "AddedTime",
                title: "创建时间",
                align: "center",
                halign: "center",
                width: $.global.getRelativeWidth(15, $("#dvGrid")),
                formatter: function (value) {
                    return $.global.formatDate(value, "yyyy-MM-dd hh:mm:ss");
                }
            },
            {
                field: "Update",
                title: "编辑",
                width: 35,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: updateRole(\'';
                    var end = '\');" >编辑</a>';
                    var element = start + row.Id + end;

                    return element;
                }
            },
            {
                field: "Remove",
                title: "删除",
                width: 35,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: removeRole(\'';
                    var end = '\');" >删除</a>';
                    var element = start + row.Id + end;

                    return element;
                }
            }
        ]
    ];

    //初始化工具栏
    $.easyuiExt.datagrid.toolbarId = "#toolbar";

    //绑定参数模型
    $.easyuiExt.datagrid.queryParams = queryParams;

    //绑定$tbGrid
    $tbGrid = $("#tbGrid").datagrid($.easyuiExt.datagrid);
}

//创建角色
function createRole() {
    $.easyuiExt.showWindow("创建角色", "/Role/Add", 840, 680);
}

//修改角色
function updateRole(roleId) {
    $.easyuiExt.showWindow("修改角色", "/Role/Update/" + roleId, 840, 680);
}

//删除角色
function removeRole(roleId) {
    $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/Role/RemoveRole/" + roleId,
                success: function () {
                    $.easyuiExt.messager.alert("OK", "删除成功！");
                    $.easyuiExt.updateGridInTab();
                },
                error: function (error) {
                    $.easyuiExt.messager.alert("Error", error.responseText);
                }
            });
        }
    });
}

//删除选中角色
function removeRoles() {
    //获取所有的选中行
    var checkedRows = $("#tbGrid").datagrid("getChecked");

    //判断角色有没有选中
    if (checkedRows.length > 0) {
        $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
            if (confirm) {
                //填充角色Id数组
                var checkedRoleIds = [];
                for (var i = 0; i < checkedRows.length; i++) {
                    checkedRoleIds.push(checkedRows[i].Id);
                }

                //JSON格式转换
                var params = $.global.appendArray(null, checkedRoleIds, "roleIds");

                $.ajax({
                    type: "POST",
                    url: "/Role/RemoveRoles",
                    data: params,
                    success: function () {
                        $.easyuiExt.messager.alert("OK", "删除成功！");
                        $.easyuiExt.updateGridInTab();
                    },
                    error: function (error) {
                        $.easyuiExt.messager.alert("Error", error.responseText);
                    }
                });
            }
        });
    }
    else {
        $.easyuiExt.messager.alert("警告", "请选中要删除的角色！");
    }
}

//搜索
function searchRoles() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getRoles(queryParams);
}