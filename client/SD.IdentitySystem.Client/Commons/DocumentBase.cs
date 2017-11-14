using SD.IdentitySystem.Client.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 文档基类
    /// </summary>
    public abstract class DocumentBase : INotifyPropertyChanged
    {
        #region 属性

        #region 标题 —— abstract string Title
        /// <summary>
        /// 标题
        /// </summary>
        public abstract string Title { get; }
        #endregion

        #region 地址 —— abstract string Url
        /// <summary>
        /// 地址
        /// </summary>
        public abstract string Url { get; }
        #endregion

        #region 文档管理器 —— abstract IDocumentManager DocumentManager
        /// <summary>
        /// 文档管理器
        /// </summary>
        public abstract IDocumentManager DocumentManager { get; }
        #endregion

        #region 关闭命令 —— ICommand CloseCommand
        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(x => this.Close());
            }
        }
        #endregion

        #region 是否活动 —— bool Active
        /// <summary>
        /// 是否活动
        /// </summary>
        public bool Active { get; set; }
        #endregion

        #region 是否选中 —— bool Selected
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 打开 —— void Open()
        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            if (this.DocumentManager.Documents.Contains(this))
            {
                this.Active = true;
            }
            else
            {
                this.DocumentManager.Documents.Add(this);
            }
        }
        #endregion

        #region 关闭 —— void Close()
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (this.DocumentManager.Documents.Contains(this))
            {
                this.DocumentManager.Documents.Remove(this);
            }
        }
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
