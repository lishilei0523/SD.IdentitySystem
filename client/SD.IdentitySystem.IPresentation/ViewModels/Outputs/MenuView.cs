using ShSoft.Infrastructure;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 菜单视图模型
    /// </summary>
    public class MenuView : ViewModel
    {
        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        #endregion

        #region 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 是否是根级节点
        /// </summary>
        public bool IsRoot { get; set; }
        #endregion

        #region 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 是否是叶子级节点
        /// </summary>

        public bool IsLeaf { get; set; }
        #endregion


        //Others

        #region 信息系统类别编号 —— string SystemKindNo
        /// <summary>
        /// 信息系统类别编号
        /// </summary>
        public string SystemKindNo { get; set; }
        #endregion

        #region 信息系统类别名称 —— string SystemKindName
        /// <summary>
        /// 信息系统类别名称
        /// </summary>
        public string SystemKindName { get; set; }
        #endregion

        #region 导航属性 - 父级菜单 —— MenuView Parent
        /// <summary>
        /// 导航属性 - 父级菜单
        /// </summary>
        public MenuView Parent { get; set; }
        #endregion
    }
}
