using System.Data.Entity;
using ShSoft.Framework2016.Infrastructure.IRepository;

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

        }
    }
}
