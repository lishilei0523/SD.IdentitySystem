using ShSoft.Infrastructure.EventBase;
using System;

namespace SD.IdentitySystem.Domain.EventSources.AuthorizationContext
{
    /// <summary>
    /// 权限创建事件
    /// </summary>
    /// <remarks>
    /// 处理流程：
    /// *1、查询出给定信息系统
    /// *2、为系统管理员角色追加最新权限
    /// </remarks>
    public class AuthorityCreatedEvent : Event
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected AuthorityCreatedEvent() { }
        #endregion

        #region 02.基础构造器
        /// <summary>
        /// 基础构造器
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="authorityId">权限Id</param>
        public AuthorityCreatedEvent(string systemNo, Guid authorityId)
            : this()
        {
            this.SystemNo = systemNo;
            this.AuthorityId = authorityId;
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; set; }
        #endregion

        #region 权限Id —— Guid AuthorityId
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid AuthorityId { get; set; }
        #endregion

        #endregion
    }
}
