using SD.Toolkits.EntityFramework.Attributes;
using ShSoft.Infrastructure.EntityBase;
using ShSoft.ValueObjects.Enums;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 信息系统类别（字典）
    /// </summary>
    [NonMap]
    public class InfoSystemKind : AggregateRootEntity
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystemKind() { }
        #endregion

        #region 02.创建系统类别构造器
        /// <summary>
        /// 创建系统类别构造器
        /// </summary>
        /// <param name="kindNo">类别编号</param>
        /// <param name="kindName">类别名称</param>
        /// <param name="applicationType">应用程序类型</param>
        public InfoSystemKind(string kindNo, string kindName, ApplicationType applicationType)
            : this()
        {
            base.Number = kindNo;
            base.Name = kindName;
            this.ApplicationType = applicationType;
        }
        #endregion

        #endregion

        #region # 属性

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
