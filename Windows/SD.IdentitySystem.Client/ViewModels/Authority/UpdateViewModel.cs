using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.WPF.Caliburn.Aspects;
using SD.Infrastructure.WPF.Caliburn.Base;
using SD.Infrastructure.WPF.Extensions;
using System;
using System.ServiceModel.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.Authority
{
    /// <summary>
    /// 权限修改视图模型
    /// </summary>
    public class UpdateViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public UpdateViewModel(ServiceProxy<IAuthorizationContract> authorizationContract)
        {
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

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; set; }
        #endregion

        #region 权限Id —— Guid AuthorityId
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid AuthorityId { get; set; }
        #endregion

        #region 权限名称 —— string AuthorityName
        /// <summary>
        /// 权限名称
        /// </summary>
        [DependencyProperty]
        public string AuthorityName { get; set; }
        #endregion

        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        [DependencyProperty]
        public string AuthorityPath { get; set; }
        #endregion

        #region 英文名称 —— string EnglishName
        /// <summary>
        /// 英文名称
        /// </summary>
        [DependencyProperty]
        public string EnglishName { get; set; }
        #endregion

        #region 程序集名称 —— string AssemblyName
        /// <summary>
        /// 程序集名称
        /// </summary>
        [DependencyProperty]
        public string AssemblyName { get; set; }
        #endregion

        #region 命名空间 —— string Namespace
        /// <summary>
        /// 命名空间
        /// </summary>
        [DependencyProperty]
        public string Namespace { get; set; }
        #endregion

        #region 类名 —— string ClassName
        /// <summary>
        /// 类名
        /// </summary>
        [DependencyProperty]
        public string ClassName { get; set; }
        #endregion

        #region 方法名 —— string MethodName
        /// <summary>
        /// 方法名
        /// </summary>
        [DependencyProperty]
        public string MethodName { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [DependencyProperty]
        public string Description { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— async Task Load(Guid authorityId)
        /// <summary>
        /// 加载
        /// </summary>
        public async Task Load(Guid authorityId)
        {
            AuthorityInfo authority = await Task.Run(() => this._authorizationContract.Channel.GetAuthority(authorityId));

            this.InfoSystemName = authority.InfoSystemInfo.Name;
            this.AuthorityId = authority.Id;
            this.AuthorityName = authority.Name;
            this.AuthorityPath = authority.AuthorityPath;
            this.EnglishName = authority.EnglishName;
            this.AssemblyName = authority.AssemblyName;
            this.Namespace = authority.Namespace;
            this.ClassName = authority.ClassName;
            this.MethodName = authority.MethodName;
            this.Description = authority.Description;
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

            if (string.IsNullOrWhiteSpace(this.AuthorityName))
            {
                MessageBox.Show("权限名称不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._authorizationContract.Channel.UpdateAuthority(this.AuthorityId, this.AuthorityName, this.AuthorityPath, this.EnglishName, this.AssemblyName, this.Namespace, this.ClassName, this.MethodName, this.Description));

            base.TryClose(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}
