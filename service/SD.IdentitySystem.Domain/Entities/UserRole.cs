using ShSoft.Common.PoweredByLee;
using ShSoft.Infrastructure.EntityBase;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole : PlainEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected UserRole() { }
        #endregion

        #region 02.创建用户角色构造器
        /// <summary>
        /// 创建用户角色构造器
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="user">用户</param>
        /// <param name="role">角色</param>
        public UserRole(string systemNo, User user, Role role)
            : this()
        {
            //验证
            Assert.IsFalse(string.IsNullOrWhiteSpace(systemNo), "信息系统编号不可为空！");
            Assert.IsNotNull(user, "用户不可为null！");
            Assert.IsNotNull(role, "角色不可为null！");

            this.SystemNo = systemNo;
            this.User = user;
            this.Role = role;
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; internal set; }
        #endregion

        #region 导航属性 - 用户 —— User User
        /// <summary>
        /// 导航属性 - 用户
        /// </summary>
        public virtual User User { get; internal set; }
        #endregion

        #region 导航属性 - 角色 —— Role Role
        /// <summary>
        /// 导航属性 - 角色
        /// </summary>
        public virtual Role Role { get; internal set; }
        #endregion

        #endregion
    }
}
