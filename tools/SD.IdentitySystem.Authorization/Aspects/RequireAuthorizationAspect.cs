using PostSharp.Aspects;
using SD.IdentitySystem.Authorization.Toolkits;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SD.IdentitySystem.Authorization.Aspects
{
    /// <summary>
    /// 需要权限验证AOP特性类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class RequireAuthorizationAspect : OnMethodBoundaryAspect
    {
        /// <summary>
        /// 执行方法开始事件
        /// </summary>
        /// <param name="eventArgs">方法元数据</param>
        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            if (eventArgs.Method.IsDefined(typeof(RequireAuthorizationAttribute)))
            {
                //获取正在执行的方法路径
                string methodPath = eventArgs.Method.GetMethodPath();

                //验证登录信息是否为null
                if (Membership.LoginInfo == null)
                {
                    throw new ApplicationException("当前登录信息为空，请重新登录！");
                }

                //从登录信息中取出权限集
                IEnumerable<string> currentAuthorityPaths = Membership.LoginInfo.LoginAuthorityInfos.Values.SelectMany(x => x).Select(x => x.AuthorityPath);

                //验证权限
                if (currentAuthorityPaths.All(path => path != methodPath))
                {
                    throw new NoPermissionException("您没有权限，请联系系统管理员！");
                }
            }
        }
    }
}
