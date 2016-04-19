using System.Collections.Generic;
using System.Runtime.Serialization;
using ShSoft.Framework2016.Infrastructure.IDTO;

namespace ShSoft.UAC.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://ShSoft.UAC.IAppService.DTOs.Outputs")]
    public class RoleInfo : BaseDTO
    {
        #region 角色描述 —— string Description
        /// <summary>
        /// 角色描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        #endregion

        #region 用户数 —— int UserCount
        /// <summary>
        /// 用户数
        /// </summary>
        [DataMember]
        public int UserCount { get; set; }
        #endregion


        //导航属性

        #region 权限集 —— IEnumerable<AuthorityInfo> AuthorityInfos
        /// <summary>
        /// 权限集
        /// </summary>
        [DataMember]
        public IEnumerable<AuthorityInfo> AuthorityInfos { get; set; }
        #endregion
    }
}
