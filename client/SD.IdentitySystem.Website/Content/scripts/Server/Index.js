//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchServers();
});

//加载服务器列表
function getServers(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/Server/GetServers";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "ck", checkbox: true, halign: "center" },
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "Name", title: "主机名", halign: "center", width: 120 },
            {
                field: "ServiceOverDate",
                title: "服务停止日期",
                align: "center",
                halign: "center",
                width: $.global.getRelativeWidth(12, $("#dvGrid")),
                formatter: function (value) {
                    return $.global.formatDate(value, "yyyy-MM-dd");
                }
            },
            {
                field: "Update",
                title: "设置",
                width: 35,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: updateServer(\'';
                    var end = '\');" >设置</a>';
                    var element = start + row.Id + end;

                    return element;
                }
            },
            {
                field: "Remove",
                title: "删除",
                width: 35,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: removeServer(\'';
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

//创建服务器
function createServer() {
    $.easyuiExt.showWindow("创建服务器", "/Server/Add", 360, 260);
}

//修改服务器
function updateServer(serverId) {
    $.easyuiExt.showWindow("修改服务器", "/Server/Update/" + serverId, 360, 260);
}

//删除服务器
function removeServer(serverId) {
    $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/Server/RemoveServer/" + serverId,
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

//删除选中服务器
function removeServers() {
    //获取所有的选中行
    var checkedRows = $("#tbGrid").datagrid("getChecked");

    //判断服务器有没有选中
    if (checkedRows.length > 0) {
        $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
            if (confirm) {
                //填充服务器Id数组
                var checkedServerIds = [];
                for (var i = 0; i < checkedRows.length; i++) {
                    checkedServerIds.push(checkedRows[i].Id);
                }

                //JSON格式转换
                var params = $.global.appendArray(null, checkedServerIds, "serverIds");

                $.ajax({
                    type: "POST",
                    url: "/Server/RemoveServers",
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
        $.easyuiExt.messager.alert("Warning", "请选中要删除的服务器！");
    }
}

//搜索
function searchServers() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getServers(queryParams);
}