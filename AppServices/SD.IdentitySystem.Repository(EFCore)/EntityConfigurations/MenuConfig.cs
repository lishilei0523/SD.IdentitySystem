using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
using System.Collections.Generic;

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
            //配置属性
            builder.Property(menu => menu.Name).IsRequired().HasMaxLength(32);
            builder.Property(menu => menu.SystemNo).IsRequired().HasMaxLength(16);

            //配置菜单树形结构关系
            builder
                .HasOne(menu => menu.ParentNode)
                .WithMany(menu => menu.SubNodes)
                .HasForeignKey("ParentNode_Id");

            //配置中间表
            builder
                .HasMany(menu => menu.Authorities)
                .WithMany(authority => authority.MenuLeaves)
                .UsingEntity<Dictionary<string, object>>("Menu_Authority",
                    x => x.HasOne<Authority>().WithMany().HasForeignKey("Authority_Id"),
                    x => x.HasOne<Menu>().WithMany().HasForeignKey("Menu_Id"),
                    map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Menu_Authority"));

            //配置索引
            builder.HasIndex(menu => menu.SystemNo).HasDatabaseName("IX_SystemNo");
        }
    }
}
