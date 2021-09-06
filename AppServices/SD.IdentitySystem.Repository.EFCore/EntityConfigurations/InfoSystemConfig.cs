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
            //设置编号长度
            builder.Property(system => system.Number).HasMaxLength(16);

            //设置索引
            builder.HasIndex(system => system.Number).HasDatabaseName("IX_Number").IsUnique();
        }
    }
}
