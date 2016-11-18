using ShSoft.Infrastructure.EventBase;

namespace SD.IdentitySystem.Domain.EventSources.UserContext
{
    /// <summary>
    /// 信息系统已创建事件
    /// </summary>
    public class InfoSystemCreatedEvent : Event
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystemCreatedEvent() { }
        #endregion

        #region 02.基础构造器
        /// <summary>
        /// 基础构造器
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="systemName">信息系统名称</param>
        /// <param name="adminLoginId">管理员登录名</param>
        public InfoSystemCreatedEvent(string systemKindNo, string systemNo, string systemName, string adminLoginId)
            : this()
        {
            this.SystemKindNo = systemKindNo;
            this.SystemNo = systemNo;
            this.SystemName = systemName;
            this.AdminLoginId = adminLoginId;
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string SystemKindNo { get; set; }
        #endregion

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string SystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string SystemName { get; set; }
        #endregion

        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string AdminLoginId { get; set; }
        #endregion

        #endregion
    }
}
