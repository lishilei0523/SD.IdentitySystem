using Caliburn.Micro;
using System.Windows.Input;

namespace SD.IdentitySystem.Client
{
    public abstract class DocumentBase : PropertyChangedBase
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

        /// <summary>
        /// 文档管理器
        /// </summary>
        public abstract IDocumentManager DocumentManager { get; }

        #region 关闭命令 —— ICommand CloseCommand
        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(x => this.Close(), x => true);
            }
        }
        #endregion

        #region 是否活动 —— bool Active
        /// <summary>
        /// 是否活动
        /// </summary>
        private bool _active;

        /// <summary>
        /// 是否活动
        /// </summary>
        public bool Active
        {
            get { return this._active; }
            set { this.Set(ref this._active, value); }
        }
        #endregion

        #region 是否选中 —— bool Selected
        /// <summary>
        /// 是否选中
        /// </summary>
        private bool _selected;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected
        {
            get { return this._selected; }
            set { this.Set(ref this._selected, value); }
        }
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
    }
}
