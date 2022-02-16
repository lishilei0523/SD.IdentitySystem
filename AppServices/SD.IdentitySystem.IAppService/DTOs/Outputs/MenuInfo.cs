using SD.Infrastructure.Constants;
using SD.Infrastructure.DTOBase;
using System;
using System.Runtime.Serialization;

namespace SD.IdentitySystem.IAppService.DTOs.Outputs
{
    /// <summary>
    /// 菜单数据传输对象
    /// </summary>
    [DataContract(Namespace = "http://SD.IdentitySystem.IAppService.DTOs.Outputs")]
    public class MenuInfo : BaseDTO
    {
        #region 信息系统编号 —— string InfoSystemNo
        /// <summary>
        /// 信息系统编号
        /// </summary>
        [DataMember]
        public string InfoSystemNo { get; set; }
        #endregion

        #region 应用程序类型 —— ApplicationType ApplicationType
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [DataMember]
        public ApplicationType ApplicationType { get; set; }
        #endregion

        #region 链接地址 —— string Url
        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        public string Url { get; set; }
        #endregion

        #region 路径 —— string Path
        /// <summary>
        /// 路径
        /// </summary>
        [DataMember]
        public string Path { get; set; }
        #endregion

        #region 图标 —— string Icon
        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        public string Icon { get; set; }
        #endregion

        #region 排序 —— int Sort
        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }
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

        #region 上级菜单Id —— Guid? ParentMenuId
        /// <summary>
        /// 上级菜单Id
        /// </summary>
        [DataMember]
        public Guid? ParentMenuId { get; set; }
        #endregion


        //导航属性

        #region 导航属性 - 信息系统 —— InfoSystemInfo InfoSystemInfo
        /// <summary>
        /// 导航属性 - 信息系统
        /// </summary>
        [DataMember]
        public InfoSystemInfo InfoSystemInfo { get; set; }
        #endregion
    }
}
