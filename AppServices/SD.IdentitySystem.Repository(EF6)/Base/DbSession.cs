using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework.Base;

namespace SD.IdentitySystem.Repository.Base
{
    /// <summary>
    /// EF上下文
    /// </summary>
    internal class DbSession : DbSessionBase
    {
        /// <summary>
        /// 默认构造器
        /// </summary>
        public DbSession()
            : base(GlobalSetting.WriteConnectionString)
        {

        }
    }
}
