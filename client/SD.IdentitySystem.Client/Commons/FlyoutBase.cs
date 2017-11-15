using MahApps.Metro.Controls;
using System.Windows;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 飞窗基类
    /// </summary>
    public abstract class FlyoutBase
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected FlyoutBase() { }

        /// <summary>
        /// 创建飞窗
        /// </summary>
        protected FlyoutBase(string title, Position position, Thickness margin)
        {
            this.Title = title;
            this.Position = position;
            this.Margin = margin;
            this.IsOpen = true;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// 外边距
        /// </summary>
        public Thickness Margin { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public bool IsOpen { get; set; }
    }
}
