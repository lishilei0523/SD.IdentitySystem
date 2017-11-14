
using SD.IdentitySystem.Client.Commons;

namespace SD.IdentitySystem.Client.ViewModels
{
    public class TestViewModel : DocumentBase
    {
        private readonly IDocumentManager _documentManager;

        public TestViewModel(IDocumentManager documentManager)
        {
            this._documentManager = documentManager;
        }


        #region Overrides of DocumentBase

        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "新建"; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public override string Url
        {
            get { return this.GetType().FullName; }
        }

        /// <summary>
        /// 文档管理器
        /// </summary>
        public override IDocumentManager DocumentManager
        {
            get { return this._documentManager; }
        }

        #endregion
    }
}
