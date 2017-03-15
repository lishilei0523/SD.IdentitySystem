using SD.Common.PoweredByLee;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;

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

            authorityView.SystemName = authorityInfo.InfoSystemInfo.Name;

            return authorityView;
        }
        #endregion

        #region # 权限EasyUI树节点映射 —— static Node ToNode(this AuthorityView authorityView)
        /// <summary>
        /// 权限EasyUI树节点映射
        /// </summary>
        /// <param name="authorityView">权限视图模型</param>
        /// <returns>EasyUI树节点</returns>
        public static Node ToNode(this AuthorityView authorityView)
        {
            var attributes = new
            {
                type = "authority"
            };

            return new Node(authorityView.Id, authorityView.Name, "open", false, attributes);
        }
        #endregion
    }
}
