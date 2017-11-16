using Caliburn.Micro;
using System;

namespace SD.IdentitySystem.Client.Commons
{
    public static class ElementManager
    {

        public static IElementManager Current;


        #region 文档列表 —— static BindableCollection<DocumentBase> Documents
        /// <summary>
        /// 文档列表
        /// </summary>
        public static BindableCollection<DocumentBase> Documents
        {
            get { return Current.Documents; }
        }
        #endregion

        #region 飞窗 —— static FlyoutBase Flyout
        /// <summary>
        /// 飞窗
        /// </summary>
        public static FlyoutBase Flyout
        {
            get { return Current.Flyout; }
        }
        #endregion




        #region 获取文档 —— static DocumentBase GetDocument(Type type)
        /// <summary>
        /// 获取文档
        /// </summary>
        /// <returns>文档</returns>
        public static DocumentBase GetDocument(Type type)
        {
            return Current.GetDocument(type);
        }
        #endregion

        #region 获取文档 —— static DocumentBase GetDocument<T>()
        /// <summary>
        /// 获取文档
        /// </summary>
        /// <returns>文档</returns>
        public static DocumentBase GetDocument<T>() where T : DocumentBase
        {
            return Current.GetDocument<T>();
        }
        #endregion

        #region 打开飞窗 —— void OpenFlyout(Type type)
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <returns>飞窗</returns>
        public static void OpenFlyout(Type type)
        {
            Current.OpenFlyout(type);
        }
        #endregion

        #region 获取飞窗 —— static void OpenFlyout<T>()
        /// <summary>
        /// 获取飞窗
        /// </summary>
        /// <returns>飞窗</returns>
        public static void OpenFlyout<T>() where T : FlyoutBase
        {
            Current.OpenFlyout<T>();
        }
        #endregion
    }
}
