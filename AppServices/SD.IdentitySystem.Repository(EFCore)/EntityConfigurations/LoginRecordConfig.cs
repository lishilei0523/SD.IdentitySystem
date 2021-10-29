using SD.IdentitySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.Ignore(record => record.Number);
            builder.Ignore(record => record.Name);

            //配置索引
            builder.HasIndex(record => record.AddedTime).HasDatabaseName("IX_AddedTime").IsUnique(false).IsClustered(true);
        }
    }
}
