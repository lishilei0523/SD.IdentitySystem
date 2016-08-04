using ShSoft.Infrastructure.EventBase;

namespace SD.UAC.Domain.EventSources.UserContext
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
        public InfoSystemCreatedEvent(string systemKindNo, string systemNo, string adminLoginId)
            : this()
        {
            this.SystemKindNo = systemKindNo;
            this.SystemNo = systemNo;
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

        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string AdminLoginId { get; set; }
        #endregion

        #endregion
    }
}
