using SD.IdentitySystem.AppService.Implements;
using System;
using System.ServiceModel;

namespace SD.IdentitySystem.WindowsHost
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        private readonly ServiceHost _authenticationContractHost;
        private readonly ServiceHost _authorizationContractHost;
        private readonly ServiceHost _userContractHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._authenticationContractHost = new ServiceHost(typeof(AuthenticationContract));
            this._authorizationContractHost = new ServiceHost(typeof(AuthorizationContract));
            this._userContractHost = new ServiceHost(typeof(UserContract));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this._authenticationContractHost.Open();
            this._authorizationContractHost.Open();
            this._userContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._authenticationContractHost.Close();
            this._authorizationContractHost.Close();
            this._userContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
