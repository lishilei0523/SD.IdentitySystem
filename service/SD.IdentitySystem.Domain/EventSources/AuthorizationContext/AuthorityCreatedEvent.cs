using System;
using ShSoft.Infrastructure.EventBase;

namespace SD.IdentitySystem.Domain.EventSources.AuthorizationContext
{
    /// <summary>
    /// 权限创建事件
    /// </summary>
    /// <remarks>
    /// 处理流程：
    /// *1、查询出给定信息系统类别的所有信息系统
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
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        public AuthorityCreatedEvent(string systemKindNo, Guid authorityId)
            : this()
        {
            this.SystemKindNo = systemKindNo;
            this.AuthorityId = authorityId;
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

        #region 权限Id —— Guid AuthorityId
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid AuthorityId { get; set; }
        #endregion

        #endregion
    }
}
