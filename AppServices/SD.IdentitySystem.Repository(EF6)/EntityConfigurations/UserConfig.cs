using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 用户实体映射配置
    /// </summary>
    public class UserConfig : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public UserConfig()
        {
            //配置属性
            this.HasKey(user => user.Id, index => index.IsClustered(false));
            this.Property(user => user.Number).IsRequired().HasMaxLength(20);
            this.Property(user => user.Keywords).IsRequired().HasMaxLength(256);
            this.Property(user => user.Password).IsRequired().HasMaxLength(32);
            this.Property(user => user.PrivateKey).IsRequired().HasMaxLength(64);

            //配置中间表
            this.HasMany(user => user.Roles).WithMany(role => role.Users).Map(map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}User_Role"));

            //配置索引
            this.HasIndex("IX_User_AddedTime", IndexType.Clustered, table => table.Property(user => user.AddedTime));
            this.HasIndex("IX_User_Number", IndexType.Unique, table => table.Property(user => user.Number));
            this.HasIndex("IX_User_PrivateKey", IndexType.Unique, table => table.Property(user => user.PrivateKey));

            //忽略映射
            this.Ignore(user => user.Deleted);
            this.Ignore(user => user.DeletedTime);
        }
    }
}
