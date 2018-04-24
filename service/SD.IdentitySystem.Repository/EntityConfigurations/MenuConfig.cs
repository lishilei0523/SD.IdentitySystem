using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.Constants;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 菜单数据映射配置
    /// </summary>
    public class MenuConfig : EntityTypeConfiguration<Menu>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public MenuConfig()
        {
            this.HasMany(menu => menu.Authorities).WithMany(authority => authority.MenuLeaves).Map(map => map.ToTable($"{GlobalSetting.TablePrefix}Menu_Authority"));

            //设置信息系统编号长度
            this.Property(menu => menu.SystemNo).HasMaxLength(16);

            //设置索引
            this.HasIndex("IX_SystemNo", IndexType.Nonclustered, table => table.Property(menu => menu.SystemNo));
        }
    }
}
