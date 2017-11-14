
using System.Collections.ObjectModel;

namespace SD.IdentitySystem.Client
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentManager
    {
        ObservableCollection<DocumentBase> Documents { get; }
        DocumentBase this[string url] { get; }
    }
}
