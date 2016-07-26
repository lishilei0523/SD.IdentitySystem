using System;
using ShSoft.Infrastructure.EventBase;

namespace SD.UAC.Domain.EventSources.AuthorizationContext
{
    /// <summary>
    /// 权限创建事件
    /// </summary>
    /// <remarks>
    /// 处理流程：
    /// *1、查询出给定信息系统类别的所有信息系统
    /// *2、如果是管理中心信息系统类别，为超级管理员授予最新权限
    /// *3、如果是供应商信息系统类别，为系统管理员及供应商代理人授予最新权限
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
        /// <param name="infoSystemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        /// <param name="triggerTime">触发时间</param>
        public AuthorityCreatedEvent(string infoSystemKindNo, Guid authorityId, DateTime? triggerTime = null)
            : this()
        {
            this.InfoSystemKindNo = infoSystemKindNo;
            this.AuthorityId = authorityId;
        }
        #endregion

        #endregion

        #region # 属性

        #region 信息系统类别编号 —— string InfoSystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string InfoSystemKindNo { get; set; }
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
