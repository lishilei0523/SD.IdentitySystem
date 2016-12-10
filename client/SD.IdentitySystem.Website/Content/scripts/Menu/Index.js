//定义全局变量$treeGrid，用于刷新treeGrid
var $treeGrid = null;

//DOM加载事件
$(function () {
    //加载列表
    searchMenus();
});

//获取菜单TreeGrid
function getMenus(queryParams) {
    //指定url
    $.easyuiExt.treegrid.url = "/Menu/GetMenuTreeGrid";

    //定义列
    $.easyuiExt.treegrid.columns = [
        [
            { field: "Id", title: "Id", hidden: true },
            { field: "Name", title: "菜单名称", halign: "center", width: 200 },
            { field: "Url", title: "链接地址", halign: "center", width: 200 },
            { field: "SystemName", title: "所属系统", halign: "center", width: 150 },
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
                    var start = '<a class="aLink" href="javascript: updateMenu(\'';
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
                    var start = '<a class="aLink" href="javascript: removeMenu(\'';
                    var end = '\');" >删除</a>';
                    var element = start + row.Id + end;

                    return element;
                }
            }
        ]
    ];

    //勾选框
    $.easyuiExt.treegrid.checkbox = function (row) {
        return row.IsLeaf === true;
    }

    //footer
    $.easyuiExt.treegrid.footer = "#footer";

    //初始化工具栏
    $.easyuiExt.treegrid.toolbarId = "#toolbar";

    //绑定参数模型
    $.easyuiExt.treegrid.queryParams = queryParams;

    //绑定$treeGrid
    $treeGrid = $("#treeGrid").treegrid($.easyuiExt.treegrid);
}

//创建菜单
function createMenu() {
    $.easyuiExt.showWindow("创建菜单", "/Menu/Add", 480, 468);
}

//修改菜单
function updateMenu(menuId) {
    $.easyuiExt.showWindow("修改菜单", "/Menu/Update/" + menuId, 480, 468);
}

//删除菜单
function removeMenu(menuId) {
    $.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/Menu/RemoveMenu/" + menuId,
                success: function () {
                    $.easyuiExt.messager.alert("OK", "删除成功！");
                    $.easyuiExt.updateTreeGridInTab();
                },
                error: function (error) {
                    $.easyuiExt.messager.alert("Error", error.responseText);
                }
            });
        }
    });
}

//删除选中菜单
function removeMenus() {
    //获取所有的选中行
    var checkedRows = $("#treeGrid").treegrid("getCheckedNodes");

    //判断菜单有没有选中
    if (checkedRows.length > 0) {
        $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
            if (confirm) {
                //填充菜单Id数组
                var checkedMenuIds = [];
                for (var i = 0; i < checkedRows.length; i++) {
                    checkedMenuIds.push(checkedRows[i].Id);
                }

                //JSON格式转换
                var params = $.global.appendArray(null, checkedMenuIds, "menuIds");

                $.ajax({
                    type: "POST",
                    url: "/Menu/RemoveMenus",
                    data: params,
                    success: function () {
                        $.easyuiExt.messager.alert("OK", "删除成功！");
                        $.easyuiExt.updateTreeGridInTab();
                    },
                    error: function (error) {
                        $.easyuiExt.messager.alert("Error", error.responseText);
                    }
                });
            }
        });
    }
    else {
        $.easyuiExt.messager.alert("警告", "请选中要删除的菜单！");
    }
}

//搜索
function searchMenus() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getMenus(queryParams);
}