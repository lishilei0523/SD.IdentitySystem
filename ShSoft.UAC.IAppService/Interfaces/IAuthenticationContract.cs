using System;
using System.ServiceModel;
using ShSoft.Framework2016.Infrastructure;

namespace ShSoft.UAC.IAppService.Interfaces
{
    /// <summary>
    /// 身份认证服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://ShSoft.UAC.IAppService.Interfaces")]
    public interface IAuthenticationContract : IApplicationService
    {
        #region # 登录 —— Guid Login(string loginId, string password)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>公钥</returns>
        [OperationContract]
        Guid Login(string loginId, string password);
        #endregion

        #region # 认证 —— bool Authenticate(Guid publicKey)
        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns>是否通过</returns>
        [OperationContract]
        bool Authenticate(Guid publicKey);
        #endregion
    }
}
