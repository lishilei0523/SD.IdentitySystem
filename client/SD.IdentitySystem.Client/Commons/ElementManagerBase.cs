using Caliburn.Micro;
using SD.Common.PoweredByLee;
using SD.IOC.Core.Mediator;
using System;
using System.Linq;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 元素管理器基类
    /// </summary>
    public abstract class ElementManagerBase : Screen, IElementManager
    {
        #region # 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        protected ElementManagerBase()
        {
            this.Documents = new BindableCollection<DocumentBase>();
        }
        #endregion

        #region # 属性

        #region 文档列表 —— BindableCollection<DocumentBase> Documents
        /// <summary>
        /// 文档列表
        /// </summary>
        private BindableCollection<DocumentBase> _documents;

        /// <summary>
        /// 文档列表
        /// </summary>
        public BindableCollection<DocumentBase> Documents
        {
            get { return this._documents; }
            set { this.Set(ref this._documents, value); }
        }
        #endregion

        #region 飞窗 —— FlyoutBase Flyout
        /// <summary>
        /// 飞窗
        /// </summary>
        private FlyoutBase _flyout;

        /// <summary>
        /// 飞窗
        /// </summary>
        public FlyoutBase Flyout
        {
            get { return this._flyout; }
            set { this.Set(ref this._flyout, value); }
        }
        #endregion

        #endregion

        #region # 方法

        #region 打开文档 —— void OpenDocument(Type type)
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="type">文档类型</param>
        public void OpenDocument(Type type)
        {
            //验证
            Assert.IsTrue(type.IsSubclassOf(typeof(DocumentBase)), "给定类型不是文档！");

            DocumentBase document = this.Documents.SingleOrDefault(x => x.GetType().FullName == type.FullName);

            if (document == null)
            {
                document = (DocumentBase)ResolveMediator.Resolve(type);
                this.Documents.Add(document);
            }

            document.Open();
        }
        #endregion

        #region 打开文档 —— void OpenDocument<T>()
        /// <summary>
        /// 打开文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        public void OpenDocument<T>() where T : DocumentBase
        {
            DocumentBase document = this.Documents.SingleOrDefault(x => x.GetType().FullName == typeof(T).FullName);

            if (document == null)
            {
                document = ResolveMediator.Resolve<T>();
                this.Documents.Add(document);
            }

            document.Open();
        }
        #endregion

        #region 打开飞窗 —— void OpenFlyout(Type type)
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <param name="type">飞窗类型</param>
        public void OpenFlyout(Type type)
        {
            //验证
            Assert.IsTrue(type.IsSubclassOf(typeof(FlyoutBase)), "给定类型不是飞窗！");

            this.Flyout = (FlyoutBase)ResolveMediator.Resolve(type);
            this.Flyout.Open();
        }
        #endregion

        #region 打开飞窗 —— void OpenFlyout<T>()
        /// <summary>
        /// 打开飞窗
        /// </summary>
        /// <typeparam name="T">飞窗类型</typeparam>
        public void OpenFlyout<T>() where T : FlyoutBase
        {
            this.Flyout = ResolveMediator.Resolve<T>();
            this.Flyout.Open();
        }
        #endregion

        #endregion
    }
}
