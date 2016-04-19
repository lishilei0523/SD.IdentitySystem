using System.Data.Entity.ModelConfiguration;
using ShSoft.Framework2016.Infrastructure.Constants;
using ShSoft.UAC.Domain.Entities;

namespace ShSoft.UAC.Repository.EntityConfigurations
{
    /// <summary>
    /// 角色数据映射配置
    /// </summary>
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public RoleConfig()
        {
            this.HasMany(role => role.Authorities).WithMany(authority => authority.Roles).Map(map => map.ToTable(string.Format("{0}Role_Authority", WebConfigSetting.TablePrefix)));
        }
    }
}
