using SD.Infrastructure.MemberShip;

// ReSharper disable once CheckNamespace
namespace SD.IdentitySystem
{
    /// <summary>
    /// Membership管理工具类
    /// </summary>
    public static class Membership
    {
        /// <summary>
        /// 当前登录信息
        /// </summary>
        public static LoginInfo LoginInfo
        {
            get
            {
                //if (!context.HttpContext.Request.Headers.TryGetValue(SessionKey.CurrentPublicKey, out StringValues header))
                //{
                //    ObjectResult response = new ObjectResult("身份认证消息头不存在，请检查程序！")
                //    {
                //        StatusCode = (int)HttpStatusCode.Unauthorized
                //    };
                //    context.Result = response;
                //}

                return null;
            }
        }
    }
}
