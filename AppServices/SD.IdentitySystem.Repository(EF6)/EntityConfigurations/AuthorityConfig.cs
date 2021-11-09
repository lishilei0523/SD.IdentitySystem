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
            //配置属性
            this.HasKey(authority => authority.Id, index => index.IsClustered(false));
            this.Property(authority => authority.Keywords).IsRequired().HasMaxLength(256);
            this.Property(authority => authority.Name).IsRequired().HasMaxLength(64);
            this.Property(authority => authority.AuthorityPath).IsRequired().HasMaxLength(256);
            this.Property(authority => authority.SystemNo).IsRequired().HasMaxLength(16);

            //配置索引
            this.HasIndex("IX_AddedTime", IndexType.Clustered, table => table.Property(authority => authority.AddedTime));
            this.HasIndex("IX_SystemNo", IndexType.Nonclustered, table => table.Property(authority => authority.SystemNo));

            //忽略映射
            this.Ignore(authority => authority.Number);
        }
    }
}
