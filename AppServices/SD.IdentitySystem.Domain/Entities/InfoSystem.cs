﻿using SD.Infrastructure.Constants;
using SD.Infrastructure.EntityBase;
using System;
using System.Text;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 信息系统
    /// </summary>
    public class InfoSystem : AggregateRootEntity
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected InfoSystem() { }
        #endregion

        #region 01.创建信息系统构造器
        /// <summary>
        /// 创建信息系统构造器
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="infoSystemName">信息系统名称</param>
        /// <param name="adminLoginId">管理员用户名</param>
        /// <param name="applicationType">应用程序类型</param>
        public InfoSystem(string infoSystemNo, string infoSystemName, string adminLoginId, ApplicationType applicationType)
            : this()
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(infoSystemNo))
            {
                throw new ArgumentNullException(nameof(infoSystemNo), "信息系统编号不可为空！");
            }

            #endregion

            base.Number = infoSystemNo;
            base.Name = infoSystemName;
            this.AdminLoginId = adminLoginId;
            this.ApplicationType = applicationType;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 管理员用户名 —— string AdminLoginId
        /// <summary>
        /// 管理员用户名
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

        #region 修改信息系统 —— void UpdateInfo(string infoSystemName)
        /// <summary>
        /// 修改信息系统
        /// </summary>
        /// <param name="infoSystemName">信息系统名称</param>
        public void UpdateInfo(string infoSystemName)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(infoSystemName))
            {
                throw new ArgumentNullException(nameof(infoSystemName), "信息系统名称不可为空！");
            }

            #endregion

            base.Name = infoSystemName;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

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

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.Number);
            builder.Append(base.Name);

            base.SetKeywords(builder.ToString());
        }
        #endregion

        #endregion
    }
}
