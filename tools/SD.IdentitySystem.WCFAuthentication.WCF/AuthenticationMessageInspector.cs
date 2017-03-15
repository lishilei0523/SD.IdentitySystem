using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using SD.CacheManager;
using SD.Infrastructure.Constants;
using SD.ValueObjects;
using SD.ValueObjects.CustomExceptions;

namespace SD.IdentitySystem.WCFAuthentication.WCF
{
    /// <summary>
    /// WCF客户端/服务端身份认证消息拦截器
    /// </summary>
    internal class AuthenticationMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync = new object();

        #region # Implements of IDispatchMessageInspector

        /// <summary>
        /// 接收请求后事件
        /// </summary>
        /// <param name="request">请求消息</param>
        /// <param name="channel">信道</param>
        /// <param name="instanceContext">WCF实例上下文</param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //如果是身份认证接口，无需认证
            if (OperationContext.Current.EndpointDispatcher.ContractName != "IAuthenticationContract")
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
                Guid publicKey = headers.GetHeader<Guid>(Constants.WcfAuthHeaderName, Constants.WcfAuthHeaderNamespace);

                //认证
                lock (_Sync)
                {
                    //以公钥为键，查询分布式缓存，如果有值则通过，无值则不通过
                    LoginInfo loginInfo = CacheMediator.Get<LoginInfo>(publicKey.ToString());

                    if (loginInfo == null)
                    {
                        throw new NoPermissionException("身份过期，请重新登录！");
                    }

                    //通过后，重新设置缓存过期时间为20分钟
                    CacheMediator.Set(publicKey.ToString(), loginInfo, DateTime.Now.AddMinutes(20));
                }
            }

            return null;
        }

        /// <summary>
        /// 响应请求前事件
        /// </summary>
        /// <param name="reply">响应消息</param>
        /// <param name="correlationState">相关状态</param>
        public void BeforeSendReply(ref Message reply, object correlationState) { }

        #endregion

        #region # Implements of IClientMessageInspector

        /// <summary>
        /// 请求发送前事件
        /// </summary>
        /// <param name="request">请求消息</param>
        /// <param name="channel">信道</param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //WCF客户端获取公钥处理
            MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;

            #region # 验证消息头

            if (!headers.Any(x => x.Name == Constants.WcfAuthHeaderName && x.Namespace == Constants.WcfAuthHeaderNamespace))
            {
                throw new NullReferenceException("身份认证消息头不存在，请检查程序！");
            }

            #endregion

            //读取消息头中的公钥
            Guid publishKey = headers.GetHeader<Guid>(Constants.WcfAuthHeaderName, Constants.WcfAuthHeaderNamespace);

            //添加消息头
            MessageHeader header = MessageHeader.CreateHeader(Constants.WcfAuthHeaderName, Constants.WcfAuthHeaderNamespace, publishKey);
            request.Headers.Add(header);

            return null;
        }

        /// <summary>
        /// 请求响应后事件
        /// </summary>
        /// <param name="reply">响应消息</param>
        /// <param name="correlationState">相关状态</param>
        public void AfterReceiveReply(ref Message reply, object correlationState) { }

        #endregion
    }
}
