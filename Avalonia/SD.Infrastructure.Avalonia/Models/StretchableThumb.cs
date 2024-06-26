using Avalonia.Controls.Primitives;
using SD.Infrastructure.Avalonia.Constants;

namespace SD.Infrastructure.Avalonia.Models
{
    /// <summary>
    /// 可伸缩拖动控件
    /// </summary>
    public class StretchableThumb : Thumb
    {
        /// <summary>
        /// 拖拽方向
        /// </summary>
        public DragDirection DragDirection { get; set; }
    }
}
