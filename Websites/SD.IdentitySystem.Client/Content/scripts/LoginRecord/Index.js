//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchRecords();
});

//加载用户列表
function getRecords(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/LoginRecord/GetLoginRecordsByPage";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "ck", checkbox: true, halign: "center" },
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "LoginId", title: "用户名", halign: "center", width: 100 },
            { field: "RealName", title: "真实姓名", halign: "center", width: 120 },
            { field: "IP", title: "IP地址", halign: "center", width: 120 },
            { field: "ClientId", title: "客户端Id", halign: "center", width: 200 },
            {
                field: "AddedTime",
                title: "登录时间",
                align: "center",
                halign: "center",
                width: 150,
                formatter: function (value) {
                    return $.global.formatDate(value, "yyyy-MM-dd hh:mm:ss");
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

//搜索
function searchRecords() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getRecords(queryParams);
}
