using System.Linq;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.Redis;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 信息系统类别仓储实现
    /// </summary>
    public class InfoSystemKindRepository : RedisRepositoryProvider<InfoSystemKind>, IInfoSystemKindRepository
    {
        #region # 获取信息系统类别列表 —— override IQueryable<InfoSystemKind> FindAllInner()
        /// <summary>
        /// 获取信息系统类别列表
        /// </summary>
        /// <returns>信息系统类别列表</returns>
        protected override IQueryable<InfoSystemKind> FindAllInner()
        {
            return base.FindAllInner().OrderBy(x => x.Number);
        }
        #endregion
    }
}
