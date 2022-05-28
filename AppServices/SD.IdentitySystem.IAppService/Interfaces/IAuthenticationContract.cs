using SD.Infrastructure.AppServiceBase;
using SD.Infrastructure.Membership;
using System.ServiceModel;

namespace SD.IdentitySystem.IAppService.Interfaces
{
    /// <summary>
    /// 身份认证服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IAuthenticationContract : IApplicationService
    {
        #region # 登录 —— LoginInfo Logon(string privateKey)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>登录信息</returns>
        [OperationContract]
        LoginInfo Logon(string privateKey);
        #endregion

        #region # 登录 —— LoginInfo Login(string loginId, string password...
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="clientId">客户端Id</param>
        /// <returns>登录信息</returns>
        [OperationContract]
        LoginInfo Login(string loginId, string password, string clientId = null);
        #endregion
    }
}
