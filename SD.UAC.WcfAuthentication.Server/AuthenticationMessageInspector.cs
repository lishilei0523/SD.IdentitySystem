using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using SD.IOC.Core.Mediator;
using SD.UAC.Common;
using SD.UAC.IAppService.Interfaces;

namespace SD.UAC.WcfAuthentication.Server
{
    /// <summary>
    /// WCF服务端身份认证消息拦截器
    /// </summary>
    internal class AuthenticationMessageInspector : IDispatchMessageInspector
    {
        /// <summary>
        /// 身份认证服务接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 构造器
        /// </summary>
        public AuthenticationMessageInspector()
        {
            this._authenticationContract = ResolveMediator.Resolve<IAuthenticationContract>();
        }

        /// <summary>
        /// 接收请求后事件
        /// </summary>
        /// <param name="request">请求消息</param>
        /// <param name="channel">信道</param>
        /// <param name="instanceContext">WCF实例上下文</param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //获取消息头
            MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

            #region # 验证消息头

            if (!headers.Any(x => x.Name == Constants.WcfAuthHeaderName && x.Namespace == Constants.WcfAuthHeaderNamespace))
            {
                throw new NullReferenceException("身份认证消息头不存在，请检查程序！");
            }

            #endregion

            //读取消息头中的公钥
            Guid publishKey = headers.GetHeader<Guid>(Constants.WcfAuthHeaderName, Constants.WcfAuthHeaderNamespace);

            //认证
            this._authenticationContract.Authenticate(publishKey);

            return null;
        }

        /// <summary>
        /// 响应请求前事件
        /// </summary>
        /// <param name="reply">响应消息</param>
        /// <param name="correlationState">相关状态</param>
        public void BeforeSendReply(ref Message reply, object correlationState) { }
    }
}
