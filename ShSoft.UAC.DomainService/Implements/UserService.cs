using System;
using ShSoft.UAC.Domain.Entities;
using ShSoft.UAC.Domain.IDomainServices;
using ShSoft.UAC.Domain.Mediators;

namespace ShSoft.UAC.DomainService.Implements
{
    /// <summary>
    /// 用户领域服务实现
    /// </summary>
    public class UserService : IUserService
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 仓储中介者
        /// </summary>
        private readonly RepositoryMediator _repMediator;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="repMediator">仓储中介者</param>
        public UserService(RepositoryMediator repMediator)
        {
            this._repMediator = repMediator;
        }

        #endregion

        #region # 断言登录名不存在 —— void AssertLoginIdNotExists(string loginId)
        /// <summary>
        /// 断言登录名不存在
        /// </summary>
        /// <param name="loginId">登录名</param>
        public void AssertLoginIdNotExists(string loginId)
        {
            if (this._repMediator.UserRep.Exists(loginId))
            {
                throw new ArgumentOutOfRangeException("loginId", string.Format("登录名\"{0}\"已存在！", loginId));
            }
        }
        #endregion

        #region 没用

        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(User entity)
        {
            throw new NotImplementedException("内部已实现");
        }


        #endregion
    }
}
