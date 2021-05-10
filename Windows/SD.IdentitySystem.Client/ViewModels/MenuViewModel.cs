using Caliburn.Micro;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models.Outputs;
using SD.Infrastructure.WPF.Aspects;
using SD.Infrastructure.WPF.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 菜单视图模型
    /// </summary>
    public class MenuViewModel : Screen
    {
        private readonly IMenuPresenter _menuPresenter;

        /// <summary>Creates an instance of the screen.</summary>
        public MenuViewModel(IMenuPresenter menuPresenter)
        {
            this._menuPresenter = menuPresenter;
        }

        [DependencyProperty]
        public ObservableCollection<Menu> Menus { get; set; }



        public async Task GetMenuTreeGrid()
        {
            LoadingIndicator.Suspend();

            IEnumerable<Menu> menus = await Task.Run(() =>
            {
                Thread.Sleep(3000);
                return this._menuPresenter.GetMenuTreeGrid(null, null);
            });
            LoadingIndicator.Dispose();

            this.Menus = new ObservableCollection<Menu>(menus);
        }
    }
}
