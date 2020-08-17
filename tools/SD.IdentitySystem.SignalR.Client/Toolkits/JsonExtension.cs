using Newtonsoft.Json;
using System;

namespace SD.IdentitySystem.SignalR.Client.Toolkits
{
    /// <summary>
    /// JSON扩展方法
    /// </summary>
    internal static class JsonExtension
    {
        #region # object序列化JSON字符串扩展方法 —— static string ToJson(this object instance...
        /// <summary>
        /// object序列化JSON字符串扩展方法
        /// </summary>
        /// <param name="instance">object及其子类对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJson(this object instance)
        {
            #region # 验证参数

            if (instance == null)
            {
                return null;
            }

            #endregion

            try
            {
                JsonSerializerSettings settting = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                return JsonConvert.SerializeObject(instance, Formatting.None, settting);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
        #endregion
    }
}
