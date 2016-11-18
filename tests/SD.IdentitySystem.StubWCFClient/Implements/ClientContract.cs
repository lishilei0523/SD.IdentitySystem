using System.ServiceModel;
using SD.IdentitySystem.StubServer.Interfaces;
using SD.IdentitySystem.StubWCFClient.Interfaces;

namespace SD.IdentitySystem.StubWCFClient.Implements
{
    /// <summary>
    /// WCF客户端契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClientContract : IClientContract
    {

        private readonly IServerContract _serverContract;

        public ClientContract(IServerContract serverContract)
        {
            this._serverContract = serverContract;
        }

        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <returns>消息头</returns>
        public string GetHeader()
        {
            string header = this._serverContract.GetHeader();

            return header;
        }
    }
}