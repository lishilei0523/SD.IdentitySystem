using System.Runtime.Serialization;
using SD.Infrastructure.DTOBase;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 权限数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class AuthorityInfo : BaseDTO
    {
        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DataMember]
        public string SystemNo { get; set; }
        #endregion

        #region 程序集名称 —— string AssemblyName
        /// <summary>
        /// 程序集名称
        /// </summary>
        [DataMember]
        public string AssemblyName { get; set; }
        #endregion

        #region 命名空间 —— string Namespace
        /// <summary>
        /// 命名空间
        /// </summary>
        [DataMember]
        public string Namespace { get; set; }
        #endregion

        #region 类名 —— string ClassName
        /// <summary>
        /// 类名
        /// </summary>
        [DataMember]
        public string ClassName { get; set; }
        #endregion

        #region 方法名 —— string MethodName
        /// <summary>
        /// 方法名
        /// </summary>
        [DataMember]
        public string MethodName { get; set; }
        #endregion

        #region 英文名 —— string EnglishName
        /// <summary>
        /// 英文名
        /// </summary>
        [DataMember]
        public string EnglishName { get; set; }
        #endregion

        #region 权限描述 —— string Description
        /// <summary>
        /// 权限描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        #endregion

        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        [DataMember]
        public string AuthorityPath { get; set; }
        #endregion


        //导航属性

        #region 导航属性 - 信息系统 —— InfoSystemInfo InfoSystemInfo
        /// <summary>
        /// 导航属性 - 信息系统
        /// </summary>
        [DataMember]
        public InfoSystemInfo InfoSystemInfo { get; set; }
        #endregion
    }
}
