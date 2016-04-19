using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;
using ShSoft.Framework2016.Common.PoweredByLee;
using ShSoft.UAC.AuthorizationAspect.Attributes;
using ShSoft.UAC.AuthorizationAspect.Common;
using ShSoft.UAC.AuthorizationAspect.CustomExceptions;
using ShSoft.UAC.IAppService.DTOs.Outputs;

namespace ShSoft.UAC.AuthorizationAspect.Aspects
{
    /// <summary>
    /// 需要权限验证AOP特性类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequireAuthorizationAspect : OnMethodBoundaryAspect
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

                object sessionObj = HttpAssitant.Session[SessionKey.CurrentAuthorities];

                #region # 验证Session

                if (sessionObj == null)
                {
                    throw new ApplicationException("Session中无权限信息，请检查程序！");
                }

                #endregion

                //从Session中取出权限集
                IEnumerable<AuthorityInfo> currentAuthorities = sessionObj.ToString().JsonToObject<IEnumerable<AuthorityInfo>>();

                #region # 验证权限

                if (currentAuthorities == null)
                {
                    throw new ApplicationException("Session中无权限信息，请检查程序！");
                }
                if (currentAuthorities.All(x => x.AuthorityPath != methodPath))
                {
                    throw new NoPermissionException();
                }

                #endregion
            }
        }
    }
}
