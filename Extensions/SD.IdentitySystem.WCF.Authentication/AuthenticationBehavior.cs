using System.Collections.ObjectModel;
#if NET40_OR_GREATER
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
#endif

namespace SD.IdentitySystem.WCF.Authentication
{
    /// <summary>
    /// WCF客户端/服务端身份认证行为
    /// </summary>
    public class AuthenticationBehavior : IServiceBehavior, System.ServiceModel.Description.IEndpointBehavior
    {
        /// <summary>
        /// 适用身份认证服务端行为
        /// </summary>
        /// <param name="serviceDescription">服务描述</param>
        /// <param name="serviceHostBase">服务主机</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher dispatcher = (ChannelDispatcher)channelDispatcherBase;
                foreach (EndpointDispatcher endpoint in dispatcher.Endpoints)
                {
                    if (!endpoint.IsSystemEndpoint)
                    {
                        endpoint.DispatchRuntime.MessageInspectors.Add(new AuthenticationMessageInspector());
                    }
                }
            }
        }

        /// <summary>
        /// 适用身份认证客户端行为
        /// </summary>
        /// <param name="endpoint">服务终结点</param>
        /// <param name="clientRuntime">客户端运行时</param>
        public void ApplyClientBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
#if NET40_OR_GREATER
            //添加消息拦截器
            clientRuntime.MessageInspectors.Add(new AuthenticationMessageInspector());
#endif
#if NETSTANDARD2_0_OR_GREATER
            //添加消息拦截器
            clientRuntime.ClientMessageInspectors.Add(new AuthenticationMessageInspector());
#endif
        }


        //没有用
#if NET40_OR_GREATER
        //Shared

        /// <summary>
        /// 添加分发行为
        /// </summary>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        //Implements of IServiceBehavior

        /// <summary>
        /// 添加绑定参数
        /// </summary>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        //Implements of IEndpointBehavior

        /// <summary>
        /// 添加绑定参数
        /// </summary>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        public void Validate(ServiceEndpoint endpoint)
        {

        }
#endif
#if NETSTANDARD2_0_OR_GREATER
        //Shared

        /// <summary>
        /// 添加分发行为
        /// </summary>
        public void ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {

        }

        //Implements of IServiceBehavior

        /// <summary>
        /// 添加绑定参数
        /// </summary>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        //Implements of IEndpointBehavior

        /// <summary>
        /// 添加绑定参数
        /// </summary>
        public void AddBindingParameters(System.ServiceModel.Description.ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        public void Validate(System.ServiceModel.Description.ServiceEndpoint endpoint)
        {

        }
#endif
    }
}
