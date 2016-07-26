using System;
using System.Collections.Generic;
using System.Linq;
using SD.UAC.Domain.EventSources.AuthorizationContext;
using ShSoft.Infrastructure.EntityBase;
using ShSoft.Infrastructure.EventBase.Mediator;

namespace SD.UAC.Domain.Entities
{
    /// <summary>
    /// 信息系统类别（字典）
    /// </summary>
    public class InfoSystemKind : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystemKind()
        {
            //初始化导航属性
            this.Menus = new HashSet<Menu>();
            this.Authorities = new HashSet<Authority>();
        }
        #endregion

        #region 02.创建系统类别构造器
        /// <summary>
        /// 创建系统类别构造器
        /// </summary>
        /// <param name="kindNo">类别编号</param>
        /// <param name="kindName">类别名称</param>
        public InfoSystemKind(string kindNo, string kindName)
            : this()
        {
            //验证参数
            base.CheckNumber(kindNo);
            base.CheckName(kindName);

            base.Number = kindNo;
            base.Name = kindName;
        }
        #endregion

        #endregion

        #region # 属性

        #region 导航属性 - 菜单集 —— ICollection<Menu> Menus
        /// <summary>
        /// 导航属性 - 菜单集
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; }
        #endregion

        #region 导航属性 - 权限集 —— ICollection<Authority> Authorities
        /// <summary>
        /// 导航属性 - 权限集
        /// </summary>
        public virtual ICollection<Authority> Authorities { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 创建菜单 —— void CreateMenu(Menu menu)
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public void CreateMenu(Menu menu)
        {
            #region # 验证参数

            if (menu == null)
            {
                throw new ArgumentNullException("menu", @"菜单不可为空！");
            }

            #endregion

            this.Menus.Add(menu);
        }
        #endregion

        #region 创建权限 —— void CreateAuthority(Authority authority)
        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="authority">权限</param>
        public void CreateAuthority(Authority authority)
        {
            #region # 验证参数

            if (authority == null)
            {
                throw new ArgumentNullException("authority", @"权限不可为空！");
            }

            #endregion

            this.Authorities.Add(authority);

            //挂起领域事件
            EventMediator.Suspend(new AuthorityCreatedEvent(this.Number, authority.Id));
        }
        #endregion

        #region 获取菜单 —— Menu GetMenu(Guid menuId)
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>菜单</returns>
        public Menu GetMenu(Guid menuId)
        {
            #region # 验证参数

            if (menuId == Guid.Empty)
            {
                throw new ArgumentNullException("menuId", @"菜单Id不可为空！");
            }
            if (this.Menus.All(x => x.Id != menuId))
            {
                throw new ArgumentOutOfRangeException("menuId", string.Format("Id为\"{0}\"的菜单不存在！", menuId));
            }

            #endregion

            return this.Menus.Single(x => x.Id == menuId);
        }
        #endregion

        #region 获取权限 —— Authority GetAuthority(Guid authorityId)
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authorityId">权限Id</param>
        /// <returns>权限</returns>
        public Authority GetAuthority(Guid authorityId)
        {
            #region # 验证参数

            if (authorityId == Guid.Empty)
            {
                throw new ArgumentNullException("authorityId", @"权限Id不可为空！");
            }
            if (this.Authorities.All(x => x.Id != authorityId))
            {
                throw new ArgumentOutOfRangeException("authorityId", string.Format("Id为\"{0}\"的权限不存在！", authorityId));
            }

            #endregion

            return this.Authorities.Single(x => x.Id == authorityId);
        }
        #endregion

        #region 删除菜单 —— void RemoveMenu(Menu menu)
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu">菜单</param>
        public void RemoveMenu(Menu menu)
        {
            #region # 验证参数

            if (menu == null)
            {
                throw new ArgumentNullException("menu", @"菜单不可为空！");
            }

            #endregion

            //清空关系
            foreach (Authority authority in menu.Authorities.ToArray())
            {
                menu.Authorities.Remove(authority);
            }

            //递归
            foreach (Menu subMenu in menu.SubNodes.ToArray())
            {
                this.RemoveMenu(subMenu);
            }

            //删除菜单
            this.Menus.Remove(menu);
        }

        #endregion

        #region 删除权限 —— void RemoveAuthority(Authority authority)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="authority">权限</param>
        public void RemoveAuthority(Authority authority)
        {
            #region # 验证参数

            if (authority == null)
            {
                throw new ArgumentNullException("authority", @"权限不可为空！");
            }

            #endregion

            foreach (Menu menu in authority.MenuLeaves)
            {
                menu.Authorities.Remove(authority);
            }
            foreach (Role role in authority.Roles)
            {
                role.Authorities.Remove(authority);
            }

            this.Authorities.Remove(authority);
        }
        #endregion

        #endregion
    }
}
