using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Toolkits.EasyUI;
using SD.Toolkits.Mapper;
using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 信息系统映射工具类
    /// </summary>
    public static class InfoSytemMap
    {
        #region # 信息系统视图模型映射 —— static InfoSystem ToModel(this InfoSystemInfo...
        /// <summary>
        /// 信息系统视图模型映射
        /// </summary>
        /// <param name="systemInfo">信息系统数据传输对象</param>
        /// <returns>信息系统视图模型</returns>
        public static InfoSystem ToModel(this InfoSystemInfo systemInfo)
        {
            InfoSystem infoSystem = systemInfo.Map<InfoSystemInfo, InfoSystem>();

            infoSystem.ApplicationTypeName = systemInfo.ApplicationType.GetEnumMember();

            return infoSystem;
        }
        #endregion

        #region # 信息系统/权限EasyUI树节点映射 —— static Node ToNode(this InfoSystem infoSystem...
        /// <summary>
        /// 信息系统/权限EasyUI树节点映射
        /// </summary>
        /// <param name="infoSystem">信息系统视图模型</param>
        /// <param name="authorities">权限列表</param>
        /// <returns>EasyUI树节点</returns>
        public static Node ToNode(this InfoSystem infoSystem, IEnumerable<Authority> authorities)
        {
            var attributes = new
            {
                type = "infoSystem"
            };

            Node systemNode = new Node(infoSystem.Id, infoSystem.Name, "open", false, attributes);

            foreach (Authority authority in authorities)
            {
                if (authority.SystemNo == infoSystem.Number)
                {
                    systemNode.children.Add(authority.ToNode());
                }
            }

            return systemNode;
        }
        #endregion

        #region # 信息系统/角色EasyUI树节点映射 —— static Node ToNode(this InfoSystem infoSystem...
        /// <summary>
        /// 信息系统/角色EasyUI树节点映射
        /// </summary>
        /// <param name="infoSystem">信息系统视图模型</param>
        /// <param name="roles">角色列表</param>
        /// <returns>EasyUI树节点</returns>
        public static Node ToNode(this InfoSystem infoSystem, IEnumerable<Role> roles)
        {
            var attributes = new
            {
                type = "infoSystem"
            };

            Node systemNode = new Node(infoSystem.Id, infoSystem.Name, "open", false, attributes);

            foreach (Role role in roles)
            {
                if (role.SystemNo == infoSystem.Number)
                {
                    systemNode.children.Add(role.ToNode());
                }
            }

            return systemNode;
        }
        #endregion
    }
}
