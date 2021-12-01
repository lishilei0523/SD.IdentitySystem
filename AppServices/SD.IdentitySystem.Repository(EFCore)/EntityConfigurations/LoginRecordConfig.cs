using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 登录记录实体映射配置
    /// </summary>
    public class LoginRecordConfig : IEntityTypeConfiguration<LoginRecord>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<LoginRecord> builder)
        {
            //配置属性
            builder.HasKey(record => record.Id).IsClustered(false);
            builder.Property(record => record.Keywords).IsRequired().HasMaxLength(256);

            //配置索引
            builder.HasIndex(record => record.AddedTime).IsUnique(false).IsClustered().HasDatabaseName("IX_AddedTime");

            //忽略映射
            builder.Ignore(record => record.Number);
            builder.Ignore(record => record.Name);
        }
    }
}
