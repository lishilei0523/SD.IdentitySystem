using Newtonsoft.Json;
using System;

namespace SD.IdentitySystem.SignalR.Server.Toolkits
{
    /// <summary>
    /// JSON扩展方法
    /// </summary>
    internal static class JsonExtension
    {
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
                throw new ArgumentNullException(nameof(json), @"JSON字符串不可为空！");
            }

            #endregion

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"无法将源JSON反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！");
            }
        }
        #endregion
    }
}
