using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IDomainServices;

namespace SD.IdentitySystem.DomainService.Implements
{
    /// <summary>
    /// 信息系统领域服务实现
    /// </summary>
    public class InfoSystemService : IInfoSystemService
    {
        #region # 获取聚合根实体关键字 —— string GetKeywords(InfoSystem entity)
        /// <summary>
        /// 获取聚合根实体关键字
        /// </summary>
        /// <param name="entity">聚合根实体对象</param>
        /// <returns>关键字</returns>
        public string GetKeywords(InfoSystem entity)
        {
            return entity.Keywords;
        }
        #endregion
    }
}
