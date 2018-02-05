using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

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
        /// <returns></returns>
        public static LoginInfo LoginInfo
        {
            get
            {
                //服务端获取
                if (OperationContext.Current != null)
                {
                    //获取消息头
                    MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

                    if (!headers.Any(x => x.Name == CommonConstants.WcfAuthHeaderName && x.Namespace == CommonConstants.WcfAuthHeaderNamespace))
                    {
                        return null;
                    }

                    //读取消息头中的公钥
                    Guid publicKey = headers.GetHeader<Guid>(CommonConstants.WcfAuthHeaderName, CommonConstants.WcfAuthHeaderNamespace);

                    //以公钥为键，查询分布式缓存
                    LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                    return loginInfo;
                }
                //Web端获取
                if (HttpContext.Current != null)
                {
                    //从Session中获取
                    LoginInfo loginInfo = HttpContext.Current.Session[SessionKey.CurrentUser] as LoginInfo;

                    return loginInfo;
                }
                //客户端获取
                else
                {
                    //从AppDomain中获取
                    LoginInfo loginInfo = AppDomain.CurrentDomain.GetData(SessionKey.CurrentUser) as LoginInfo;

                    return loginInfo;
                }
            }
        }
    }
}
