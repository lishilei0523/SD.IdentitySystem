using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 菜单实体映射配置
    /// </summary>
    public class MenuConfig : EntityTypeConfiguration<Menu>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public MenuConfig()
        {
            //配置属性
            this.HasKey(menu => menu.Id, index => index.IsClustered(false));
            this.Property(menu => menu.Name).IsRequired().HasMaxLength(32);
            this.Property(menu => menu.Keywords).IsRequired().HasMaxLength(256);
            this.Property(menu => menu.InfoSystemNo).IsRequired().HasMaxLength(16);

            //配置中间表
            this.HasMany(menu => menu.Authorities).WithMany(authority => authority.MenuLeaves).Map(map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Menu_Authority"));

            //配置索引
            this.HasIndex("IX_Menu_InfoSystemNo", IndexType.Nonclustered, table => table.Property(menu => menu.InfoSystemNo));

            //忽略映射
            this.Ignore(menu => menu.Number);
            this.Ignore(menu => menu.Deleted);
            this.Ignore(menu => menu.DeletedTime);
        }
    }
}
