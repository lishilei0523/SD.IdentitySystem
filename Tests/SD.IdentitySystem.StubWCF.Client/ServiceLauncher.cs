using SD.IdentitySystem.StubWCF.Client.Implements;
using System;
using System.ServiceModel;

namespace SD.IdentitySystem.StubWCF.Client
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        private readonly ServiceHost _clientContractHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._clientContractHost = new ServiceHost(typeof(ClientContract));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this._clientContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._clientContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
