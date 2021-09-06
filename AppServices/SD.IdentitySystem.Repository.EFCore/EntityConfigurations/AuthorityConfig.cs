using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 权限实体映射配置
    /// </summary>
    public class AuthorityConfig : IEntityTypeConfiguration<Authority>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<Authority> builder)
        {
            //设置信息系统编号长度
            builder.Property(authority => authority.SystemNo).HasMaxLength(16);

            //设置索引
            builder.HasIndex(authority => authority.SystemNo).HasDatabaseName("IX_SystemNo");
        }
    }
}
