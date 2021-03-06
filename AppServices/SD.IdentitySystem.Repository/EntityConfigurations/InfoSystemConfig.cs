﻿using SD.IdentitySystem.Domain.Entities;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.IdentitySystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 信息系统实体映射配置
    /// </summary>
    public class InfoSystemConfig : EntityTypeConfiguration<InfoSystem>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public InfoSystemConfig()
        {
            //设置编号长度
            this.Property(system => system.Number).HasMaxLength(16);

            //设置索引
            this.HasIndex("IX_Number", IndexType.Unique, table => table.Property(system => system.Number));
        }
    }
}
