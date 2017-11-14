using SD.IdentitySystem.Client.Annotations;
using SD.IOC.Core.Mediator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 文档管理器基类
    /// </summary>
    public abstract class DocumentManagerBase : INotifyPropertyChanged, IDocumentManager
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected DocumentManagerBase()
        {
            this.Documents = new ObservableCollection<DocumentBase>();
        }
        #endregion

        #endregion

        #region # 索引器与属性

        #region 文档索引器 —— DocumentBase this[string url]
        /// <summary>
        /// 文档索引器
        /// </summary>
        /// <param name="url">完整类名</param>
        /// <returns>文档</returns>
        public DocumentBase this[string url]
        {
            get
            {
                if (this.Documents == null)
                {
                    this.Documents = new ObservableCollection<DocumentBase>();
                }

                DocumentBase document = this.Documents.SingleOrDefault(item => item.Url == url);

                if (document == null)
                {
                    Type type = Type.GetType(url);

                    if (type == null)
                    {
                        return null;
                    }

                    document = (DocumentBase)ResolveMediator.Resolve(type);

                    this.Documents.Add(document);
                }

                return document;
            }
        }
        #endregion

        #region 文档列表 —— ObservableCollection<DocumentBase> Documents
        /// <summary>
        /// 文档列表
        /// </summary>
        public ObservableCollection<DocumentBase> Documents { get; private set; }
        #endregion

        #endregion

        #region # 属性通知相关

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
