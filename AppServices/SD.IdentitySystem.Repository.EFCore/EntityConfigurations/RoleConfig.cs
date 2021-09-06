using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 角色数据映射配置
    /// </summary>
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(role => role.Authorities).WithMany(authority => authority.Roles).UsingEntity(map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Role_Authority"));

            //设置信息系统编号长度
            builder.Property(role => role.SystemNo).HasMaxLength(16);

            //设置索引
            builder.HasIndex(role => role.SystemNo).HasDatabaseName("IX_SystemNo");
        }
    }
}
