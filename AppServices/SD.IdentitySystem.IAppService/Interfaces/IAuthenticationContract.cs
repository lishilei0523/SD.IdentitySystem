using SD.Infrastructure.AppServiceBase;
using SD.Infrastructure.MemberShip;
using System.ServiceModel;

namespace SD.IdentitySystem.IAppService.Interfaces
{
    /// <summary>
    /// 身份认证服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.IdentitySystem.IAppService.Interfaces")]
    public interface IAuthenticationContract : IApplicationService
    {
        #region # 登录 —— LoginInfo Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>登录信息</returns>
        [OperationContract]
        LoginInfo Login(string loginId, string password);
        #endregion

        #region # 私钥登录 —— LoginInfo LoginByPrivateKey(string privateKey)
        /// <summary>
        /// 私钥登录
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>登录信息</returns>
        [OperationContract]
        LoginInfo LoginByPrivateKey(string privateKey);
        #endregion
    }
}
