﻿using SD.Common;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.InfoSystem
{
    /// <summary>
    /// 信息系统创建视图模型
    /// </summary>
    public class AddViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public AddViewModel(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DependencyProperty]
        public string InfoSystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        [DependencyProperty]
        public string InfoSystemName { get; set; }
        #endregion

        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        [DependencyProperty]
        public string AdminLoginId { get; set; }
        #endregion

        #region 应用程序类型 —— ApplicationType? ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [DependencyProperty]
        public ApplicationType? ApplicationType { get; set; }
        #endregion

        #region 应用程序类型字典 —— IDictionary<string, string> ApplicationTypes
        /// <summary>
        /// 应用程序类型字典
        /// </summary>
        [DependencyProperty]
        public IDictionary<string, string> ApplicationTypes { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— override void OnInitialize()
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialize()
        {
            this.ApplicationTypes = typeof(ApplicationType).GetEnumMembers();
        }
        #endregion

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.InfoSystemNo))
            {
                MessageBox.Show("信息系统编号不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.InfoSystemName))
            {
                MessageBox.Show("信息系统名称不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.AdminLoginId))
            {
                MessageBox.Show("系统管理员账号不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!this.ApplicationType.HasValue)
            {
                MessageBox.Show("应用程序类型不可为空！", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._authorizationContract.CreateInfoSystem(this.InfoSystemNo, this.InfoSystemName, this.AdminLoginId, this.ApplicationType.Value));

            base.TryClose(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
