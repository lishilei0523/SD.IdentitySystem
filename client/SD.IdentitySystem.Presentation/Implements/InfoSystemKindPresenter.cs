using System.Collections.Generic;
using System.Linq;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.ViewModels.Outputs;
using SD.IdentitySystem.Presentation.Maps;

namespace SD.IdentitySystem.Presentation.Implements
{
    /// <summary>
    /// 信息系统类别呈现器实现
    /// </summary>
    public class InfoSystemKindPresenter : IInfoSystemKindPresenter
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
        public InfoSystemKindPresenter(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion

        #region # 获取信息系统类别列表 —— IEnumerable<InfoSystemKindView> GetSystemKinds()
        /// <summary>
        /// 获取信息系统类别列表
        /// </summary>
        /// <returns>信息系统类别列表</returns>
        public IEnumerable<InfoSystemKindView> GetSystemKinds()
        {
            IEnumerable<InfoSystemKindInfo> systemKindInfos = this._authorizationContract.GetSystemKinds();

            IEnumerable<InfoSystemKindView> systemKindViews = systemKindInfos.Select(x => x.ToViewModel());

            return systemKindViews;
        }
        #endregion
    }
}
