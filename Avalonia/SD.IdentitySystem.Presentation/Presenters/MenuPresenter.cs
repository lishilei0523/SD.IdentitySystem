using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.IdentitySystem.Presentation.Maps;
using SD.IdentitySystem.Presentation.Models;
using SD.Infrastructure.Avalonia.Models;
using SD.Infrastructure.Constants;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Extensions;

namespace SD.IdentitySystem.Presentation.Presenters
{
    /// <summary>
    /// 菜单呈现器
    /// </summary>
    public class MenuPresenter
    {
        #region # 字段及构造器

        /// <summary>
        /// 权限管理服务契约接口代理
        /// </summary>
        private readonly ServiceProxy<IAuthorizationContract> _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public MenuPresenter(ServiceProxy<IAuthorizationContract> authorizationContract)
        {
            this._authorizationContract = authorizationContract;
        }

        #endregion


        //Public

        #region # 获取菜单树 —— ICollection<Node> GetMenuTree(string infoSystemNo...
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单树</returns>
        public ICollection<Node> GetMenuTree(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this.GetMenus(infoSystemNo, applicationType);
            ICollection<Node> menuTree = menus.ToTree(null);

            return menuTree;
        }
        #endregion

        #region # 获取菜单树列表 —— IEnumerable<Menu> GetMenuTreeList(string infoSystemNo...
        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单TreeGrid</returns>
        public IEnumerable<Menu> GetMenuTreeList(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<Menu> menus = this.GetMenus(infoSystemNo, applicationType);
            IEnumerable<Menu> menuTreeList = menus.ToTreeList();

            return menuTreeList;
        }
        #endregion


        //Private

        #region # 获取菜单列表 —— IEnumerable<Menu> GetMenus(string infoSystemNo...
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        /// <returns>菜单列表</returns>
        private IEnumerable<Menu> GetMenus(string infoSystemNo, ApplicationType? applicationType)
        {
            IEnumerable<MenuInfo> menuInfos = this._authorizationContract.Channel.GetMenus(null, infoSystemNo, applicationType);
            IEnumerable<Menu> menus = menuInfos.OrderBy(x => x.Sort).Select(x => x.ToModel());

            return menus;
        }
        #endregion
    }
}
