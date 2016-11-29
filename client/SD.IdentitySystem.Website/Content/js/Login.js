//定义消息框
var messageBox = null;

//DOM OnReady
$(function () {
    //让用户名文本框获得焦点
    $("#txtUserName").focus();

    //验证码图片点击事件
    $("#imgValidCode").click(resetValidCode);

    //初始化消息框
    messageBox = new MessageBox({ imghref: "/Content/images/" });
});

//刷新验证码
function resetValidCode() {
    $("#imgValidCode").attr("src", "/User/GetValidCode?id=" + Math.random());
}

//登录开始时执行方法
function loginBegin() {
    messageBox.showMsgWait("验证中，请稍候...");
}

//登录成功
function loginSucceed(result) {
    //登录成功，跳转至主页
    messageBox.showMsgOk("登录成功");
    window.location.href = "/Home/Index";
}

//登录失败
function loginFail(result) {


    //登录失败，清空密码、刷新验证码、提醒错误消息
    $("#txtPwd").val("");
    $("#txtVadlidCode").val("");
    $("#imgValidCode").attr("src", "/User/GetValidCode?id=" + Math.random());

    alert(JSON.stringify(result));
    messageBox.showMsgErr(result.responseText);
}