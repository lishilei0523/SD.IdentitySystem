using Caliburn.Micro;
using SD.IOC.Core.Mediator;
using System;

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

        #region # 获取文档 —— static DocumentBase GetDocument(Type type)
        /// <summary>
        /// 获取文档
        /// </summary>
        /// <returns>文档</returns>
        public static DocumentBase GetDocument(Type type)
        {
            return _Current.GetDocument(type);
        }
        #endregion

        #region # 获取文档 —— static DocumentBase GetDocument<T>()
        /// <summary>
        /// 获取文档
        /// </summary>
        /// <returns>文档</returns>
        public static DocumentBase GetDocument<T>() where T : DocumentBase
        {
            return _Current.GetDocument<T>();
        }
        #endregion

        #region # 打开飞窗 —— void OpenFlyout(Type type)
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <param name="type">飞窗类型</param>
        public static void OpenFlyout(Type type)
        {
            _Current.OpenFlyout(type);
        }
        #endregion

        #region # 获取飞窗 —— static void OpenFlyout<T>()
        /// <summary>
        /// 获取飞窗
        /// </summary>
        /// <typeparam name="T">飞窗类型</typeparam>
        public static void OpenFlyout<T>() where T : FlyoutBase
        {
            _Current.OpenFlyout<T>();
        }
        #endregion
    }
}
