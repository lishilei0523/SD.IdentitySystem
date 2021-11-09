using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 信息系统实体映射配置
    /// </summary>
    public class InfoSystemConfig : IEntityTypeConfiguration<InfoSystem>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<InfoSystem> builder)
        {
            //配置属性
            builder.HasKey(system => system.Id).IsClustered(false);
            builder.Property(system => system.Keywords).IsRequired().HasMaxLength(256);
            builder.Property(system => system.Number).IsRequired().HasMaxLength(16);
            builder.Property(system => system.Name).IsRequired().HasMaxLength(64);

            //配置索引
            builder.HasIndex(system => system.AddedTime).HasDatabaseName("IX_AddedTime").IsUnique(false).IsClustered(true);
            builder.HasIndex(system => system.Number).HasDatabaseName("IX_Number").IsUnique();
        }
    }
}
