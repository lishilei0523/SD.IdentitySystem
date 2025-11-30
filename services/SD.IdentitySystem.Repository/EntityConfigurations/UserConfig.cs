using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
using System.Collections.Generic;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 用户实体映射配置
    /// </summary>
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //配置属性
            builder.HasKey(user => user.Id).IsClustered(false);
            builder.HasAlternateKey(user => user.Number);
            builder.Property(user => user.Number).IsRequired().HasMaxLength(20);
            builder.Property(user => user.Password).IsRequired().HasMaxLength(32);
            builder.Property(user => user.PrivateKey).IsRequired().HasMaxLength(64);
            builder.Property(user => user.Keywords).IsRequired().HasMaxLength(256);

            //配置中间表
            builder
                .HasMany(user => user.Roles)
                .WithMany(role => role.Users)
                .UsingEntity<Dictionary<string, object>>("User_Role",
                    x => x.HasOne<Role>().WithMany().HasForeignKey("Role_Id"),
                    x => x.HasOne<User>().WithMany().HasForeignKey("User_Id"),
                    map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}User_Role"));

            //配置索引
            builder.HasIndex(user => user.AddedTime).IsUnique(false).IsClustered();
            builder.HasIndex(user => user.PrivateKey).IsUnique();

            //忽略映射
            builder.Ignore(user => user.Deleted);
            builder.Ignore(user => user.DeletedTime);
        }
    }
}
