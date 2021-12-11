using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models;
using SD.Toolkits.EasyUI;
using SD.Toolkits.Mapper;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 权限映射
    /// </summary>
    public static class AuthorityMap
    {
        #region # 权限模型映射 —— static Authority ToModel(this AuthorityInfo...
        /// <summary>
        /// 权限模型映射
        /// </summary>
        public static Authority ToModel(this AuthorityInfo authorityInfo)
        {
            Authority authority = authorityInfo.Map<AuthorityInfo, Authority>();
            authority.InfoSystemName = authorityInfo.InfoSystemInfo.Name;
            authority.ApplicationTypeName = authorityInfo.ApplicationType.GetEnumMember();

            return authority;
        }
        #endregion

        #region # 权限EasyUI树节点映射 —— static Node ToNode(this Authority authority)
        /// <summary>
        /// 权限EasyUI树节点映射
        /// </summary>
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
