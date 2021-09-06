﻿using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace SD.IdentitySystem.WCF.Authentication.Windows
{
    /// <summary>
    /// WCF/Windows客户端身份认证行为
    /// </summary>
    public class AuthenticationBehavior : IEndpointBehavior
    {
        /// <summary>
        /// 适用客户端行为
        /// </summary>
        /// <param name="endpoint">服务终结点</param>
        /// <param name="clientRuntime">客户端运行时</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
#if NET46_OR_GREATER
            //添加消息拦截器
            clientRuntime.MessageInspectors.Add(new AuthenticationMessageInspector());
#endif
#if NETSTANDARD2_0_OR_GREATER
            //添加消息拦截器
            clientRuntime.ClientMessageInspectors.Add(new AuthenticationMessageInspector());
#endif

        }


        //没有用
        public void Validate(ServiceEndpoint endpoint) { }
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }
    }
}
