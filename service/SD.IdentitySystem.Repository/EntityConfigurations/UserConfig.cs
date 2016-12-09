using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.Constants;
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
        }
    }
}
