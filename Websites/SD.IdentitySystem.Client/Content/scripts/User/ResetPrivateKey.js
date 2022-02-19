//重置开始
function resetBegin() {
    window.top.window.$.messageBox.showWait("重置中，请稍候...");
}

//重置成功
function resetSucceed() {
    window.top.window.$.messageBox.close();
    $.easyuiExt.messager.alert("OK", "重置成功！");

    $.easyuiExt.updateGridInTab();
    $.easyuiExt.closeWindow();
}

//重置失败
function resetFail(result) {
    window.top.window.$.messageBox.close();
    $.easyuiExt.messager.alert("Error", result.responseText);
}
