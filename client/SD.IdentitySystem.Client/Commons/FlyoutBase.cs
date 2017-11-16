using MahApps.Metro.Controls;
using System.Windows;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 飞窗基类
    /// </summary>
    public abstract class FlyoutBase : ElementBase
    {
        #region # 属性

        #region 位置 —— Position Position
        /// <summary>
        /// 位置
        /// </summary>
        private Position _position;

        /// <summary>
        /// 位置
        /// </summary>
        public Position Position
        {
            get { return this._position; }
            protected set { this.Set(ref this._position, value); }
        }
        #endregion

        #region 外边距 —— Thickness Margin
        /// <summary>
        /// 外边距
        /// </summary>
        private Thickness _margin;

        /// <summary>
        /// 外边距
        /// </summary>
        public Thickness Margin
        {
            get { return this._margin; }
            protected set { this.Set(ref this._margin, value); }
        }
        #endregion

        #endregion

        #region # 方法

        #region 打开 —— override void Open()
        /// <summary>
        /// 打开
        /// </summary>
        public override void Open()
        {
            this.Active = true;
        }
        #endregion

        #region 关闭 —— override void Close()
        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            this.Active = false;
        }
        #endregion

        #endregion
    }
}
