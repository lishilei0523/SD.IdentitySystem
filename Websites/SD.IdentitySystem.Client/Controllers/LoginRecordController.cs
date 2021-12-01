using SD.IdentitySystem.IPresentation.Interfaces;
using SD.IdentitySystem.IPresentation.Models;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.EasyUI;
using System;
using System.Web.Mvc;

namespace SD.IdentitySystem.Client.Controllers
{
    /// <summary>
    /// 用户登录记录控制器
    /// </summary>
    public class LoginRecordController : Controller
    {
        #region # 字段及构造器

        /// <summary>
        /// 用户呈现器接口
        /// </summary>
        private readonly IUserPresenter _userPresenter;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public LoginRecordController(IUserPresenter userPresenter)
        {
            this._userPresenter = userPresenter;
        }

        #endregion


        //视图部分

        #region # 加载首页视图 —— ViewResult Index()
        /// <summary>
        /// 加载首页视图
        /// </summary>
        /// <returns>首页视图</returns>
        [HttpGet]
        [RequireAuthorization("登录记录首页视图")]
        public ViewResult Index()
        {
            return base.View();
        }
        #endregion


        //查询部分

        #region # 分页获取登录记录列表 —— JsonResult GetLoginRecordsByPage(string keywords...
        /// <summary>
        /// 分页获取登录记录列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="page">页码</param>
        /// <param name="rows">页容量</param>
        /// <returns>登录记录列表</returns>
        [RequireAuthorization("分页获取登录记录列表")]
        public JsonResult GetLoginRecordsByPage(string keywords, DateTime? startTime, DateTime? endTime, int page, int rows)
        {
            PageModel<LoginRecord> pageModel = this._userPresenter.GetLoginRecordsByPage(keywords, startTime, endTime, page, rows);
            Grid<LoginRecord> grid = new Grid<LoginRecord>(pageModel.RowCount, pageModel.Datas);

            return base.Json(grid, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
