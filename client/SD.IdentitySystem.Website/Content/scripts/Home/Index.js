//提供给iframe里子页面操作当前页面的一些便捷方法
var topHelper = {};

//DOM初始化事件
$(function () {
    //初始化用户菜单
    $("#menuTree").tree({
        url: "/Menu/GetMenuTree/00",
        animate: true,
        lines: true,
        onClick: function (node) {
            createTab(node.text, node.attributes.href, node.attributes.isLeaf);
        }
    });

    //初始化公共窗体
    topHelper.comWin = $("#commonWindow").window({
        width: 800,
        height: 500,
        collapsible: false,
        minimizable: false,
        maximizable: true,
        modal: true
    }).window("close");

    //添加一个打开公共窗体的方法
    topHelper.showWindow = function (title, url, width, height, resizable) {
        var trueTitle = "公共窗体";
        var trueWidth = 1024;
        var trueHeight = 768;
        var trueResizable = false;

        if (title) {
            trueTitle = title;
        }
        if (width && parseInt(width) > 10) {
            trueWidth = width;
        }
        if (height && parseInt(height) > 10) {
            trueHeight = height;
        }
        if (resizable) {
            trueResizable = resizable;
        }

        //判断是否置顶url，如果有，则设置公共窗体里的iframe的src
        if (url && url.length) {
            $("#commonWindow iframe").attr("src", url);
        }

        //重新设置窗体的大小，并自动居中，然后才显示
        topHelper.comWin.window({
            title: trueTitle,
            width: trueWidth,
            height: trueHeight,
            resizable: trueResizable
        }).window("center").window("open");
    };

    //添加一个关闭公共窗体方法
    topHelper.closeWindow = function () {
        topHelper.comWin.window("close");
    };

    //新增或修改成功后，可通过此方法更新tab里的DataGrid组件
    topHelper.updateGridInTab = function () {
        //1.获取后台首页的tab容器
        var $tabBox = $("#tabs");

        //2.获取选中的tab
        var $curTab = $tabBox.tabs("getSelected");

        //3.从选中的tab中获取iframe，并以jq对象返回
        var $ifram = $("iframe", $curTab);

        //4.从jq对象中获取iframe，并通过contentWindow对象操作iframe里的window的全局变量$tbGrid

        //清除选中
        $ifram[0].contentWindow.$tbGrid.datagrid("clearSelections");

        //刷新表格
        $ifram[0].contentWindow.$tbGrid.datagrid("reload");
    };

    //新增或修改成功后，可通过此方法更新tab里的TreeGrid组件
    topHelper.updateTreeGridInTab = function () {
        //1.获取后台首页的tab容器
        var $tabBox = $("#tabs");

        //2.获取选中的tab
        var $curTab = $tabBox.tabs("getSelected");

        //3.从选中的tab中获取iframe，并以jq对象返回
        var $ifram = $("iframe", $curTab);

        //4.从jq对象中获取iframe，并通过contentWindow对象操作iframe里的window的全局变量$treeGrid

        //清除选中
        $ifram[0].contentWindow.$treeGrid.treegrid("clearSelections");

        //刷新表格
        $ifram[0].contentWindow.$treeGrid.treegrid("reload");
    };
});

//注销
function logout() {
    $.messager.confirm("Warning", "确定注销当前用户吗？", function (confirm) {
        if (confirm) {
            $.ajax({
                type: "POST",
                url: "/User/Logout",
                success: function () {
                    window.location.href = "/User/Login";
                },
                error: function (error) {
                    $.messager.alert("Error", error.responseText);
                }
            });
        }
    });
}

//打开修改密码窗口
function openChangePassword() {
    $("#dvUpdatePwd").window("open");
    $("#fmUpdatePwd").form("clear");
}

//修改密码
function changePassword() {
    //获取用户输入密码
    var newPassword = $("#newPassword").val();
    var confirmPassword = $("#confirmPassword").val();

    //判断用户输入
    if (newPassword !== confirmPassword) {
        $.messager.alert("Warning", "两次密码不一致，请重试！");
        return;
    }
    else {
        //填充用户登录名
        var loginId = $("#spLoginId").text();
        $("#hdLoginId").val(loginId);

        //提交表单
        $("#fmUpdatePwd").submit();
    }
}

//修改密码中事件
function updatingPassword() {
    $.messageBox.showWait("修改中，请稍后...");
}

//修改密码成功
function updateSucceed() {
    $.messager.alert("OK", "修改成功");
    $("#dvUpdatePwd").window("close");
}

//修改密码失败
function updateFailed(result) {
    $.messager.alert("Warning", result.responseText);
}

//创建选项卡
function createTab(title, url, isLeaf) {
    //判断是否是链接
    if (isLeaf) {
        //如果选项卡存在，刷新
        if ($("#tabs").tabs("exists", title)) {
            var currTab = $("#tabs").tabs("getSelected");
            $("#tabs").tabs("select", title);
            url = $(currTab.panel("options").content).attr("src");
            if (url != undefined) {
                $("#tabs").tabs("update", {
                    tab: currTab,
                    options: {
                        content: createFrame(url)
                    }
                });
            }
        }
            //如果选项卡不存在，创建
        else {
            var content = createFrame(url);
            $("#tabs").tabs("add", {
                title: title,
                content: content,
                closable: true
            });
        }
    }
}

//创建选项卡中的iframe
function createFrame(url) {
    var tabHeight = $("#tabs").height() - 35;
    var frame = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:' + tabHeight + 'px;"></iframe>';
    return frame;
}