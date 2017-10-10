using SD.IdentitySystem.Domain.Entities;
using SD.Infrastructure.Constants;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 用户数据映射配置
    /// </summary>
    public class UserConfig : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public UserConfig()
        {
            this.HasMany(user => user.Roles).WithMany(role => role.Users).Map(map => map.ToTable(string.Format("{0}User_Role", WebConfigSetting.TablePrefix)));

            //设置编号长度
            this.Property(user => user.Number).HasMaxLength(20);

            //设置索引
            this.HasIndex("IX_Number", IndexType.Unique, table => table.Property(user => user.Number));
        }
    }
}
