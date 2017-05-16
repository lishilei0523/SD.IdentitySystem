using PostSharp.Aspects;
using SD.Infrastructure.Attributes;
using System;
using System.Reflection;

namespace SD.IdentitySystem.Authorization.Windows.Aspects
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
                //TODO 验证Windows客户端权限
            }
        }
    }
}
