using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 角色实体映射配置
    /// </summary>
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public RoleConfig()
        {
            //配置属性
            this.HasKey(role => role.Id, index => index.IsClustered(false));
            this.Property(role => role.Name).IsRequired().HasMaxLength(32);
            this.Property(role => role.Keywords).IsRequired().HasMaxLength(256);
            this.Property(role => role.InfoSystemNo).IsRequired().HasMaxLength(16);

            //配置中间表
            this.HasMany(role => role.Authorities).WithMany(authority => authority.Roles).Map(map => map.ToTable($"{FrameworkSection.Setting.EntityTablePrefix.Value}Role_Authority"));

            //配置索引
            this.HasIndex("IX_Role_AddedTime", IndexType.Clustered, table => table.Property(role => role.AddedTime));
            this.HasIndex("IX_Role_InfoSystemNo", IndexType.Nonclustered, table => table.Property(role => role.InfoSystemNo));

            //忽略映射
            this.Ignore(role => role.Deleted);
            this.Ignore(role => role.DeletedTime);
        }
    }
}
