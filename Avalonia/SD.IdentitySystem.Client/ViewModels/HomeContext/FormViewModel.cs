using Caliburn.Micro;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.Avalonia.Caliburn.Aspects;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SD.IdentitySystem.Client.ViewModels.HomeContext
{
    /// <summary>
    /// 表单视图模型
    /// </summary>
    public class FormViewModel : Screen
    {
        #region # 字段及构造器

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public FormViewModel(IWindowManager windowManager)
        {
            this._windowManager = windowManager;
        }

        #endregion

        #region # 属性

        #region 信息系统 —— InfoSystem SelectedInfoSystem
        /// <summary>
        /// 信息系统
        /// </summary>
        [DependencyProperty]
        public InfoSystemInfo SelectedInfoSystem { get; set; }
        #endregion

        #region 编号 —— string Number
        /// <summary>
        /// 编号
        /// </summary>
        [DependencyProperty]
        public string Number { get; set; }
        #endregion

        #region 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [DependencyProperty]
        public string Name { get; set; }
        #endregion

        #region 是否启用 —— bool? Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        [DependencyProperty]
        public bool? Enabled { get; set; }
        #endregion

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        [DependencyProperty]
        public string Description { get; set; }
        #endregion

        #region 信息系统列表 —— ObservableCollection<InfoSystem> InfoSystems
        /// <summary>
        /// 信息系统列表
        /// </summary>
        [DependencyProperty]
        public ObservableCollection<InfoSystemInfo> InfoSystems { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— override Task OnInitializeAsync(CancellationToken cancellationToken)
        /// <summary>
        /// 初始化
        /// </summary>
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return base.OnInitializeAsync(cancellationToken);
        }

        #endregion

        #region 赋值 —— void Fill()
        /// <summary>
        /// 赋值
        /// </summary>
        public void Fill()
        {
            this.Number = "编号";
            this.Name = "名称";
            this.Enabled = true;
            this.Description = "描述";
        }
        #endregion

        #region 提交 —— async void Submit()
        /// <summary>
        /// 提交
        /// </summary>
        public async void Submit()
        {
            this.Enabled = false;

            Trace.WriteLine(this.Number);
            Trace.WriteLine(this.Name);
            Trace.WriteLine(this.Enabled);
            Trace.WriteLine(this.Description);
            //SukiHost.ShowMessageBox(new MessageBoxModel("提示", this.Number));
            //SukiHost.ShowMessageBox(new MessageBoxModel("提示", this.Name));
            //SukiHost.ShowMessageBox(new MessageBoxModel("提示", this.Enabled));
            //SukiHost.ShowMessageBox(new MessageBoxModel("提示", this.Description));
        }
        #endregion 

        #endregion
    }
}
