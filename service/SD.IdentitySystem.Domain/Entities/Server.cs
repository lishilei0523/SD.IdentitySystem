using SD.Infrastructure.EntityBase;
using System;
using System.Text;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class Server : AggregateRootEntity
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Server() { }
        #endregion

        #region 01.创建服务器构造器
        /// <summary>
        /// 创建服务器构造器
        /// </summary>
        /// <param name="uniqueCode">唯一码</param>
        /// <param name="hostName">主机名</param>
        /// <param name="serviceOverDate">服务停止日期</param>
        public Server(string uniqueCode, string hostName, DateTime serviceOverDate)
            : this()
        {
            base.Number = uniqueCode;
            base.Name = hostName;
            this.ServiceOverDate = serviceOverDate.Date;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 服务停止日期 —— DateTime ServiceOverDate
        /// <summary>
        /// 服务停止日期
        /// </summary>
        public DateTime ServiceOverDate { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 修改主机名 —— void UpdateHostName(string hostName)
        /// <summary>
        /// 修改主机名
        /// </summary>
        /// <param name="hostName">主机名</param>
        public void UpdateHostName(string hostName)
        {
            base.Name = hostName;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #region 修改服务停止日期 —— void UpdateServiceOverDate(DateTime serviceOverDate)
        /// <summary>
        /// 修改服务停止日期
        /// </summary>
        /// <param name="serviceOverDate">服务停止日期</param>
        public void UpdateServiceOverDate(DateTime serviceOverDate)
        {
            this.ServiceOverDate = serviceOverDate.Date;
        }
        #endregion


        //Private

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            StringBuilder keywordsBuilder = new StringBuilder();
            keywordsBuilder.Append(base.Name);

            base.SetKeywords(keywordsBuilder.ToString());
        }
        #endregion

        #endregion
    }
}
