//DOM初始化事件
$(function () {
    //让用户名文本框获得焦点
    $("#txtUserName").focus();

    //验证码图片点击事件
    $("#imgValidCode").click(resetValidCode);
});

//刷新验证码
function resetValidCode() {
    $("#imgValidCode").attr("src", "/User/GetValidCode?id=" + Math.random());
}

//登录开始时执行方法
function loginBegin() {
    $.messageBox.showWait("验证中，请稍候...");
}

//登录成功
function loginSucceed() {
    //登录成功，跳转至主页
    $.messageBox.showSuccess("登录成功！");

    window.location.href = "/Home/Index";
}

//登录失败
function loginFail(result) {
    //清空密码、刷新验证码、提醒错误消息
    $("#txtPwd").val("");
    $("#txtVadlidCode").val("");
    $("#imgValidCode").attr("src", "/User/GetValidCode?id=" + Math.random());

    $.messageBox.showError(result.responseText);
}