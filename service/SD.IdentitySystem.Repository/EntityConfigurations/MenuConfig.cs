using System.Data.Entity.ModelConfiguration;
using SD.IdentitySystem.Domain.Entities;
using ShSoft.Infrastructure.Constants;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 菜单数据映射配置
    /// </summary>
    public class MenuConfig : EntityTypeConfiguration<Menu>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public MenuConfig()
        {
            this.HasMany(menu => menu.Authorities).WithMany(authority => authority.MenuLeaves).Map(map => map.ToTable(string.Format("{0}Menu_Authority", WebConfigSetting.TablePrefix)));
        }
    }
}
