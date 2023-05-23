using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Infrastructure;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;
using System.Configuration;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.IdentitySystem.Membership.Tests.TestCases
{
    /// <summary>
    /// Membership提供者测试
    /// </summary>
    [TestClass]
    public class MembershipProviderTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            FrameworkSection.Initialize(configuration);
#endif
        }
        #endregion

        #region # 测试获取登录信息 —— void TestGetLoginInfo()
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
        #endregion
    }
}
