using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 权限参数模型
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Inputs")]
    public struct AuthorityParam
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        [DataMember]
        public string authorityName;

        /// <summary>
        /// 权限路径
        /// </summary>
        [DataMember]
        public string authorityPath;

        /// <summary>
        /// 英文名称
        /// </summary>
        [DataMember]
        public string englishName;

        /// <summary>
        /// 程序集名称
        /// </summary>
        [DataMember]
        public string assemblyName;

        /// <summary>
        /// 命名空间
        /// </summary>
        [DataMember]
        public string @namespace;

        /// <summary>
        /// 类名
        /// </summary>
        [DataMember]
        public string className;

        /// <summary>
        /// 方法名
        /// </summary>
        [DataMember]
        public string methodName;

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string description;
    }
}
