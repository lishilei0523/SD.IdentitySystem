using ArxOne.MrAdvice.Advice;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.CustomExceptions;
using SD.Infrastructure.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SD.IdentitySystem.Authorization.Aspects
{
    /// <summary>
    /// 需要权限验证AOP特性类
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class RequireAuthorizationAspect : Attribute, IMethodAdvice
    {
        //Implements

        #region # 拦截方法 —— void Advise(MethodAdviceContext context)
        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="context">方法元数据</param>
        public void Advise(MethodAdviceContext context)
        {
            if (context.TargetMethod.IsDefined(typeof(RequireAuthorizationAttribute)))
            {
                LoginInfo loginInfo = MembershipMediator.GetLoginInfo();
                if (loginInfo == null)
                {
                    throw new NoPermissionException("当前登录信息为空，请重新登录！");
                }

                //获取正在执行的方法路径
                string authorityPath = this.GetMethodPath(context.TargetMethod);

                //从登录信息中取出权限集
                IEnumerable<string> ownedAuthorityPaths = loginInfo.LoginAuthorityInfos.Select(x => x.Path);

                //验证权限
                if (!ownedAuthorityPaths.Contains(authorityPath))
                {
                    throw new NoPermissionException("您没有权限，请联系系统管理员！");
                }
            }

            context.Proceed();
        }
        #endregion


        //Private

        #region # 获取方法路径 —— string GetMethodPath(this MethodBase method)
        /// <summary>
        /// 获取方法路径
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法路径</returns>
        private string GetMethodPath(MethodBase method)
        {
            #region # 验证

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), @"方法信息不可为空！");
            }

            #endregion

            const string separator = "/";
            string assemblyName = method.DeclaringType?.Assembly.GetName().Name;
            string @namespace = method.DeclaringType?.Namespace;
            string className = method.DeclaringType?.Name;

            StringBuilder pathBuilder = new StringBuilder(separator);
            pathBuilder.Append(assemblyName);
            pathBuilder.Append(separator);
            pathBuilder.Append(@namespace);
            pathBuilder.Append(separator);
            pathBuilder.Append(className);
            pathBuilder.Append(separator);
            pathBuilder.Append(method.Name);

            return pathBuilder.ToString();
        }
        #endregion
    }
}
