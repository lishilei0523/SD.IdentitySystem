using SD.Infrastructure.PresentationBase;

namespace SD.IdentitySystem.IPresentation.Models.Outputs
{
    /// <summary>
    /// 权限模型
    /// </summary>
    public class Authority : ModelBase
    {
        #region 权限路径 —— string AuthorityPath
        /// <summary>
        /// 权限路径
        /// </summary>
        public string AuthorityPath { get; set; }
        #endregion

        #region 英文名称 —— string EnglishName
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }
        #endregion

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

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion


        //Others

        #region 信息系统编号 —— string SystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        public string SystemNo { get; set; }
        #endregion

        #region 信息系统名称 —— string SystemName
        /// <summary>
        /// 信息系统名称
        /// </summary>
        public string SystemName { get; set; }
        #endregion

        #region 应用程序类型 —— string ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public string ApplicationType { get; set; }
        #endregion
    }
}
