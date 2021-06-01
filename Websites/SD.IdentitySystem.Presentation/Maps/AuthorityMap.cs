using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Toolkits.EasyUI;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 权限映射工具类
    /// </summary>
    public static class AuthorityMap
    {
        #region # 权限模型映射 —— static Authority ToModel(this AuthorityInfo...
        /// <summary>
        /// 权限模型映射
        /// </summary>
        /// <param name="authorityInfo">权限数据传输对象</param>
        /// <returns>权限模型</returns>
        public static Authority ToModel(this AuthorityInfo authorityInfo)
        {
            Authority authority = authorityInfo.Map<AuthorityInfo, Authority>();
            authority.SystemName = authorityInfo.InfoSystemInfo.Name;
            authority.ApplicationType = authorityInfo.ApplicationType.GetEnumMember();

            return authority;
        }
        #endregion

        #region # 权限EasyUI树节点映射 —— static Node ToNode(this Authority authority)
        /// <summary>
        /// 权限EasyUI树节点映射
        /// </summary>
        /// <param name="authority">权限模型</param>
        /// <returns>EasyUI树节点</returns>
        public static Node ToNode(this Authority authority)
        {
            var attributes = new
            {
                type = "authority"
            };

            return new Node(authority.Id, authority.Name, "open", false, attributes);
        }
        #endregion
    }
}
