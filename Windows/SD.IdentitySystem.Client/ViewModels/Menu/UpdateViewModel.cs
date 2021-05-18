﻿using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Menu
{
    /// <summary>
    /// 菜单修改视图模型
    /// </summary>
    public class UpdateViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 角色服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public UpdateViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 菜单Id —— Guid MenuId
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }
        #endregion

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string InfoSystemName { get; set; }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; set; }
        #endregion

        #region 上级菜单名称 —— string ParentMenuName
        /// <summary>
        /// 上级菜单名称
        /// </summary>
        public string ParentMenuName { get; set; }
        #endregion

        #region 菜单名称 —— string MenuName
        /// <summary>
        /// 菜单名称
        /// </summary>
        [DependencyProperty]
        public string MenuName { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [DependencyProperty]
        public string Url { get; set; }
        #endregion

        #region 路径 —— string Path
        /// <summary>
        /// 路径
        /// </summary>
        [DependencyProperty]
        public string Path { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        [DependencyProperty]
        public string Icon { get; set; }
        #endregion

        #region 排序 —— int? Sort
        /// <summary>
        /// 排序
        /// </summary>
        [DependencyProperty]
        public int? Sort { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— async Task Load(Guid menuId)
        /// <summary>
        /// 加载
        /// </summary>
        public async Task Load(Guid menuId)
        {
            MenuInfo menu = await Task.Run(() => this._authorizationContract.GetMenu(menuId));
            MenuInfo parentMenu = menu.ParentMenuId.HasValue
                ? await Task.Run(() => this._authorizationContract.GetMenu(menu.ParentMenuId.Value))
                : null;

            this.MenuId = menu.Id;
            this.InfoSystemName = menu.InfoSystemInfo.Name;
            this.ApplicationType = menu.ApplicationType;
            this.ParentMenuName = parentMenu?.Name;
            this.MenuName = menu.Name;
            this.Url = menu.Url;
            this.Path = menu.Path;
            this.Icon = menu.Icon;
            this.Sort = menu.Sort;
        }
        #endregion


        //Actions

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.MenuName))
            {
                MessageBox.Show("菜单名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!this.Sort.HasValue)
            {
                MessageBox.Show("排序不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._authorizationContract.UpdateMenu(this.MenuId, this.MenuName, this.Sort.Value, this.Url, this.Path, this.Icon));

            base.TryClose(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
