using System;
using SD.UAC.Domain.EventSources.UserContext;
using ShSoft.Infrastructure.EntityBase;
using ShSoft.Infrastructure.EventBase.Mediator;

namespace SD.UAC.Domain.Entities
{
    /// <summary>
    /// 信息系统
    /// </summary>
    public class InfoSystem : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystem() { }
        #endregion

        #region 02.创建信息系统
        /// <summary>
        /// 创建信息系统
        /// </summary>
        /// <param name="systemName">系统名称</param>
        /// <param name="systemNo">系统编号</param>
        /// <param name="systemKindNo">系统类别编号</param>
        /// <param name="adminLoginId">管理员登录名</param>
        public InfoSystem(string systemNo, string systemName, string systemKindNo, string adminLoginId)
            : this()
        {
            #region # 验证参数

            base.CheckName(systemName, 2, 50);

            if (string.IsNullOrWhiteSpace(systemNo))
            {
                throw new ArgumentNullException("systemNo", @"系统编号不可为空！");
            }
            if (string.IsNullOrWhiteSpace(systemKindNo))
            {
                throw new ArgumentNullException("systemKindNo", @"系统类别编号不可为空！");
            }

            #endregion

            base.Number = systemNo;
            base.Name = systemName;
            this.SystemKindNo = systemKindNo;
            this.AdminLoginId = adminLoginId;

            //挂起领域事件
            EventMediator.Suspend(new InfoSystemCreatedEvent(this.SystemKindNo, this.Number, this.AdminLoginId));
        }
        #endregion

        #endregion

        #region # 属性

        #region 管理员登录名 —— string AdminLoginId
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string AdminLoginId { get; private set; }
        #endregion

        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string SystemKindNo { get; private set; }
        #endregion

        #endregion
    }
}
