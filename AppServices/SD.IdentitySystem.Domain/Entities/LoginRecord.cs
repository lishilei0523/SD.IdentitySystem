using SD.Infrastructure.Constants;
using SD.Infrastructure.EntityBase;
using System;
using System.Text;

namespace SD.IdentitySystem.Domain.Entities
{
    /// <summary>
    /// 登录记录
    /// </summary>
    public class LoginRecord : AggregateRootEntity, IPartible
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected LoginRecord() { }
        #endregion

        #region 02.创建登录记录构造器
        /// <summary>
        /// 创建登录记录构造器
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="loginId">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="ip">IP地址</param>
        public LoginRecord(Guid publicKey, string loginId, string realName, string ip)
            : this()
        {
            this.PublicKey = publicKey;
            this.LoginId = loginId;
            this.RealName = realName;
            this.IP = ip;

            //初始化关键字
            this.InitKeywords();
        }
        #endregion

        #endregion

        #region # 属性

        #region 公钥 —— Guid PublicKey
        /// <summary>
        /// 公钥
        /// </summary>
        public Guid PublicKey { get; private set; }
        #endregion

        #region 用户名 —— string LoginId
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginId { get; private set; }
        #endregion

        #region 真实姓名 —— string RealName
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; private set; }
        #endregion

        #region IP地址 —— string IP
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; private set; }
        #endregion

        #region 分区索引 —— int PartitionIndex
        /// <summary>
        /// 分区索引
        /// </summary>
        public int PartitionIndex
        {
            get => (int)(this.AddedTime.Ticks % GlobalSetting.PartitionsCount);
            set => value = (int)(this.AddedTime.Ticks % GlobalSetting.PartitionsCount);
        }
        #endregion

        #endregion

        #region # 方法

        #region 初始化关键字 —— void InitKeywords()
        /// <summary>
        /// 初始化关键字
        /// </summary>
        private void InitKeywords()
        {
            StringBuilder keywordsBuilder = new StringBuilder();
            keywordsBuilder.Append(this.LoginId);
            keywordsBuilder.Append(this.RealName);

            base.SetKeywords(keywordsBuilder.ToString());
        }
        #endregion

        #endregion
    }
}
