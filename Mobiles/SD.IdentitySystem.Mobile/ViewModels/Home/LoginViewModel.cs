using Caliburn.Micro.Xamarin.Forms;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using SD.Infrastructure.Xamarin.Caliburn.Aspects;
using SD.Infrastructure.Xamarin.Caliburn.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SD.IdentitySystem.Mobile.ViewModels.Home
{
    /// <summary>
    /// 登录页视图模型
    /// </summary>
    public class LoginViewModel : ScreenBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 身份认证服务契约接口
        /// </summary>
        private readonly ServiceProxy<IAuthenticationContract> _authenticationContract;

        /// <summary>
        /// 导航服务
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public LoginViewModel(ServiceProxy<IAuthenticationContract> authenticationContract, INavigationService navigationService)
        {
            this._authenticationContract = authenticationContract;
            this._navigationService = navigationService;
        }

        #endregion

        #region # 属性

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        [DependencyProperty]
        public string LoginId { get; set; }
        #endregion

        #region 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [DependencyProperty]
        public string Password { get; set; }
        #endregion

        #endregion

        #region # 方法

        //Actions

        #region 登录 —— async void Login()
        /// <summary>
        /// 登录
        /// </summary>
        public async void Login()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.LoginId))
            {
                await this.Alert("用户名不可为空！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                await this.Alert("密码不可为空！");
                return;
            }

            #endregion

            this.Busy();

            LoginInfo loginInfo = await Task.Run(() => this._authenticationContract.Channel.Login(this.LoginId, this.Password));
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);

            this.Idle();

            TextToSpeech.SpeakAsync("登录成功");
            this.Toast($"登录成功！{loginInfo.LoginId}");

            IList<LoginMenuInfo> menuItems = new List<LoginMenuInfo>
            {
                new LoginMenuInfo
                {
                    Name = "在线模式",
                    Icon = "online",
                    SubMenuInfos = new List<LoginMenuInfo>
                    {
                        new LoginMenuInfo
                        {
                            Name = "单据管理",
                            SubMenuInfos = new List<LoginMenuInfo>
                            {
                                new LoginMenuInfo
                                {
                                    Name = "收货管理",
                                    Icon = "warehouse_in",
                                },
                                new LoginMenuInfo
                                {
                                    Name = "发货管理",
                                    Icon = "warehouse_out",
                                    Url = "url2"
                                }
                            }
                        },
                        new LoginMenuInfo
                        {
                            Name = "托盘管理",
                            Icon = "pallet_stacking",
                            Url = "url3",
                            SubMenuInfos = new List<LoginMenuInfo>
                            {
                                new LoginMenuInfo
                                {
                                    Name = "装托",
                                    Icon = "pallet_stacking",
                                },
                                new LoginMenuInfo
                                {
                                    Name = "拼托",
                                    Icon = "pallet_join"
                                },
                                new LoginMenuInfo
                                {
                                    Name = "拆托",
                                    Icon = "pallet_breaking"
                                },
                                new LoginMenuInfo
                                {
                                    Name = "换托/货",
                                    Icon = "pallet_change"
                                }
                            }
                        }
                    }
                },
                new LoginMenuInfo
                {
                    Name = "离线模式",
                    Icon = "offline",
                    SubMenuInfos = new List<LoginMenuInfo>
                    {
                        new LoginMenuInfo
                        {
                            Name = "单据管理",
                            SubMenuInfos = new List<LoginMenuInfo>
                            {
                                new LoginMenuInfo
                                {
                                    Name = "收货管理",
                                    Icon = "warehouse_in",
                                    Url = "url2"
                                },
                                new LoginMenuInfo
                                {
                                    Name = "发货管理",
                                    Icon = "warehouse_out",
                                    Url = "url2"
                                }
                            }
                        }
                    }
                },
                new LoginMenuInfo
                {
                    Name = "个人中心",
                    Icon = "center",
                    SubMenuInfos = new List<LoginMenuInfo>
                    {
                        new LoginMenuInfo
                        {
                            Name = "个人中心",
                            SubMenuInfos = new List<LoginMenuInfo>
                            {
                                new LoginMenuInfo
                                {
                                    Name = "设备注册",
                                    Icon = "device_info",
                                },
                                new LoginMenuInfo
                                {
                                    Name = "修改密码",
                                    Icon = "stock_taking_order",
                                }
                            }
                        }
                    }
                }
            };
            this._navigationService
                .For<IndexViewModel>()
                .WithParam(x => x.MenuItems, new ObservableCollection<LoginMenuInfo>(menuItems))
                .Navigate(false);
        }
        #endregion

        #endregion

        //Finalize

        #region 失去活动 —— override Task OnDeactivateAsync(bool close...
        /// <summary>
        /// 失去活动
        /// </summary>
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            if (close)
            {
                this._authenticationContract.Dispose();
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }
        #endregion
    }
}
