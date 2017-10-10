using SD.IdentitySystem.Domain.Entities;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 服务器实体映射配置
    /// </summary>
    public class ServerConfig : EntityTypeConfiguration<Server>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public ServerConfig()
        {
            //设置编号长度
            this.Property(server => server.Number).HasMaxLength(32);

            //设置唯一键索引
            this.HasIndex("IX_Number", IndexType.Unique, table => table.Property(server => server.Number));
        }
    }
}
