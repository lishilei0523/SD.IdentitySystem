using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
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
            //配置属性
            this.Property(menu => menu.Name).IsRequired().HasMaxLength(32);
            this.Property(menu => menu.SystemNo).IsRequired().HasMaxLength(16);

            //配置中间表
            this.HasMany(menu => menu.Authorities).WithMany(authority => authority.MenuLeaves).Map(map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Menu_Authority"));

            //配置索引
            this.HasIndex("IX_SystemNo", IndexType.Nonclustered, table => table.Property(menu => menu.SystemNo));
        }
    }
}
