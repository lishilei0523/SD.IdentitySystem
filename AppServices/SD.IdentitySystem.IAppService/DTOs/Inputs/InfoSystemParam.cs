﻿using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Inputs
{
    /// <summary>
    /// 信息系统参数模型
    /// </summary>
    [DataContract]
    public struct InfoSystemParam
    {
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DataMember]
        public string infoSystemNo;

        /// <summary>
        /// 主机名称
        /// </summary>
        [DataMember]
        public string host;

        /// <summary>
        /// 端口
        /// </summary>
        [DataMember]
        public int port;

        /// <summary>
        /// 首页
        /// </summary>
        [DataMember]
        public string index;
    }
}
