using System.Collections.Generic;
using SD.UAC.Domain.Entities;
using ShSoft.Framework2016.Infrastructure.IRepository;

namespace SD.UAC.Domain.IRepositories.Interfaces
{
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        #region # 获取角色集 —— IEnumerable<Role> FindBySystem(string systemNo)
        /// <summary>
        /// 获取角色集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>角色集</returns>
        IEnumerable<Role> FindBySystem(string systemNo);
        #endregion

        #region # 分页获取角色集 —— IEnumerable<Role> FindByPage(string systemNo, string keywords...
        /// <summary>
        /// 分页获取角色集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>角色集</returns>
        IEnumerable<Role> FindByPage(string systemNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion
    }
}
