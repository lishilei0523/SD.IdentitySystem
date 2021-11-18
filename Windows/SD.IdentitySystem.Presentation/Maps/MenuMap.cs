using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.WPF.Models;
using SD.Toolkits.Mapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 菜单映射工具类
    /// </summary>
    public static class MenuMap
    {
        #region # 菜单映射 —— static Menu ToModel(this MenuInfo...
        /// <summary>
        /// 菜单映射
        /// </summary>
        /// <param name="menuInfo">菜单</param>
        /// <returns>菜单</returns>
        public static Menu ToModel(this MenuInfo menuInfo)
        {
            Menu menu = menuInfo.Map<MenuInfo, Menu>();
            menu.SystemName = menuInfo.InfoSystemInfo.Name;

            return menu;
        }
        #endregion

        #region # 菜单树节点映射 —— static Node ToNode(this Menu menu)
        /// <summary>
        /// 菜单树节点映射
        /// </summary>
        /// <param name="menu">菜单</param>
        /// <returns>树节点</returns>
        public static Node ToNode(this Menu menu)
        {
            return new Node(menu.Id, null, menu.Name, false, null);
        }
        #endregion

        #region # 菜单树映射 —— static ICollection<Node> ToTree(this IEnumerable<Menu...
        /// <summary>
        /// 菜单树映射
        /// </summary>
        /// <param name="menus">菜单集</param>
        /// <param name="parentId">父级菜单Id</param>
        /// <returns>菜单树</returns>
        public static ICollection<Node> ToTree(this IEnumerable<Menu> menus, Guid? parentId)
        {
            //验证
            menus = menus?.ToArray() ?? Array.Empty<Menu>();

            //声明容器
            ICollection<Node> tree = new HashSet<Node>();

            //判断上级菜单Id是否为null
            if (!parentId.HasValue)
            {
                //从根级开始遍历
                foreach (Menu menu in menus.Where(x => x.IsRoot))
                {
                    Node node = menu.ToNode();
                    tree.Add(node);

                    node.SubNodes = new ObservableCollection<Node>(menus.ToTree(node.Id));
                }
            }
            else
            {
                //从给定Id向下遍历
                foreach (Menu menu in menus.Where(x => x.ParentMenuId.HasValue && x.ParentMenuId.Value == parentId.Value))
                {
                    Node node = menu.ToNode();
                    tree.Add(node);

                    node.SubNodes = new ObservableCollection<Node>(menus.ToTree(node.Id));
                }
            }

            return tree;
        }
        #endregion

        #region # 菜单树列表映射 —— static IEnumerable<Menu> ToTreeList(this...
        /// <summary>
        /// 菜单树列表映射
        /// </summary>
        /// <param name="menus">菜单列表</param>
        /// <returns>菜单树列表</returns>
        public static IEnumerable<Menu> ToTreeList(this IEnumerable<Menu> menus)
        {
            Menu[] allMenus = menus?.ToArray() ?? Array.Empty<Menu>();
            foreach (Menu menu in allMenus)
            {
                menu.FillSubNodes(allMenus);
            }

            return allMenus.Where(x => x.IsRoot);
        }
        #endregion


        //Private

        #region # 填充下级节点 —— static void FillSubNodes(this Menu menu...
        /// <summary>
        /// 填充下级节点
        /// </summary>
        /// <param name="menu">菜单模型</param>
        /// <param name="allMenus">菜单模型集</param>
        private static void FillSubNodes(this Menu menu, Menu[] allMenus)
        {
            foreach (Menu subMenu in allMenus)
            {
                if (subMenu.ParentMenuId.HasValue && subMenu.ParentMenuId.Value == menu.Id)
                {
                    menu.SubNodes.Add(subMenu);
                    subMenu.ParentNode = menu;

                    FillSubNodes(subMenu, allMenus);
                }
            }
        }
        #endregion
    }
}
