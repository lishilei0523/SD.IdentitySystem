using PostSharp.Aspects;
using SD.IdentitySystem.Authorization.Windows.Toolkits;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //获取方法路径
                string methodPath = eventArgs.Method.GetMethodPath();

                object sessionAuthorityPaths = AppDomain.CurrentDomain.GetData(SessionKey.CurrentAuthorities);

                #region # 验证Session

                if (sessionAuthorityPaths == null)
                {
                    throw new ApplicationException("Session中无权限信息，请检查程序！");
                }

                #endregion

                //从Session中取出权限集
                IList<string> currentAuthorityPaths = sessionAuthorityPaths.ToString().JsonToObject<IList<string>>();

                #region # 验证权限

                if (currentAuthorityPaths == null)
                {
                    throw new ApplicationException("Session中无权限信息，请检查程序！");
                }
                if (currentAuthorityPaths.All(path => path != methodPath))
                {
                    throw new NoPermissionException("您没有权限，请联系系统管理员！");
                }

                #endregion
            }
        }
    }
}
