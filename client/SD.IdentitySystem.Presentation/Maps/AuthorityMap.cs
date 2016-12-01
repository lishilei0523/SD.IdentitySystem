using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using ShSoft.Common.PoweredByLee;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 权限映射工具类
    /// </summary>
    public static class AuthorityMap
    {
        #region # 权限视图模型映射 —— static AuthorityView ToViewModel(this AuthorityInfo...
        /// <summary>
        /// 权限视图模型映射
        /// </summary>
        /// <param name="authorityInfo">权限数据传输对象</param>
        /// <returns>权限视图模型</returns>
        public static AuthorityView ToViewModel(this AuthorityInfo authorityInfo)
        {
            AuthorityView authorityView = Transform<AuthorityInfo, AuthorityView>.Map(authorityInfo);

            authorityView.SystemNo = authorityInfo.InfoSystemInfo.Number;
            authorityView.SystemName = authorityInfo.InfoSystemInfo.Name;

            return authorityView;
        }
        #endregion
    }
}
