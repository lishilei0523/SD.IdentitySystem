using ShSoft.Infrastructure;

namespace SD.IdentitySystem.IPresentation.ViewModels.Outputs
{
    /// <summary>
    /// 权限视图模型
    /// </summary>
    public class AuthorityView : ViewModel
    {
        #region 程序集名称 —— string AssemblyName
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
        #endregion

        #region 命名空间 —— string Namespace
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }
        #endregion

        #region 类名 —— string ClassName
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }
        #endregion

        #region 方法名 —— string MethodName
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }
        #endregion

        #region 英文名 —— string EnglishName
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        #endregion

        #region 权限描述 —— string Description
        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        public string AuthorityPath { get; set; }
        #endregion
    }
}
