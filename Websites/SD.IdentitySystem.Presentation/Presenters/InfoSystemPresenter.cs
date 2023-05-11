using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.DTOBase;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 信息系统呈现器实现
    /// </summary>
    public class InfoSystemPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public InfoSystemPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取信息系统 —— InfoSystem GetInfoSystem(string infoSystemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        public InfoSystem GetInfoSystem(string infoSystemNo)
        {
            InfoSystemInfo infoSystemInfo = this._authorizationContract.GetInfoSystem(infoSystemNo);

            return infoSystemInfo.ToModel();
        }
        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystem> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystem> GetInfoSystems()
        {
            IEnumerable<InfoSystemInfo> infoSystemInfos = this._authorizationContract.GetInfoSystems(null);
            IEnumerable<InfoSystem> infoSystems = infoSystemInfos.Select(x => x.ToModel());

            return infoSystems;
        }
        #endregion

        #region # 分页获取信息系统列表 —— PageModel<InfoSystem> GetInfoSystemsByPage(string keywords...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>信息系统列表</returns>
        public PageModel<InfoSystem> GetInfoSystemsByPage(string keywords, int pageIndex, int pageSize)
        {
            PageModel<InfoSystemInfo> pageModel = this._authorizationContract.GetInfoSystemsByPage(keywords, pageIndex, pageSize);

            IEnumerable<InfoSystem> infoSystems = pageModel.Datas.Select(x => x.ToModel());

            return new PageModel<InfoSystem>(infoSystems, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);

        }
        #endregion
    }
}
