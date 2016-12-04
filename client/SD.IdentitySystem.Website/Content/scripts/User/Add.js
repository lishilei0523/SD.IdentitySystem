//创建开始
function addBegin() {
    window.top.window.$.messageBox.showWait("创建中，请稍候...");
}

//创建成功
function addSucceed() {
    window.top.window.$.messageBox.close();
    $.easyuiExt.messager.alert("OK", "创建成功！");

    $.easyuiExt.updateGridInTab();
    $.easyuiExt.closeWindow();
}

//创建失败
function addFail(result) {
    window.top.window.$.messageBox.close();
    $.easyuiExt.messager.alert("Error", result.responseText);
}