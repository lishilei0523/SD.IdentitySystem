using SD.IdentitySystem.Domain.Entities;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 权限实体映射配置
    /// </summary>
    public class AuthorityConfig : EntityTypeConfiguration<Authority>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public AuthorityConfig()
        {
            //设置信息系统编号长度
            this.Property(authority => authority.SystemNo).HasMaxLength(16);

            //设置索引
            this.HasIndex("IX_SystemNo", IndexType.Nonclustered, table => table.Property(authority => authority.SystemNo));
        }
    }
}
