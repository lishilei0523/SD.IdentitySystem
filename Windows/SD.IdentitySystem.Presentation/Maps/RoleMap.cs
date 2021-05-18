using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 角色映射工具类
    /// </summary>
    public static class RoleMap
    {
        #region # 角色数据项映射 —— static Item ToItem(this RoleInfo role)
        /// <summary>
        /// 角色数据项映射
        /// </summary>
        public static Item ToItem(this RoleInfo role)
        {
            return new Item(role.Id, role.Name, false, false, role.InfoSystemInfo.Name);
        }
        #endregion

        #region # 角色树节点映射 —— static Node ToNode(this RoleInfo role)
        /// <summary>
        /// 角色树节点映射
        /// </summary>
        /// <param name="role">角色模型</param>
        /// <returns>树节点</returns>
        public static Node ToNode(this RoleInfo role)
        {
            return new Node(role.Id, role.Name, false, null);
        }
        #endregion

        #region # 信息系统/角色树节点映射 —— static Node ToNode(this InfoSystem infoSystem...
        /// <summary>
        /// 信息系统/角色树节点映射
        /// </summary>
        /// <param name="infoSystem">信息系统</param>
        /// <param name="roles">角色列表</param>
        /// <returns>树节点</returns>
        public static Node ToNode(this InfoSystemInfo infoSystem, IEnumerable<RoleInfo> roles)
        {
            Node systemNode = new Node(infoSystem.Id, infoSystem.Name, false, null);
            foreach (RoleInfo role in roles)
            {
                if (role.SystemNo == infoSystem.Number)
                {
                    systemNode.SubNodes.Add(role.ToNode());
                }
            }

            return systemNode;
        }
        #endregion
    }
}
