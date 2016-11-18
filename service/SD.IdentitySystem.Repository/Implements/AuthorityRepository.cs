using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using ShSoft.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 权限仓储实现
    /// </summary>
    public class AuthorityRepository : EFRepositoryProvider<Authority>, IAuthorityRepository
    {
        #region # 根据信息系统类别获取权限集 —— IEnumerable<Authority> FindBySystemKind(...
        /// <summary>
        /// 根据信息系统类别获取权限集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> FindBySystemKind(string systemKindNo)
        {
            return base.Find(x => x.SystemKindNo == systemKindNo).AsEnumerable();
        }
        #endregion

        #region # 根据信息系统类别获取权限Id集 —— IEnumerable<Guid> FindAuthorityIds(string systemKindNo)
        /// <summary>
        /// 根据信息系统类别获取权限Id集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>权限Id集</returns>
        public IEnumerable<Guid> FindAuthorityIds(string systemKindNo)
        {
            return base.FindIds(x => x.SystemKindNo == systemKindNo).AsEnumerable();
        }
        #endregion

        #region # 根据角色获取权限列表 —— IEnumerable<Authority> FindByRole(...
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<Authority> FindByRole(Guid roleId)
        {
            return base.Find(x => x.Roles.Any(y => y.Id == roleId)).AsEnumerable();
        }
        #endregion

        #region # 根据角色获取权限Id列表 —— IEnumerable<Authority> FindIdsByRole(...
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>权限Id列表</returns>
        public IEnumerable<Guid> FindIdsByRole(Guid roleId)
        {
            return base.Find(x => x.Roles.Any(y => y.Id == roleId)).Select(x => x.Id).AsEnumerable();
        }
        #endregion

        #region # 分页获取权限集 —— IEnumerable<Authority> FindByPage(string systemKindNo...
        /// <summary>
        /// 分页获取权限集
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> FindByPage(string systemKindNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(systemKindNo) || x.SystemKindNo == systemKindNo) &&
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords));

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
        }
        #endregion

        #region # 是否存在给定权限 —— bool Exists(string systemKindNo, Guid authorityId)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityId">权限Id</param>
        /// <returns>是否存在</returns>
        public bool Exists(string systemKindNo, Guid authorityId)
        {
            return base.Exists(x => x.SystemKindNo == systemKindNo && x.Id == authorityId);
        }
        #endregion

        #region # 是否存在给定权限 —— bool Exists(string systemKindNo, string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        public bool Exists(string systemKindNo, string authorityPath)
        {
            return base.Exists(x => x.SystemKindNo == systemKindNo && x.AuthorityPath == authorityPath);
        }
        #endregion

        #region # 是否存在给定权限 —— bool ExistsPath(string authorityPath)
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="authorityPath">权限路径</param>
        /// <returns>是否存在</returns>
        public bool ExistsPath(string authorityPath)
        {
            return base.Exists(x => x.AuthorityPath == authorityPath);
        }
        #endregion
    }
}
