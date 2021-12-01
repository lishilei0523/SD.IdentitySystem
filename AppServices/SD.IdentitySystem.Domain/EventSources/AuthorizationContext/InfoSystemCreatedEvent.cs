using SD.Infrastructure.Constants;
using SD.Infrastructure.EventBase;

namespace SD.IdentitySystem.Domain.EventSources.AuthorizationContext
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
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <param name="adminLoginId">管理员用户名</param>
        public InfoSystemCreatedEvent(string infoSystemNo, string infoSystemName, ApplicationType applicationType, string adminLoginId)
            : this()
        {
            this.InfoSystemNo = infoSystemNo;
            this.SystemName = infoSystemName;
            this.ApplicationType = applicationType;
            this.AdminLoginId = adminLoginId;
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string InfoSystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string SystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string SystemName { get; set; }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; set; }
        #endregion

        #region 管理员用户名 —— string AdminLoginId
        /// <summary>
        /// 管理员用户名
        /// </summary>
        public string AdminLoginId { get; set; }
        #endregion

        #endregion
    }
}
