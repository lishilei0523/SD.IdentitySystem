using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI
{
    /// <summary>
    /// EasyUI TreeGrid接口
    /// </summary>
    public interface ITreeGrid<T>
    {
        /// <summary>
        /// 类型
        /// <remarks>folder/pack</remarks>
        /// </summary>
        string type { get; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        ICollection<T> children { get; }
    }
}
