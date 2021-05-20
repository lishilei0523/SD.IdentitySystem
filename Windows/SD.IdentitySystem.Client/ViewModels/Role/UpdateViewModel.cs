using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Presentors;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using SD.Infrastructure.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Role
{
    /// <summary>
    /// 角色修改视图模型
    /// </summary>
    public class UpdateViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限呈现器
        /// </summary>
        private readonly AuthorityPresenter _authorityPresenter;

        /// <summary>
        /// 角色服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public UpdateViewModel(AuthorityPresenter authorityPresenter, IAuthorizationContract authorizationContract)
        {
            this._authorityPresenter = authorityPresenter;
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 属性

        #region 信息系统名称 —— string InfoSystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string InfoSystemName { get; set; }
        #endregion

        #region 角色Id —— Guid RoleId
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
        #endregion

        #region 角色名称 —— string RoleName
        /// <summary>
        /// 角色名称
        /// </summary>
        [DependencyProperty]
        public string RoleName { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [DependencyProperty]
        public string Description { get; set; }
        #endregion

        #region 权限数据项列表 —— ObservableCollection<Item> AuthorityItems
        /// <summary>
        /// 权限数据项列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<Item> AuthorityItems { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— async Task Load(Guid roleId)
        /// <summary>
        /// 加载
        /// </summary>
        public async Task Load(Guid roleId)
        {
            RoleInfo role = await Task.Run(() => this._authorizationContract.GetRole(roleId));
            ICollection<Item> authorityItems = await Task.Run(() => this._authorityPresenter.GetRoleAuthorityItems(roleId));

            this.InfoSystemName = role.InfoSystemInfo.Name;
            this.RoleId = role.Id;
            this.RoleName = role.Name;
            this.Description = role.Description;
            this.AuthorityItems = new ObservableCollection<Item>(authorityItems);
        }
        #endregion


        //Privates

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.RoleName))
            {
                MessageBox.Show("角色名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            IEnumerable<Guid> authorityIds = this.AuthorityItems.Where(x => x.IsChecked == true).Select(x => x.Id);
            await Task.Run(() => this._authorizationContract.UpdateRole(this.RoleId, this.RoleName, this.Description, authorityIds));

            base.TryClose(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
