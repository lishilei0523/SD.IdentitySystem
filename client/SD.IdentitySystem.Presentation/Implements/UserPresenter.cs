using System;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using ShSoft.ValueObjects.Structs;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 用户呈现器实现
    /// </summary>
    public class UserPresenter : IUserPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="userContract">用户服务接口</param>
        public UserPresenter(IUserContract userContract)
        {
            this._userContract = userContract;
        }

        #endregion
    }
}
