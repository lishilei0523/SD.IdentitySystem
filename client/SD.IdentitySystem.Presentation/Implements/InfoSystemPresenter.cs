using System.Collections.Generic;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 信息系统呈现器实现
    /// </summary>
    public class InfoSystemPresenter : IInfoSystemPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户服务接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="userContract">用户服务接口</param>
        public InfoSystemPresenter(IUserContract userContract)
        {
            this._userContract = userContract;
        }

        #endregion

        #region # 获取信息系统列表 —— IEnumerable<InfoSystemView> GetInfoSystems(string systemKindNo)
        /// <summary>
        /// 获取信息系统列表
        /// </summary>
        /// <param name="systemKindNo">信息系统类别编号</param>
        /// <returns>信息系统列表</returns>
        public IEnumerable<InfoSystemView> GetInfoSystems(string systemKindNo)
        {
            //TODO 添加服务接口

            //IEnumerable<InfoSystemInfo> systemInfos=this._userContract.GetInfoSystems()

            return null;
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
            InfoSystemInfo systemInfo = this._userContract.GetInfoSystem(systemNo);

            return systemInfo.ToViewModel();
        }
        #endregion
    }
}
