
using SD.IdentitySystem.Client.Commons;

namespace SD.IdentitySystem.Client.ViewModels
{
    public class Test2ViewModel : DocumentBase
    {
        #region Overrides of ElementBase

        /// <summary>
        /// 标题
        /// </summary>
        public override string Title
        {
            get { return "选项卡1"; }
        }


        #endregion


        public void TestFlyout()
        {
            ElementManager.OpenFlyout<TestViewModel>();
        }
    }
}
