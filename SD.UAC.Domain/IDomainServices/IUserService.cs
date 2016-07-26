using SD.UAC.Domain.Entities;
using ShSoft.Infrastructure.DomainServiceBase;

namespace SD.UAC.Domain.IDomainServices
{
    /// <summary>
    /// 用户领域服务接口
    /// </summary>
    public interface IUserService : IDomainService<User>
    {
        #region # 断言登录名不存在 —— void AssertLoginIdNotExists(string loginId)
        /// <summary>
        /// 断言登录名不存在
        /// </summary>
        /// <param name="loginId">登录名</param>
        void AssertLoginIdNotExists(string loginId);
        #endregion
    }
}
