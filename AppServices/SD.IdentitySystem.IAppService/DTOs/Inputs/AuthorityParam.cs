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
        public string AuthorityName;

        /// <summary>
        /// 权限路径
        /// </summary>
        [DataMember]
        public string AuthorityPath;

        /// <summary>
        /// 英文名称
        /// </summary>
        [DataMember]
        public string EnglishName;

        /// <summary>
        /// 程序集名称
        /// </summary>
        [DataMember]
        public string AssemblyName;

        /// <summary>
        /// 命名空间
        /// </summary>
        [DataMember]
        public string Namespace;

        /// <summary>
        /// 类名
        /// </summary>
        [DataMember]
        public string ClassName;

        /// <summary>
        /// 方法名
        /// </summary>
        [DataMember]
        public string MethodName;

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description;
    }
}
