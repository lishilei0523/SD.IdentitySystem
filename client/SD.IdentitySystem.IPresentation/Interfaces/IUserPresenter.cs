using ShSoft.ValueObjects.Structs;

namespace SD.IdentitySystem.IPresentation.Interfaces
{
    /// <summary>
    /// 用户呈现器接口
    /// </summary>
    public interface IUserPresenter
    {
        #region # 登录 —— LoginInfo Login(string loginId, string password...
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="ip">IP地址</param>
        /// <returns>公钥</returns>
        LoginInfo Login(string loginId, string password, string ip);
        #endregion
    }
}
