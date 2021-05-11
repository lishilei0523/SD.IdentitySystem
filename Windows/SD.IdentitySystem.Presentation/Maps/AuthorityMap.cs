using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.WPF.Models;
using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 权限映射工具类
    /// </summary>
    public static class AuthorityMap
    {
        #region # 权限树节点映射 —— static Node ToNode(this AuthorityInfo authority)
        /// <summary>
        /// 权限树节点映射
        /// </summary>
        /// <param name="authority">权限</param>
        /// <returns>树节点</returns>
        public static Node ToNode(this AuthorityInfo authority)
        {
            return new Node(authority.Id, authority.Name, false, null);
        }
        #endregion

        #region # 信息系统/权限树节点映射 —— static Node ToNode(this InfoSystemInfo infoSystem...
        /// <summary>
        /// 信息系统/权限树节点映射
        /// </summary>
        /// <param name="infoSystem">信息系统</param>
        /// <param name="authorities">权限列表</param>
        /// <returns>树节点</returns>
        public static Node ToNode(this InfoSystemInfo infoSystem, IEnumerable<AuthorityInfo> authorities)
        {
            Node systemNode = new Node(infoSystem.Id, infoSystem.Name, false, null);
            foreach (AuthorityInfo authority in authorities)
            {
                if (authority.SystemNo == infoSystem.Number)
                {
                    systemNode.SubNodes.Add(authority.ToNode());
                }
            }

            return systemNode;
        }
        #endregion
    }
}
