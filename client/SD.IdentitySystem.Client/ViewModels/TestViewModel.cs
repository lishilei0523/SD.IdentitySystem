using MahApps.Metro.Controls;
using SD.IdentitySystem.Client.Commons;
using System.Windows;

namespace SD.IdentitySystem.Client.ViewModels
{
    public class TestViewModel : FlyoutBase
    {
        public TestViewModel()
        {
            this.Position = Position.Right;
            this.Active = false;
            this.Margin = new Thickness(240, 30, 0, 0);
        }

        #region Overrides of ElementBase

        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "测试飞窗"; }
        }


        #endregion
    }
}
