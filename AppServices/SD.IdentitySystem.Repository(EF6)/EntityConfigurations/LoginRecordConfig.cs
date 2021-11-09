using SD.IdentitySystem.Domain.Entities;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 登录记录实体映射配置
    /// </summary>
    public class LoginRecordConfig : EntityTypeConfiguration<LoginRecord>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public LoginRecordConfig()
        {
            //配置属性
            this.HasKey(record => record.Id, index => index.IsClustered(false));
            this.Property(record => record.Keywords).IsRequired().HasMaxLength(256);

            //配置索引
            this.HasIndex("IX_AddedTime", IndexType.Clustered, table => table.Property(record => record.AddedTime));

            //忽略映射
            this.Ignore(record => record.Number);
            this.Ignore(record => record.Name);
        }
    }
}
