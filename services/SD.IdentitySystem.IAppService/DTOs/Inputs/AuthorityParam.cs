using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 权限参数模型
    /// </summary>
    [DataContract]
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
        /// 描述
        /// </summary>
        [DataMember]
        public string description;
    }
}
