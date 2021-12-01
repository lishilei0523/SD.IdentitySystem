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
            builder.HasKey(menu => menu.Id).IsClustered(false);
            builder.HasOne(menu => menu.ParentNode).WithMany(menu => menu.SubNodes).IsRequired(false).HasForeignKey("ParentNode_Id");
            builder.HasOne<InfoSystem>().WithMany().IsRequired().HasForeignKey(menu => menu.InfoSystemNo).OnDelete(DeleteBehavior.Restrict); ;
            builder.Property(menu => menu.Keywords).IsRequired().HasMaxLength(256);
            builder.Property(menu => menu.Name).IsRequired().HasMaxLength(32);

            //配置中间表
            builder
                .HasMany(menu => menu.Authorities)
                .WithMany(authority => authority.MenuLeaves)
                .UsingEntity<Dictionary<string, object>>("Menu_Authority",
                    x => x.HasOne<Authority>().WithMany().HasForeignKey("Authority_Id"),
                    x => x.HasOne<Menu>().WithMany().HasForeignKey("Menu_Id"),
                    map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Menu_Authority"));

            //忽略映射
            builder.Ignore(menu => menu.Number);
        }
    }
}
