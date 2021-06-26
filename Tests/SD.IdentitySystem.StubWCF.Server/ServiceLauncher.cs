using SD.IdentitySystem.StubWCF.Server.Implements;
using System;
using System.ServiceModel;

namespace SD.IdentitySystem.StubWCF.Server
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        private readonly ServiceHost _serverContractHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._serverContractHost = new ServiceHost(typeof(ServerContract));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this._serverContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._serverContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
