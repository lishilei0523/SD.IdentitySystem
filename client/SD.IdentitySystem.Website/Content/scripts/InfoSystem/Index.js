//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchSystems();
});

//加载信息系统列表
function getSystems(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/InfoSystem/GetInfoSystems";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "Name", title: "信息系统名称", halign: "center", width: 120 },
            { field: "ApplicationTypeName", title: "应用程序类型名称", halign: "center", width: 120 },
            { field: "AdminLoginId", title: "管理员登录名", halign: "center", width: 120 },
            { field: "Host", title: "主机名", halign: "center", width: 120 },
            { field: "Port", title: "端口", halign: "center", width: 120 },
            { field: "Index", title: "首页", halign: "center", width: 120 },
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
                field: "Init",
                title: "初始化",
                width: 50,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: initSystem(\'';
                    var end = '\');" >初始化</a>';
                    var element = start + row.Number + end;

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

//创建信息系统
function createSystem() {
    $.easyuiExt.showWindow("创建信息系统", "/InfoSystem/Add", 480, 368);
}

//初始化信息系统
function initSystem(systemNo) {
    $.easyuiExt.showWindow("初始化信息系统", "/InfoSystem/Init/" + systemNo, 480, 368);
}

//搜索
function searchSystems() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getSystems(queryParams);
}