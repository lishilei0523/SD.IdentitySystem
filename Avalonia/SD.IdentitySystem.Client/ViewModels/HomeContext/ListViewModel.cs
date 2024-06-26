using Caliburn.Micro;
using SD.Infrastructure.Avalonia.Caliburn.Aspects;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Client.ViewModels.HomeContext
{
    /// <summary>
    /// 列表视图模型
    /// </summary>
    public class ListViewModel : Screen
    {
        #region # 字段及构造器

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ListViewModel(IWindowManager windowManager)
        {
            this._windowManager = windowManager;
        }

        #endregion

        #region # 属性

        #region 条码列表 —— ObservableCollection<string> BarcodeNos
        /// <summary>
        /// 条码列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<string> BarcodeNos { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— override Task OnInitializeAsync(CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            this.BarcodeNos = new ObservableCollection<string>
            {
                "697183836",
                "111623227",
                "671164796",
                "121478417",
                "746997643",
                "971622329",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
                "219262377",
            };

            return base.OnInitializeAsync(cancellationToken);
        }

        #endregion

        #endregion
    }
}
