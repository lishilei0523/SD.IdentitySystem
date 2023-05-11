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
            //配置属性
            builder.HasKey(authority => authority.Id).IsClustered(false);
            builder.HasOne<InfoSystem>().WithMany().IsRequired().HasForeignKey(authority => authority.InfoSystemNo).HasPrincipalKey(system => system.Number).OnDelete(DeleteBehavior.Restrict);
            builder.Property(authority => authority.Name).IsRequired().HasMaxLength(64);
            builder.Property(authority => authority.Keywords).IsRequired().HasMaxLength(256);
            builder.Property(authority => authority.InfoSystemNo).IsRequired();
            builder.Property(authority => authority.AuthorityPath).IsRequired().HasMaxLength(256);

            //配置索引
            builder.HasIndex(authority => authority.AddedTime).IsUnique(false).IsClustered();

            //忽略映射
            builder.Ignore(authority => authority.Number);
        }
    }
}
