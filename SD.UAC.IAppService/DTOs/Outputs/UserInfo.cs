using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ShSoft.Framework2016.Infrastructure.IDTO;

namespace SD.UAC.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://ShSoft.UAC.IAppService.DTOs.Outputs")]
    public class UserInfo : BaseDTO
    {
        #region 员工编号 —— string EmployeeNo
        /// <summary>
        /// 员工编号
        /// </summary>
        [DataMember]
        public string EmployeeNo { get; set; }
        #endregion

        #region 密码 —— string Password
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        #endregion

        #region 上次登录时间 —— DateTime? LastLoginTime
        /// <summary>
        /// 上次登录时间
        /// </summary>
        [DataMember]
        public DateTime? LastLoginTime { get; set; }
        #endregion

        #region 是否启用 —— bool Enabled
        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }
        #endregion


        //外部

        #region 角色集 —— IEnumerable<RoleInfo> RoleInfos
        /// <summary>
        /// 角色集
        /// </summary>
        [DataMember]
        public IEnumerable<RoleInfo> RoleInfos { get; set; }
        #endregion
    }
}
