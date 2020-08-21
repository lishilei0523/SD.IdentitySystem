using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 初始化信息系统参数模型
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Inputs")]
    public struct InfoSystemParam
    {
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DataMember]
        public string SystemNo;

        /// <summary>
        /// 主机名称
        /// </summary>
        [DataMember]
        public string Host;

        /// <summary>
        /// 端口
        /// </summary>
        [DataMember]
        public int Port;

        /// <summary>
        /// 首页
        /// </summary>
        [DataMember]
        public string Index;
    }
}
