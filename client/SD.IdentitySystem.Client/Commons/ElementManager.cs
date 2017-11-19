using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SD.IOC.Core.Mediator;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 元素管理器静态工具类
    /// </summary>
    public static class ElementManager
    {
        #region # 字段及构造器

        /// <summary>
        /// 元素管理器实例
        /// </summary>
        private static readonly IElementManager _Current;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ElementManager()
        {
            _Current = ResolveMediator.Resolve<IElementManager>();
        }

        #endregion

        #region # 文档列表 —— static BindableCollection<DocumentBase> Documents
        /// <summary>
        /// 文档列表
        /// </summary>
        public static BindableCollection<DocumentBase> Documents
        {
            get { return _Current.Documents; }
        }
        #endregion

        #region # 打开文档 —— static DocumentBase OpenDocument(Type type)
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="type">文档类型</param>
        /// <returns>文档</returns>
        public static DocumentBase OpenDocument(Type type)
        {
            if (type != null)
            {
                return _Current.OpenDocument(type);
            }

            return null;
        }
        #endregion

        #region # 打开文档 —— static DocumentBase OpenDocument<T>()
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <returns>文档</returns>
        public static DocumentBase OpenDocument<T>() where T : DocumentBase
        {
            return _Current.OpenDocument<T>();
        }
        #endregion

        #region # 打开飞窗 —— static FlyoutBase OpenFlyout(Type type)
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <param name="type">飞窗类型</param>
        /// <returns>飞窗</returns>
        public static FlyoutBase OpenFlyout(Type type)
        {
            if (type != null)
            {
                return _Current.OpenFlyout(type);
            }

            return null;
        }
        #endregion

        #region # 打开飞窗 —— static T OpenFlyout<T>()
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <typeparam name="T">飞窗类型</typeparam>
        /// <returns>飞窗</returns>
        public static T OpenFlyout<T>() where T : FlyoutBase
        {
            return _Current.OpenFlyout<T>();
        }
        #endregion

        #region # 打开消息框 —— static async Task<MessageDialogResult> ShowMessage(...
        /// <summary>
        /// 打开消息框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="style">样式</param>
        /// <param name="config">配置</param>
        public static async Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings config = null)
        {
            MetroWindow currentView = (MetroWindow)Application.Current.MainWindow;

            if (currentView == null)
            {
                ViewAware viewAware = (ViewAware)_Current;
                currentView = (MetroWindow)viewAware.GetView();
            }

            return await currentView.ShowMessageAsync(title, message, style, config);
        }
        #endregion
    }
}
