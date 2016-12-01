using System;
using SD.IdentitySystem.Domain.EventSources.UserContext;
using ShSoft.Infrastructure.EntityBase;
using ShSoft.Infrastructure.EventBase.Mediator;
using ShSoft.ValueObjects.Enums;

namespace SD.IdentitySystem.Domain.Entities
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
        /// <param name="adminLoginId">管理员登录名</param>
        public InfoSystem(string systemNo, string systemName, string adminLoginId)
            : this()
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(systemNo))
            {
                throw new ArgumentNullException("systemNo", @"系统编号不可为空！");
            }

            #endregion

            base.Number = systemNo;
            base.Name = systemName;
            this.AdminLoginId = adminLoginId;

            //挂起领域事件
            EventMediator.Suspend(new InfoSystemCreatedEvent(this.Number, this.Name, this.AdminLoginId));
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

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; private set; }
        #endregion

        #region 主机名 —— string Host
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; private set; }
        #endregion

        #region 端口 —— int? Port
        /// <summary>
        /// 端口
        /// </summary>
        public int? Port { get; private set; }
        #endregion

        #region 首页 —— string Index
        /// <summary>
        /// 首页
        /// </summary>
        public string Index { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 初始化 —— void Init(string host, int port, string index)
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="port">端口</param>
        /// <param name="index">首页</param>
        public void Init(string host, int port, string index)
        {
            this.Host = host;
            this.Port = port;
            this.Index = index;
        }
        #endregion

        #endregion
    }
}
