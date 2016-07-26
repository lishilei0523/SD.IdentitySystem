using System.Data.Entity;
using System.Linq;
using SD.UAC.Domain.Entities;
using ShSoft.Infrastructure.RepositoryBase;

namespace SD.UAC.Repository.Base
{
    /// <summary>
    /// 数据初始化器实现
    /// </summary>
    public class DataInitializer : IDataInitializer
    {
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// 构造器
        /// </summary>
        public DataInitializer()
        {
            this._dbContext = DbSession.CommandInstance;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public DataInitializer(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// 初始化基础数据
        /// </summary>
        public void Initialize()
        {
            this.InitInfoSystemKind();
        }


        /// <summary>
        /// 初始化信息系统类别
        /// </summary>
        private void InitInfoSystemKind()
        {
            if (!this._dbContext.Set<InfoSystemKind>().Any())
            {
                InfoSystemKind manageCenterKind = new InfoSystemKind("X1", "管理中心系统类别");

                this._dbContext.Set<InfoSystemKind>().Add(manageCenterKind);

                this._dbContext.SaveChanges();
            }
        }
    }
}
