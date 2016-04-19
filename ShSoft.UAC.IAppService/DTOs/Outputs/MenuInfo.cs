using System.Runtime.Serialization;
using ShSoft.Framework2016.Infrastructure.IDTO;

namespace ShSoft.UAC.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 菜单数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://ShSoft.UAC.IAppService.DTOs.Outputs")]
    public class MenuInfo : BaseDTO
    {
        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        public string Url { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        public string Icon { get; set; }
        #endregion

        #region 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 是否是根级节点
        /// </summary>
        [DataMember]
        public bool IsRoot { get; set; }
        #endregion

        #region 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 是否是叶子级节点
        /// </summary>
        [DataMember]
        public bool IsLeaf { get; set; }
        #endregion


        //导航属性

        #region 信息系统类别 —— InfoSystemKindInfo InfoSystemKindInfo
        /// <summary>
        /// 信息系统类别
        /// </summary>
        [DataMember]
        public InfoSystemKindInfo InfoSystemKindInfo { get; set; }
        #endregion

        #region 父级菜单 —— MenuInfo ParentMenu
        /// <summary>
        /// 父级菜单
        /// </summary>
        [DataMember]
        public MenuInfo ParentMenu { get; set; }
        #endregion
    }
}
