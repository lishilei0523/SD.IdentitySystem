using System;
using ShSoft.Framework2016.Common.PoweredByLee;
using ShSoft.Framework2016.Infrastructure.IDomainEvent;

namespace ShSoft.UAC.DomainEventStore.EventSources.Authority
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
    public class AuthorityCreatedEvent : DomainEvent
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
            : base(triggerTime)
        {
            AuthorityCreatedEventData data = new AuthorityCreatedEventData(infoSystemKindNo, authorityId);
            base.SourceDataStr = data.ToJson();
        }
        #endregion

        #endregion

        #region # 属性

        #region 只读属性 - 事件数据 —— AuthorityCreatedEventData Data
        /// <summary>
        /// 只读属性 - 事件数据
        /// </summary>
        public AuthorityCreatedEventData Data
        {
            get { return base.SourceDataStr.JsonToObject<AuthorityCreatedEventData>(); }
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 权限创建事件数据
    /// </summary>
    public struct AuthorityCreatedEventData
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="infoSystemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        public AuthorityCreatedEventData(string infoSystemKindNo, Guid authorityId)
        {
            this.InfoSystemKindNo = infoSystemKindNo;
            this.AuthorityId = authorityId;
        }

        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string InfoSystemKindNo;

        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid AuthorityId;
    }
}
