using System;
using ShSoft.Infrastructure.EntityBase;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 登录记录
    /// </summary>
    public class LoginRecord : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected LoginRecord() { }
        #endregion

        #region 02.创建登录记录构造器
        /// <summary>
        /// 创建登录记录构造器
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="loginId">登录名</param>
        /// <param name="employeeId">员工Id</param>
        /// <param name="employeeNo">员工编号</param>
        /// <param name="employeeName">员工名称</param>
        /// <param name="ip">IP地址</param>
        public LoginRecord(Guid publicKey, string loginId, Guid? employeeId, string employeeNo, string employeeName, string ip)
        {
            this.PublicKey = publicKey;
            this.LoginId = loginId;
            this.EmployeeId = employeeId;
            this.EmployeeNo = employeeNo;
            this.EmployeeName = employeeName;
            this.IP = ip;
        }
        #endregion

        #endregion

        #region # 属性

        #region 公钥 —— Guid PublicKey
        /// <summary>
        /// 公钥
        /// </summary>
        public Guid PublicKey { get; private set; }
        #endregion

        #region 登录名 —— string LoginId
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginId { get; private set; }
        #endregion

        #region 员工Id —— Guid? EmployeeId
        /// <summary>
        /// 员工Id
        /// </summary>
        public Guid? EmployeeId { get; private set; }
        #endregion

        #region 员工编号 —— string EmployeeNo
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; private set; }
        #endregion

        #region 员工名称 —— string EmployeeName
        /// <summary>
        /// 员工名称
        /// </summary>
        public string EmployeeName { get; private set; }
        #endregion

        #region IP地址 —— string IP
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; private set; }
        #endregion

        #endregion
    }
}
