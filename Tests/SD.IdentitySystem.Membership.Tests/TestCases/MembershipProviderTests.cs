using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;

namespace SD.IdentitySystem.Membership.Tests.TestCases
{
    /// <summary>
    /// Membership提供者测试
    /// </summary>
    [TestClass]
    public class MembershipProviderTests
    {
        /// <summary>
        /// 测试获取登录信息
        /// </summary>
        [TestMethod]
        public void TestGetLoginInfo()
        {
            LoginInfo loginInfoInput = new LoginInfo(CommonConstants.AdminLoginId, "超级管理员", Guid.Empty);
            AppDomain.CurrentDomain.SetData(GlobalSetting.ApplicationId, loginInfoInput);

            LoginInfo loginInfoOutput = MembershipMediator.GetLoginInfo();

            Assert.AreEqual(loginInfoInput.LoginId, loginInfoOutput.LoginId);
        }
    }
}
