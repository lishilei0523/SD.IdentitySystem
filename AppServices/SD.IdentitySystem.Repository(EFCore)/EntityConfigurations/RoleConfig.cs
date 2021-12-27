using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
using System.Collections.Generic;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 角色实体映射配置
    /// </summary>
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //配置属性
            builder.HasKey(role => role.Id).IsClustered(false);
            builder.HasOne<InfoSystem>().WithMany().IsRequired().HasForeignKey(role => role.InfoSystemNo).OnDelete(DeleteBehavior.Restrict);
            builder.Property(role => role.Keywords).IsRequired().HasMaxLength(256);
            builder.Property(role => role.Name).IsRequired().HasMaxLength(32);

            //配置中间表
            builder
                .HasMany(role => role.Authorities)
                .WithMany(authority => authority.Roles)
                .UsingEntity<Dictionary<string, object>>("Role_Authority",
                    x => x.HasOne<Authority>().WithMany().HasForeignKey("Authority_Id"),
                    x => x.HasOne<Role>().WithMany().HasForeignKey("Role_Id"),
                    map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Role_Authority"));

            //配置索引
            builder.HasIndex(role => role.AddedTime).IsUnique(false).IsClustered();
        }
    }
}
