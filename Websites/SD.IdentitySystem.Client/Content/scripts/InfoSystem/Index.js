//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchInfoSystems();
});

//加载信息系统列表
function getInfoSystems(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/InfoSystem/GetInfoSystemsByPage";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "Number", title: "信息系统编号", halign: "center", width: 120 },
            { field: "Name", title: "信息系统名称", halign: "center", width: 120 },
            { field: "ApplicationTypeName", title: "应用程序类型名称", halign: "center", width: 180 },
            { field: "AdminLoginId", title: "管理员用户名", halign: "center", width: 120 },
            { field: "Host", title: "主机名", halign: "center", width: 120 },
            { field: "Port", title: "端口", halign: "center", width: 120 },
            { field: "Index", title: "首页", halign: "center", width: 120 },
            {
                field: "AddedTime",
                title: "创建时间",
                align: "center",
                halign: "center",
                width: 150,
                formatter: function (value) {
                    return $.global.formatDate(value, "yyyy-MM-dd hh:mm:ss");
                }
            },
            {
                field: "Init",
                title: "初始化",
                width: 50,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: initInfoSystem(\'';
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
function createInfoSystem() {
    $.easyuiExt.showWindow("创建信息系统", "/InfoSystem/Add", 480, 368);
}

//初始化信息系统
function initInfoSystem(infoSystemNo) {
    $.easyuiExt.showWindow("初始化信息系统", "/InfoSystem/Init/" + infoSystemNo, 480, 368);
}

//搜索
function searchInfoSystems() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getInfoSystems(queryParams);
}