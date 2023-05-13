(function (object) {
    //1.定义一个 局部函数变量
    function MessageBox(href) {

        //系统默认显示时间
        var secondConst = 2000;

        //显示时间
        var secondWait = 2000;

        //计时器
        var timer;

        //左边距,顶边距
        var lf, tp;

        //json参数
        var paras = {};

        this.showMessage = function (message) {
            showMsgAllT(message, 0);
        }

        this.close = function () {
            hideBox();
        }

        this.showWait = function (message) {
            showMsgAllT(paras.waitImgTag + message, 0);
        }

        this.showSuccess = function (message) {
            showSysMsg(message, 2);
        }

        this.showError = function (message) {
            showSysMsg(message, 3);
        }

        this.showMsgInfo = function (msg) {
            if (arguments.length > 1) paras.callBack = arguments[1];
            showSysMsg(msg, 1);
        }

        this.showMsgInfoSide = function (eleId, msg, doHid) {//doHid 是否消失
            if (arguments.length > 3) paras.callBack = arguments[1];
            showSysMsgSideEle(eleId, msg, 1, doHid);
        }

        this.showMsgOkSide = function (eleId, msg, doHid) {
            if (arguments.length > 3) paras.callBack = arguments[1];
            showSysMsgSideEle(eleId, msg, 2, doHid);
        }

        this.showMsgErrSide = function (eleId, msg, doHid) {
            if (arguments.length > 3) paras.callBack = arguments[1];
            showSysMsgSideEle(eleId, msg, 3, doHid);
        }

        this.showSysMsgWTime = function (msg, type, second) {
            if (arguments.length > 3) paras.callBack = arguments[3];
            changeIco(type);
            gelContainer().innerHTML = msg;
            showBox();
            secondWait = second;
            if (second >= 0)
                startTimer(emptyMsg);
        }

        this.showReqErr = function () {
            this.showMsgErr("请求错误 ToT!");
        }

        this.showReqOk = function () {
            this.showMsgOk("操作成功 ^o^!");
        }

        this.showReqVF = function () {
            this.showSysMsgWTime("会话过期,3秒后自动返回登录界面 -o-!", 1, 3000);
        }


        function readyMsgBox() {
            paras = { imghref: "/images/", waitImg: "loader.gif", bgImg: "qzonebg.gif" };
            if (href != null) {
                if (href.imghref != null) paras.imghref = href.imghref;
                if (href.waitImg != null) paras.waitImg = href.waitImg;
                if (href.bgImg != null) paras.bgImg = href.bgImg;
            }
            paras.waitImgTag = "<img src='" + paras.imghref + paras.waitImg + "' style='margin-right:10px;' align='middle'/>    ";
            preloadImg(new Array(paras.imghref + paras.bgImg, paras.imghref + paras.waitImg));
            writeMsgBox();
            window.onresize = function () { setPosition(); }
        }

        function showMsgAllT(message, type) {
            clearTimer();
            changeIco(type);
            gelContainer().innerHTML = message;
            showBox();
        }

        function showSysMsg(message, type) {
            changeIco(type);
            gelContainer().innerHTML = message;
            showBox();
            secondWait = secondConst;
            startTimer(emptyMsg);
        }

        //---显示在元素右边
        function showSysMsgSideEle(eleId, msg, type, doHid) {
            changeIco(type);
            gelContainer().innerHTML = msg;
            setPosSideEle(eleId);
            if (doHid) {
                secondWait = secondConst;
                startTimer(emptyMsg);
            } else clearTimer();
        }

        function setPosSideEle(eleId) {
            var wid = document.getElementById(eleId).offsetWidth;
            var hig = document.getElementById(eleId).offsetHeight;
            var pos = getPos(eleId);
            gelBox().style.left = (wid + 2 + pos.left) + "px";
            gelBox().style.top = (pos.top - (hig / 2)) + "px";
            gelBox().style.display = "block";
        }

        function startTimer(functionName) {
            clearTimer();
            timer = window.setTimeout(functionName, secondWait);
        }

        function clearTimer() {
            if (timer != null && timer != undefined) { clearTimeout(timer); }
        }

        function emptyMsg() {
            gelContainer().innerHTML = "";
            hideBox();
            if (paras.callBack != null) { paras.callBack(); paras.callBack = null; }
        }

        function writeMsgBox() {
            var msgBox = document.createElement("table");
            var msgTbody = document.createElement("tbody");
            var msgTr = document.createElement("tr");
            var msgBoxL = document.createElement("td");
            var msgBoxC = document.createElement("td");
            var msgBoxR = document.createElement("td");
            document.body.appendChild(msgBox);
            msgBox.appendChild(msgTbody);
            msgTbody.appendChild(msgTr);
            msgTr.appendChild(msgBoxL);
            msgTr.appendChild(msgBoxC);
            msgTr.appendChild(msgBoxR);
            msgBox.setAttribute("id", "msgBox");
            msgBox.setAttribute("cellpadding", "0");
            msgBox.setAttribute("cellspacing", "0");
            msgBox.style.cssText = "height:52px;width:auto;position:absolute;z-index:9003;display:none; background:url(" + paras.imghref + paras.bgImg + ") 0px -161px;";
            msgBoxL.setAttribute("id", "msgBoxL");
            msgBoxL.style.cssText = "width:50px;background:url(" + paras.imghref + paras.bgImg + ") -7px -108px no-repeat;";
            msgBoxC.setAttribute("id", "msgBoxC");
            msgBoxC.style.cssText = "width:auto;line-height:51px;color:#666666;font-weight:bold;font-size:14px;padding-right:10px;";
            msgBoxR.setAttribute("id", "msgBoxR");
            msgBoxR.style.cssText = "width:5px;background:url(" + paras.imghref + paras.bgImg + ") 0px 0px no-repeat;";
        }

        function changeIco(ty) {
            if (ty === 0)//none
                document.getElementById("msgBoxL").style.width = "10px";
            else document.getElementById("msgBoxL").style.width = "50px";
            if (ty === 1)//info
                document.getElementById("msgBoxL").style.backgroundPosition = "-7px -54px";
            else if (ty === 2)//ok
                document.getElementById("msgBoxL").style.backgroundPosition = "-7px 0px";
            else if (ty === 3)//err
                document.getElementById("msgBoxL").style.backgroundPosition = "-7px -108px";
        }

        function gelBox() {
            return document.getElementById("msgBox");
        }

        function gelContainer() {
            return document.getElementById("msgBoxC");
        }

        function hideBox() {
            gelBox().style.display = "none";
        }

        function showBox() {
            setPosition();
            gelBox().style.display = "block";
        }

        function setPosition() {
            lf = document.body.clientWidth / 2 - (gelBox().innerHTML.replace(/<[^>].*?>/g, "").length) * 10;
            tp = window.screen.height / 2 - 200 + document.documentElement.scrollTop;
            gelBox().style.left = lf + "px";
            gelBox().style.top = tp + "px";
        }

        function preloadImg() {
            var arrimg = new Array();
            if (typeof (arguments[0]) == "string") {
                arrimg[0] = arguments[0];
            }
            if (typeof (arguments[0]) == "object") {
                for (var i = 0; i < arguments[0].length; i++) {
                    arrimg[i] = arguments[0][i];
                }
            }
            var img = new Array();
            for (var i = 0; i < arrimg.length; i++) {
                img[i] = new Image();
                img[i].src = arrimg[i];
            }
        }

        function getPos(eid) {
            var target = document.getElementById(eid); var left = 0, top = 0;
            do { left += target.offsetLeft || 0; top += target.offsetTop || 0; target = target.offsetParent; } while (target);
            return { left: left, top: top }
        }

        readyMsgBox();
    }

    //在DOM树创建完毕后，为jquery添加一个messageBox对象
    //因为MessageBox方法中创建了一个文本框，并添加到了body元素中，所以需要等DOM树加载完毕后执行
    object(document).ready(function () {
        object.extend(object, {
            messageBox: new MessageBox({ imghref: "/Content/images/" })
        });
    });
})($);
