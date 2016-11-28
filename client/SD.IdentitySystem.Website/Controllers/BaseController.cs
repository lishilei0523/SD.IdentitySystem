using System;
using System.Web;
using System.Web.Mvc;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IOC.Core.Mediator;
using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.Constants;
using ShSoft.ValueObjects.Structs;

namespace SD.IdentitySystem.Website.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class BaseController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户呈现器接口
        /// </summary>
        private readonly IUserPresenter _userPresenter;

        /// <summary>
        /// 字段及依赖注入构造器
        /// </summary>
        protected BaseController()
        {
            this._userPresenter = ResolveMediator.Resolve<IUserPresenter>();
        }

        #endregion

        #region # 属性

        #region 验证码 —— string ValidCode
        /// <summary>
        /// 验证码
        /// </summary>
        protected string ValidCode
        {
            get
            {
                return
                    base.Session[CacheConstants.ValidCodeKey] == null ?
                    null :
                    base.Session[CacheConstants.ValidCodeKey].ToString();
            }
            set
            {
                base.Session[CacheConstants.ValidCodeKey] = value;
            }
        }
        #endregion

        #region 当前登录用户 —— LoginInfo LoginInfo
        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected LoginInfo LoginInfo
        {
            get
            {
                return base.Session[CacheConstants.CurrentUserKey] as LoginInfo; ;
            }
            set
            {
                base.Session[CacheConstants.CurrentUserKey] = value;
            }
        }
        #endregion

        #region 用户名Cookie —— string LoginIdCookie
        /// <summary>
        /// 用户名Cookie
        /// </summary>
        protected string LoginIdCookie
        {
            get
            {
                //获取本地用户名Cookie
                HttpCookie cookieUserName = base.Request.Cookies["UserName"];

                //判断是否存在
                if (cookieUserName != null)
                {
                    //cookie中有值，返回
                    return cookieUserName.Value;
                }

                //cookie中无值，返回null
                return null;
            }
            set
            {
                //存放用户名的cookie
                HttpCookie cookieUserName = new HttpCookie("UserName", value);
                cookieUserName.Expires = DateTime.Now.AddDays(7);
                this.Response.Cookies.Add(cookieUserName);
            }
        }
        #endregion

        #region 密码Cookie —— string PasswordCookie
        /// <summary>
        /// 密码Cookie
        /// </summary>
        protected string PasswordCookie
        {
            get
            {
                HttpCookie cookiePwd = this.Request.Cookies["Password"];

                if (cookiePwd != null)
                {
                    return cookiePwd.Value;
                }
                return null;
            }
            set
            {
                HttpCookie cookiePwd = new HttpCookie("Password", value.ToMD5())
                {
                    Expires = DateTime.Now.AddDays(7)
                };

                this.Response.Cookies.Add(cookiePwd);
            }
        }
        #endregion

        #region 只读属性 - 是否已登录 —— bool Logined
        /// <summary>
        /// 只读属性 - 是否已登录
        /// </summary>
        protected bool Logined
        {
            get
            {
                //校验Session
                if (this.LoginInfo != null)
                {
                    return true;
                }
                //校验Cookie
                if (string.IsNullOrWhiteSpace(this.LoginIdCookie) ||
                    string.IsNullOrWhiteSpace(this.PasswordCookie))
                {
                    return false;
                }

                try
                {
                    this.LoginInfo = this._userPresenter.Login(this.LoginIdCookie, this.PasswordCookie, base.Request.UserHostAddress);

                    return true;
                }
                catch
                {

                    return false;
                }
            }
        }
        #endregion

        #endregion

        #region # 方法

        #region Overrides of Controller

        /// <summary>
        /// 当操作中发生未经处理的异常时调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.Write(filterContext.Exception.Message);
        }

        #endregion

        #region 授权过滤器 —— override void OnAuthorization(AuthorizationContext filterContext)
        /// <summary>
        /// 授权过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断用户是否登录，Action上是否贴有无需过滤标签
            if (!this.Logined && !this.HasAttribute<AllowAnonymousAttribute>(filterContext.ActionDescriptor))
            {
                filterContext.HttpContext.Response.Redirect(base.Url.Action("Login", "User"));
            }
        }
        #endregion

        #region 清空验证码 —— void ClearValidCode()
        /// <summary>
        /// 清空验证码
        /// </summary>
        protected void ClearValidCode()
        {
            this.Session.Remove(CacheConstants.ValidCodeKey);
        }
        #endregion

        #region 判断控制器或方法上是否贴有某特性标签 —— bool HasAttribute<T>(ActionDescriptor action)
        /// <summary>
        /// 判断控制器或方法上是否贴有某特性标签
        /// </summary>
        /// <typeparam name="T">特性标签类型</typeparam>
        /// <param name="action">ActionDescriptor</param>
        /// <returns>是否拥有该特性</returns>
        private bool HasAttribute<T>(ActionDescriptor action)
        {
            Type type = typeof(T);
            return action.IsDefined(type, false) || action.ControllerDescriptor.IsDefined(type, false);
        }
        #endregion

        #endregion
    }
}