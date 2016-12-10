using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;
using ShSoft.Infrastructure.DTOBase;
using System.Collections.Generic;
using System.Linq;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 信息系统呈现器实现
    /// </summary>
    public class InfoSystemPresenter : IInfoSystemPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限服务接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务接口</param>
        public InfoSystemPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemView> GetInfoSystems()
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystemView> GetInfoSystems()
        {
            IEnumerable<InfoSystemInfo> systemInfos = this._authorizationContract.GetInfoSystems();

            IEnumerable<InfoSystemView> systemViews = systemInfos.Select(x => x.ToViewModel());

            return systemViews;
        }
        #endregion

        #region # 分页获取信息系统列表 —— PageModel<InfoSystemView> GetInfoSystems(string keywords...
        /// <summary>
        /// 分页获取信息系统列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>信息系统列表</returns>
        public PageModel<InfoSystemView> GetInfoSystems(string keywords, int pageIndex, int pageSize)
        {
            PageModel<InfoSystemInfo> pageModel = this._authorizationContract.GetInfoSystemsByPage(keywords, pageIndex, pageSize);

            IEnumerable<InfoSystemView> systemViews = pageModel.Datas.Select(x => x.ToViewModel());

            return new PageModel<InfoSystemView>(systemViews, pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, pageModel.RowCount);

        }
        #endregion

        #region # 获取信息系统 —— InfoSystemView GetInfoSystem(string systemNo)
        /// <summary>
        /// 获取信息系统
        /// </summary>
        /// <param name="systemNo">信息系统编号</param>
        /// <returns>信息系统</returns>
        public InfoSystemView GetInfoSystem(string systemNo)
        {
            InfoSystemInfo systemInfo = this._authorizationContract.GetInfoSystem(systemNo);

            return systemInfo.ToViewModel();
        }
        #endregion
    }
}
