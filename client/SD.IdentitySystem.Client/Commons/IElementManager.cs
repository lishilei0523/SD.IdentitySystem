using Caliburn.Micro;
using System;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 元素管理器接口
    /// </summary>
    public interface IElementManager
    {
        /// <summary>
        /// 文档列表
        /// </summary>
        BindableCollection<DocumentBase> Documents { get; }

        /// <summary>
        /// 飞窗
        /// </summary>
        FlyoutBase Flyout { get; }

        /// <summary>
        /// 获取文档
        /// </summary>
        /// <returns>文档</returns>
        DocumentBase GetDocument(Type type);

        /// <summary>
        /// 获取文档
        /// </summary>
        /// <returns>文档</returns>
        DocumentBase GetDocument<T>() where T : DocumentBase;

        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <param name="type">飞窗类型</param>
        void OpenFlyout(Type type);

        /// <summary>
        /// 获取飞窗
        /// </summary>
        /// <typeparam name="T">飞窗类型</typeparam>
        void OpenFlyout<T>() where T : FlyoutBase;
    }
}
