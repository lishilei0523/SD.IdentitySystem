using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System.Web;

namespace SD.IdentitySystem.Membership.AspNet
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
