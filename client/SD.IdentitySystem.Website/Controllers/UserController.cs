using System.Web.Mvc;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.Constants;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : Controller
    {
        //视图部分

        #region # 加载登录视图 —— ViewResult Login()
        /// <summary>
        /// 加载登录视图
        /// </summary>
        /// <returns>登录视图</returns>
        public ViewResult Login()
        {
            return this.View();
        }
        #endregion


        //命令部分

        #region # 登录 —— JsonResult Login(string loginId, string password...
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="validCode">验证码</param>
        /// <param name="rememberMe">记住密码</param>
        /// <returns>登录结果</returns>
        [AllowAnonymous]
        public JsonResult Login(string loginId, string password, string validCode, bool rememberMe)
        {
            string currentValidCode =
                this.Session[CacheConstants.ValidCodeKey] == null ? null :
                this.Session[CacheConstants.ValidCodeKey].ToString();

            //校验验证码
            if (currentValidCode != null && currentValidCode == validCode)        //验证码正确
            {
                //清空验证码
                this.Session.Remove(CacheConstants.ValidCodeKey);

                //2.校验用户名密码
                return OperateContext.Current.CheckLogin(userView);
            }

            //清空验证码
            this.Session.Remove(CacheConstants.ValidCodeKey);

            //返回JsonResult
            return base.Json(new { Success = false, Message = "验证码错误！" });
        }
        #endregion


        //查询部分

        #region # 获取验证码图片 —— FileContentResult GetValidCode()
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns>验证码图片二进制内容</returns>
        [AllowAnonymous]
        public FileContentResult GetValidCode()
        {
            string validCode = ValidCodeGenerator.GenerateCode(4);
            byte[] buffer = ValidCodeGenerator.GenerateStream(validCode);

            this.Session[CacheConstants.ValidCodeKey] = validCode;

            return base.File(buffer, @"image/jpeg");
        }
        #endregion

        #region 04.注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns>JsonResult</returns>
        public ActionResult Logout()
        {
            //1.清空Session
            OperateContext.Current.Session.Clear();
            return OperateContext.Current.JsonModel(1, "注销成功", "/Admin/User/Login");
        }
        #endregion
    }
}
