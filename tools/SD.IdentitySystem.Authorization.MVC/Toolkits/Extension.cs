using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Text;

namespace SD.IdentitySystem.Authorization.MVC.Toolkits
{
    /// <summary>
    /// 扩展工具类
    /// </summary>
    public static class Extension
    {
        #region # 常量

        /// <summary>
        /// 分隔符
        /// </summary>
        private const string Separator = "/";

        #endregion

        #region # 获取方法路径 —— string GetMethodPath(this MethodBase method)
        /// <summary>
        /// 获取方法路径
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法路径</returns>
        public static string GetMethodPath(this MethodBase method)
        {
            #region # 验证参数

            if (method == null)
            {
                throw new ArgumentNullException("method", @"方法信息不可为空！");
            }

            #endregion

            string assemblyName = method.DeclaringType.Assembly.GetName().Name;
            string @namespace = method.DeclaringType.Namespace;
            string className = method.DeclaringType.Name;

            StringBuilder pathBuilder = new StringBuilder(Extension.Separator);
            pathBuilder.Append(assemblyName);
            pathBuilder.Append(Extension.Separator);
            pathBuilder.Append(@namespace);
            pathBuilder.Append(Extension.Separator);
            pathBuilder.Append(className);
            pathBuilder.Append(Extension.Separator);
            pathBuilder.Append(method.Name);

            return pathBuilder.ToString();
        }
        #endregion

        #region # JSON字符串反序列化为对象扩展方法 —— static T JsonToObject<T>(this string json)
        /// <summary>
        /// JSON字符串反序列化为对象扩展方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>给定类型对象</returns>
        /// <exception cref="ArgumentNullException">JSON字符串为空</exception>
        /// <exception cref="InvalidOperationException">反序列化为给定类型失败</exception>
        public static T JsonToObject<T>(this string json)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentNullException("json", @"JSON字符串不可为空！");
            }

            #endregion

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(string.Format("无法将源JSON反序列化为给定类型\"{0}\"，请检查类型后重试！", typeof(T).Name));
            }
        }
        #endregion
    }
}
