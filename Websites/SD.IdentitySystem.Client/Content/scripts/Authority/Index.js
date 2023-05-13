//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchAuthorities();
});

//加载权限列表
function getAuthorities(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/Authority/GetAuthoritiesByPage";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "ck", checkbox: true, halign: "center" },
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "Name", title: "权限名称", halign: "center", width: 120 },
            { field: "AuthorityPath", title: "权限路径", halign: "center", width: 300 },
            { field: "InfoSystemName", title: "所属系统", halign: "center", width: 120 },
            { field: "ApplicationTypeName", title: "应用程序类型", halign: "center", width: 150 },
            { field: "Description", title: "描述", halign: "center", width: 200 },
            { field: "AddedTime", title: "创建时间", align: "center", halign: "center", width: 150 },
            {
                field: "Update",
                title: "编辑",
                width: 35,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: updateAuthority(\'';
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
                    var start = '<a class="aLink" href="javascript: removeAuthority(\'';
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

//创建权限
function createAuthority() {
    $.easyuiExt.showWindow("创建权限", "/Authority/Add", 450, 363);
}

//修改权限
function updateAuthority(authorityId) {
    $.easyuiExt.showWindow("修改权限", "/Authority/Update/" + authorityId, 450, 363);
}

//删除权限
function removeAuthority(authorityId) {
    $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/Authority/RemoveAuthority/" + authorityId,
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

//删除选中权限
function removeAuthorities() {
    //获取所有的选中行
    var checkedRows = $("#tbGrid").datagrid("getChecked");

    //判断权限有没有选中
    if (checkedRows.length > 0) {
        $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
            if (confirm) {
                //填充权限Id数组
                var checkedAuthorityIds = [];
                for (var i = 0; i < checkedRows.length; i++) {
                    checkedAuthorityIds.push(checkedRows[i].Id);
                }

                //JSON格式转换
                var params = $.global.appendArray(null, checkedAuthorityIds, "authorityIds");

                $.ajax({
                    type: "POST",
                    url: "/Authority/RemoveAuthorities",
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
        $.easyuiExt.messager.alert("Warning", "请选中要删除的权限！");
    }
}

//搜索
function searchAuthorities() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getAuthorities(queryParams);
}
