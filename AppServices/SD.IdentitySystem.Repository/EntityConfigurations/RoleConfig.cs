using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.Constants;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 角色数据映射配置
    /// </summary>
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public RoleConfig()
        {
            this.HasMany(role => role.Authorities).WithMany(authority => authority.Roles).Map(map => map.ToTable($"{GlobalSetting.TablePrefix}Role_Authority"));

            //设置信息系统编号长度
            this.Property(role => role.SystemNo).HasMaxLength(16);

            //设置索引
            this.HasIndex("IX_SystemNo", IndexType.Nonclustered, table => table.Property(role => role.SystemNo));
        }
    }
}
