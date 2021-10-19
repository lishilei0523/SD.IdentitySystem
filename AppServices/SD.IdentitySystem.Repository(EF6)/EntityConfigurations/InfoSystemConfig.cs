using SD.IdentitySystem.Domain.Entities;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 信息系统实体映射配置
    /// </summary>
    public class InfoSystemConfig : EntityTypeConfiguration<InfoSystem>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public InfoSystemConfig()
        {
            //配置属性
            this.Property(system => system.Number).IsRequired().HasMaxLength(16);
            this.Property(system => system.Name).IsRequired().HasMaxLength(64);

            //配置索引
            this.HasIndex("IX_Number", IndexType.Unique, table => table.Property(system => system.Number));
        }
    }
}
