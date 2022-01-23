using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System.Web;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// ASP.NET Membership提供者
    /// </summary>
    public class MembershipProvider : IMembershipProvider
    {
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>登录信息</returns>
        public LoginInfo GetLoginInfo()
        {
            if (HttpContext.Current != null)
            {
                //从Session中获取
                LoginInfo loginInfo = HttpContext.Current.Session[SessionKey.CurrentUser] as LoginInfo;

                return loginInfo;
            }

            return null;
        }
    }
}
