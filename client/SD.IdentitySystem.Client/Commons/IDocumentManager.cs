
using System.Collections.ObjectModel;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 文档管理器接口
    /// </summary>
    public interface IDocumentManager
    {
        /// <summary>
        /// 文档索引器
        /// </summary>
        /// <param name="url">完整类名</param>
        /// <returns>文档</returns>
        DocumentBase this[string url] { get; }

        /// <summary>
        /// 文档列表
        /// </summary>
        ObservableCollection<DocumentBase> Documents { get; }
    }
}
