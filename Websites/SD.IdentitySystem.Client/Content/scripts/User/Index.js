//定义全局变量$tbGrid，用于刷新Grid
var $tbGrid = null;

//DOM初始化事件
$(function () {
    //加载列表
    searchUsers();
});

//加载用户列表
function getUsers(queryParams) {
    //指定url
    $.easyuiExt.datagrid.url = "/User/GetUsersByPage";

    //定义列
    $.easyuiExt.datagrid.columns = [
        [
            { field: "ck", checkbox: true, halign: "center" },
            { field: "Id", title: "Id", halign: "center", hidden: true },
            { field: "Number", title: "用户名", halign: "center", width: 100 },
            { field: "Name", title: "真实姓名", halign: "center", width: 150 },
            { field: "PrivateKey", title: "私钥", halign: "center", width: 300 },
            { field: "Status", title: "状态", align: "center", halign: "center", width: 45 },
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
                field: "ResetPassord",
                title: "重置密码",
                width: 70,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: resetPassord(\'';
                    var end = '\');" >重置密码</a>';
                    var element = start + row.Number + end;

                    return element;
                }
            },
            {
                field: "ResetPrivateKey",
                title: "重置私钥",
                width: 70,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: resetPrivateKey(\'';
                    var end = '\');" >重置私钥</a>';
                    var element = start + row.Number + end;

                    return element;
                }
            },
            {
                field: "Enabled",
                title: "停/启用",
                align: "center",
                halign: "center",
                width: 50,
                formatter: function (value, row) {
                    var start =
                        value === true ?
                            '<a class="aLink" href="javascript: disableUser(\'' :
                            '<a class="aLink" href="javascript: enableUser(\'';
                    var end =
                        value === true ?
                            '\');" >停用</a>' :
                            '\');" >启用</a>';

                    var element = start + row.Number + end;

                    return element;
                }
            },
            {
                field: "SetRole",
                title: "分配角色",
                width: 60,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: setRole(\'';
                    var end = '\');" >分配角色</a>';
                    var element = start + row.Number + end;

                    return element;
                }
            },
            {
                field: "Remove",
                title: "删除",
                width: 35,
                formatter: function (value, row) {
                    var start = '<a class="aLink" href="javascript: removeUser(\'';
                    var end = '\');" >删除</a>';
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

//创建用户
function createUser() {
    $.easyuiExt.showWindow("创建用户", "/User/Add", 360, 260);
}

//重置密码
function resetPassord(loginId) {
    $.easyuiExt.showWindow("重置密码", "/User/ResetPassword/" + loginId, 360, 230);
}

//重置私钥
function resetPrivateKey(loginId) {
    $.easyuiExt.showWindow("重置私钥", "/User/ResetPrivateKey/" + loginId, 430, 200);
}

//分配角色
function setRole(loginId) {
    $.easyuiExt.showWindow("分配角色", "/User/SetRole/" + loginId, 480, 642);
}

//删除用户
function removeUser(loginId) {
    $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/User/RemoveUser/" + loginId,
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

//删除选中用户
function removeUsers() {
    //获取所有的选中行
    var checkedRows = $("#tbGrid").datagrid("getChecked");

    //判断用户有没有选中
    if (checkedRows.length > 0) {
        $.easyuiExt.messager.confirm("Warning", "确定要删除吗？", function (confirm) {
            if (confirm) {
                //填充用户名数组
                var checkedLoginIds = [];
                for (var i = 0; i < checkedRows.length; i++) {
                    checkedLoginIds.push(checkedRows[i].Number);
                }

                //JSON格式转换
                var params = $.global.appendArray(null, checkedLoginIds, "loginIds");

                $.ajax({
                    type: "POST",
                    url: "/User/RemoveUsers",
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
        $.easyuiExt.messager.alert("Warning", "请选中要删除的用户！");
    }
}

//启用用户
function enableUser(loginId) {
    $.easyuiExt.messager.confirm("Warning", "确定要启用吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/User/EnableUser/" + loginId,
                success: function () {
                    $.easyuiExt.updateGridInTab();
                },
                error: function (error) {
                    $.easyuiExt.messager.alert("Error", error.responseText);
                }
            });
        }
    });
}

//停用用户
function disableUser(loginId) {
    $.easyuiExt.messager.confirm("Warning", "确定要停用吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "post",
                url: "/User/DisableUser/" + loginId,
                success: function () {
                    $.easyuiExt.updateGridInTab();
                },
                error: function (error) {
                    $.easyuiExt.messager.alert("Error", error.responseText);
                }
            });
        }
    });
}

//搜索
function searchUsers() {
    var form = $("#frmQueries");
    var queryParams = $.global.formatForm(form);

    getUsers(queryParams);
}