using System;
using System.Collections.Generic;
using System.Linq;
using SD.Toolkits.EntityFramework.Extensions;
using SD.UAC.Domain.Entities;
using SD.UAC.Domain.IRepositories.Interfaces;
using ShSoft.Framework2016.Infrastructure.Repository.EntityFrameworkProvider;

namespace SD.UAC.Repository.Implements
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
            return base.Find(x => x.InfoSystemKind.Number == systemKindNo).AsEnumerable();
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
            return base.FindIds(x => x.InfoSystemKind.Number == systemKindNo).AsEnumerable();
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
            PredicateBuilder<Authority> conditionBuilder = new PredicateBuilder<Authority>(x => x.InfoSystemKind.Number == systemKindNo);

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                conditionBuilder.And(x => x.Keywords.Contains(keywords));
            }

            return base.FindByPage(conditionBuilder.Build(), pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
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
            return base.Exists(x => x.InfoSystemKind.Number == systemKindNo && x.Id == authorityId);
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
            return base.Exists(x => x.InfoSystemKind.Number == systemKindNo && x.AuthorityPath == authorityPath);
        }
        #endregion
    }
}
