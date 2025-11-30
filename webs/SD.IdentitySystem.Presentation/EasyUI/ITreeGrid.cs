using System.Collections.Generic;

namespace SD.IdentitySystem.Presentation.EasyUI
{
    /// <summary>
    /// EasyUI树形表格接口
    /// </summary>
    public interface ITreeGrid<T>
    {
        /// <summary>
        /// 类型
        /// </summary>
        /// <remarks>folder|pack</remarks>
        string type { get; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        ICollection<T> children { get; }
    }
}
