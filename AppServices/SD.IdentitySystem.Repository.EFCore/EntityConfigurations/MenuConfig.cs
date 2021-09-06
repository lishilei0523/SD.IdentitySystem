using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 菜单数据映射配置
    /// </summary>
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasMany(menu => menu.Authorities).WithMany(authority => authority.MenuLeaves).UsingEntity(map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Menu_Authority"));

            //设置信息系统编号长度
            builder.Property(menu => menu.SystemNo).HasMaxLength(16);

            //设置索引
            builder.HasIndex(menu => menu.SystemNo).HasDatabaseName("IX_SystemNo");
        }
    }
}
