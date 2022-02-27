using Caliburn.Micro;
using SD.Infrastructure.Membership;
using SD.Infrastructure.Xamarin.Caliburn.Aspects;
using SD.Infrastructure.Xamarin.Caliburn.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Mobile.ViewModels.Home
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class IndexViewModel : OneActiveConductorBase
    {
        /// <summary>
        /// 菜单可见性
        /// </summary>
        [DependencyProperty]
        public bool MenuVisible { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            this.MenuVisible = true;

            #region # 菜单示例数据

            LoginMenuInfo loginMenuInfo = new LoginMenuInfo
            {
                SubMenuInfos = new List<LoginMenuInfo>
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
                }
            };

            #endregion

            foreach (LoginMenuInfo rootMenuItem in loginMenuInfo.SubMenuInfos)
            {
                ObservableCollection<LoginMenuInfo> menuItems = new ObservableCollection<LoginMenuInfo>(rootMenuItem.SubMenuInfos);
                ChapterViewModel chapterViewModel = new ChapterViewModel(rootMenuItem.Name, rootMenuItem.Icon, menuItems);
                base.Items.Add(chapterViewModel);
            }

            return base.OnInitializeAsync(cancellationToken);
        }

        /// <summary>
        /// 已激活Screen事件
        /// </summary>
        protected override void OnActivationProcessed(IScreen screen, bool success)
        {
            this.MenuVisible = screen == null;
        }
    }
}
