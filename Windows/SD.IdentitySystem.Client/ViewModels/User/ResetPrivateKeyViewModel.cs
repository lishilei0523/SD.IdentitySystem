﻿using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Base;
using SD.Infrastructure.WPF.Extensions;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels.User
{
    /// <summary>
    /// 用户重置私钥视图模型
    /// </summary>
    public class ResetPrivateKeyViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ResetPrivateKeyViewModel(IUserContract userContract)
        {
            this._userContract = userContract;
        }

        #endregion

        #region # 属性

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginId { get; set; }
        #endregion

        #region 私钥 —— string PrivateKey
        /// <summary>
        /// 私钥
        /// </summary>
        [DependencyProperty]
        public string PrivateKey { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Initializations

        #region 加载 —— async Task Load(string loginId)
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="loginId">用户名</param>
        public async Task Load(string loginId)
        {
            UserInfo user = await Task.Run(() => this._userContract.GetUser(loginId));
            this.LoginId = loginId;
            this.PrivateKey = user.PrivateKey;
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

            if (string.IsNullOrWhiteSpace(this.PrivateKey))
            {
                MessageBox.Show("私钥不可为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            this.Busy();

            await Task.Run(() => this._userContract.SetPrivateKey(this.LoginId, this.PrivateKey));

            base.TryClose(true);
            this.Idle();
        }
        #endregion

        #endregion
    }
}