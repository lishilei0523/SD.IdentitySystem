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
            builder.HasAlternateKey(system => system.Number);
            builder.Property(system => system.Number).IsRequired().HasMaxLength(16);
            builder.Property(system => system.Name).IsRequired().HasMaxLength(64);
            builder.Property(system => system.Keywords).IsRequired().HasMaxLength(256);

            //配置索引
            builder.HasIndex(system => system.AddedTime).IsUnique(false).IsClustered();

            //忽略映射
            builder.Ignore(system => system.Deleted);
            builder.Ignore(system => system.DeletedTime);
        }
    }
}
