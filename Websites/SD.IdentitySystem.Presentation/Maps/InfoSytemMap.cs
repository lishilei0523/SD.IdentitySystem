using SD.Common;
using SD.FormatModel.EasyUI;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.Toolkits.Mapper;
using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 信息系统映射工具类
    /// </summary>
    public static class InfoSytemMap
    {
        #region # 信息系统视图模型映射 —— static InfoSystemView ToViewModel(this InfoSystemInfo...
        /// <summary>
        /// 信息系统视图模型映射
        /// </summary>
        /// <param name="systemInfo">信息系统数据传输对象</param>
        /// <returns>信息系统视图模型</returns>
        public static InfoSystemView ToViewModel(this InfoSystemInfo systemInfo)
        {
            InfoSystemView systemView = systemInfo.Map<InfoSystemInfo, InfoSystemView>();

            systemView.ApplicationTypeName = systemInfo.ApplicationType.GetEnumMember();

            return systemView;
        }
        #endregion

        #region # 信息系统/权限EasyUI树节点映射 —— static Node ToNode(this InfoSystemView systemView...
        /// <summary>
        /// 信息系统/权限EasyUI树节点映射
        /// </summary>
        /// <param name="systemView">信息系统视图模型</param>
        /// <param name="authorities">权限列表</param>
        /// <returns>EasyUI树节点</returns>
        public static Node ToNode(this InfoSystemView systemView, IEnumerable<AuthorityView> authorities)
        {
            var attributes = new
            {
                type = "infoSystem"
            };

            Node systemNode = new Node(systemView.Id, systemView.Name, "open", false, attributes);

            foreach (AuthorityView authority in authorities)
            {
                if (authority.SystemNo == systemView.Number)
                {
                    systemNode.children.Add(authority.ToNode());
                }
            }

            return systemNode;
        }
        #endregion

        #region # 信息系统/角色EasyUI树节点映射 —— static Node ToNode(this InfoSystemView systemView...
        /// <summary>
        /// 信息系统/角色EasyUI树节点映射
        /// </summary>
        /// <param name="systemView">信息系统视图模型</param>
        /// <param name="roles">角色列表</param>
        /// <returns>EasyUI树节点</returns>
        public static Node ToNode(this InfoSystemView systemView, IEnumerable<RoleView> roles)
        {
            var attributes = new
            {
                type = "infoSystem"
            };

            Node systemNode = new Node(systemView.Id, systemView.Name, "open", false, attributes);

            foreach (RoleView role in roles)
            {
                if (role.SystemNo == systemView.Number)
                {
                    systemNode.children.Add(role.ToNode());
                }
            }

            return systemNode;
        }
        #endregion
    }
}
