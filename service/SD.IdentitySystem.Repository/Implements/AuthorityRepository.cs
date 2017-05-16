using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SD.IdentitySystem.Domain.Entities;
using SD.IdentitySystem.Domain.IRepositories.Interfaces;
using SD.Infrastructure.Repository.EntityFramework;

namespace SD.IdentitySystem.Repository.Implements
{
    /// <summary>
    /// 权限仓储实现
    /// </summary>
    public class AuthorityRepository : EFAggRootRepositoryProvider<Authority>, IAuthorityRepository
    {
        #region # 根据信息系统获取权限集 —— IEnumerable<Authority> FindBySystem(...
        /// <summary>
        /// 根据信息系统获取权限集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> FindBySystem(string systemNo)
        {
            return base.Find(x => x.SystemNo == systemNo).AsEnumerable();
        }
        #endregion

        #region # 根据信息系统获取权限Id集 —— IEnumerable<Guid> FindAuthorityIds(string systemNo)
        /// <summary>
        /// 根据信息系统获取权限Id集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>权限Id集</returns>
        public IEnumerable<Guid> FindAuthorityIds(string systemNo)
        {
            return base.FindIds(x => x.SystemNo == systemNo).AsEnumerable();
        }
        #endregion

        #region # 根据菜单获取权限列表 —— IEnumerable<Authority> FindByMenu(Guid menuId)
        /// <summary>
        /// 根据菜单获取权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns>权限列表</returns>
        public IEnumerable<Authority> FindByMenu(Guid menuId)
        {
            return base.Find(x => x.MenuLeaves.Any(y => y.Id == menuId)).AsEnumerable();
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

        #region # 根据角色获取权限列表 —— IEnumerable<Authority> FindByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限列表</returns>
        public IEnumerable<Authority> FindByRole(IEnumerable<Guid> roleIds)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    x.Roles.Any(y => roleIds.Contains(y.Id));

            return base.Find(condition).AsEnumerable();
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

        #region # 根据角色获取权限Id列表 —— IEnumerable<Guid> FindIdsByRole(IEnumerable<Guid> roleIds)
        /// <summary>
        /// 根据角色获取权限Id列表
        /// </summary>
        /// <param name="roleIds">角色Id集</param>
        /// <returns>权限Id列表</returns>
        public IEnumerable<Guid> FindIdsByRole(IEnumerable<Guid> roleIds)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    x.Roles.Any(y => roleIds.Contains(y.Id));

            return base.Find(condition).Select(x => x.Id).AsEnumerable();
        }
        #endregion

        #region # 分页获取权限集 —— IEnumerable<Authority> FindByPage(string systemNo...
        /// <summary>
        /// 分页获取权限集
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录条数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>权限集</returns>
        public IEnumerable<Authority> FindByPage(string systemNo, string keywords, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(systemNo) || x.SystemNo == systemNo) &&
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords));

            return base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount).AsEnumerable();
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

        #region # 是否存在给定权限 ——  bool ExistsPath(string assemblyName, string @namespace
        /// <summary>
        /// 是否存在给定权限
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <returns>是否存在</returns>
        public bool ExistsPath(string assemblyName, string @namespace, string className, string methodName)
        {
            Expression<Func<Authority, bool>> condition =
                x =>
                    x.AssemblyName == assemblyName &&
                    x.Namespace == @namespace &&
                    x.ClassName == className &&
                    x.MethodName == methodName;

            return base.Exists(condition);
        }
        #endregion
    }
}
