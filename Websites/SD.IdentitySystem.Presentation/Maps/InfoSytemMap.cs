using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Toolkits.EasyUI;
using SD.Toolkits.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 信息系统映射工具类
    /// </summary>
    public static class InfoSytemMap
    {
        #region # 信息系统模型映射 —— static InfoSystem ToModel(this InfoSystemInfo...
        /// <summary>
        /// 信息系统模型映射
        /// </summary>
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
        public static Node ToNode(this InfoSystem infoSystem, IEnumerable<Authority> authorities)
        {
            var attributes = new
            {
                type = "infoSystem"
            };

            Node systemNode = new Node(infoSystem.Id, infoSystem.Name, "open", false, attributes);
            IEnumerable<IGrouping<string, Authority>> authoritiesGroups = authorities.GroupBy(x => x.ApplicationType);
            foreach (IGrouping<string, Authority> authoritiesGroup in authoritiesGroups)
            {
                Node applicationTypeNode = new Node(Guid.Empty, authoritiesGroup.Key, "open", false, attributes);
                systemNode.children.Add(applicationTypeNode);
                foreach (Authority authority in authoritiesGroup)
                {
                    applicationTypeNode.children.Add(authority.ToNode());
                }
            }

            return systemNode;
        }
        #endregion

        #region # 信息系统/角色EasyUI树节点映射 —— static Node ToNode(this InfoSystem infoSystem...
        /// <summary>
        /// 信息系统/角色EasyUI树节点映射
        /// </summary>
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
