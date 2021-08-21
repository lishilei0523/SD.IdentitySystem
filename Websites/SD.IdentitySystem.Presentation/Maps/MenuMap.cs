using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IPresentation.Models;
using SD.Toolkits.EasyUI;
using SD.Toolkits.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Maps
{
    /// <summary>
    /// 菜单映射工具类
    /// </summary>
    public static class MenuMap
    {
        #region # 菜单模型映射 —— static Menu ToModel(this MenuInfo...
        /// <summary>
        /// 菜单模型映射
        /// </summary>
        public static Menu ToModel(this MenuInfo menuInfo)
        {
            Menu menu = menuInfo.Map<MenuInfo, Menu>();
            menu.SystemName = menuInfo.InfoSystemInfo.Name;
            menu.ApplicationType = menuInfo.ApplicationType.GetEnumMember();

            return menu;
        }
        #endregion

        #region # 菜单EasyUI树节点映射 —— static Node ToNode(this Menu menu)
        /// <summary>
        /// 菜单EasyUI树节点映射
        /// </summary>
        public static Node ToNode(this Menu menu)
        {
            var attributes = new
            {
                href = menu.Url,
                isLeaf = menu.IsLeaf
            };

            return new Node(menu.Id, menu.Name, "open", false, attributes);
        }
        #endregion

        #region # 菜单EasyUI树集合映射 —— static ICollection<Node> ToTree(this IEnumerable<Menu...
        /// <summary>
        /// 菜单EasyUI树集合映射
        /// </summary>
        public static ICollection<Node> ToTree(this IEnumerable<Menu> menus, Guid? parentId)
        {
            //验证
            menus = menus == null ? new Menu[0] : menus.ToArray();

            //声明容器
            ICollection<Node> tree = new HashSet<Node>();

            //判断父级菜单Id是否为null
            if (!parentId.HasValue)
            {
                //从根级开始遍历
                foreach (Menu menu in menus.Where(x => x.IsRoot))
                {
                    Node node = menu.ToNode();

                    tree.Add(node);

                    node.children = menus.ToTree(node.id);
                }
            }
            else
            {
                //从给定Id向下遍历
                foreach (Menu menu in menus.Where(x => x.ParentMenuId != null && x.ParentMenuId == parentId.Value))
                {
                    Node node = menu.ToNode();

                    tree.Add(node);

                    node.children = menus.ToTree(node.id);
                }
            }

            return tree;
        }
        #endregion

        #region # 菜单EasyUI TreeGrid映射 —— static IEnumerable<Menu> ToTreeGrid(this...
        /// <summary>
        /// 菜单EasyUI TreeGrid映射
        /// </summary>
        public static IEnumerable<Menu> ToTreeGrid(this IEnumerable<Menu> menus)
        {
            Menu[] allMenus = menus?.ToArray() ?? new Menu[0];
            foreach (Menu menu in allMenus)
            {
                menu.FillChildren(allMenus);
            }

            return allMenus.Where(x => x.IsRoot);
        }
        #endregion


        //Private

        #region # 填充子节点 —— static void FillChildren(this Menu menu...
        /// <summary>
        /// 填充子节点
        /// </summary>
        /// <param name="menu">菜单模型</param>
        /// <param name="allMenus">菜单模型集</param>
        private static void FillChildren(this Menu menu, Menu[] allMenus)
        {
            foreach (Menu subMenu in allMenus)
            {
                if (subMenu.ParentMenuId.HasValue && subMenu.ParentMenuId.Value == menu.Id)
                {
                    menu.children.Add(subMenu);
                    menu.type = menu.IsLeaf ? "pack" : "folder";
                    subMenu.ParentMenuId = null;

                    FillChildren(subMenu, allMenus);
                }
            }
        }
        #endregion
    }
}
