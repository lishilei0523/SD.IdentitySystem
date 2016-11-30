using System;
using System.Collections.Generic;

namespace SD.IdentitySystem.IPresentation.ViewModels.Formats.EasyUI
{
    /// <summary>
    /// EasyUI 树节点格式化模型
    /// </summary>
    public class Node
    {
        #region # 构造器

        /// <summary>
        /// 无参构造器
        /// </summary>
        public Node()
        {
            this.children = new HashSet<Node>();
        }

        /// <summary>
        /// 基础构造器
        /// </summary>
        /// <param name="id">标识Id</param>
        /// <param name="text">文本</param>
        /// <param name="state">状态</param>
        /// <param name="checked">是否选中</param>
        /// <param name="attributes">自定义特性</param>
        public Node(Guid id, string text, string state, bool @checked, object attributes)
            : this()
        {
            this.id = id;
            this.text = text;
            this.state = state;
            this.@checked = @checked;
            this.attributes = attributes;
        }

        #endregion

        #region # 属性

        #region 标识Id —— Guid id
        /// <summary>
        /// 标识Id
        /// </summary>
        public Guid id { get; set; }
        #endregion

        #region 文本 —— string text
        /// <summary>
        /// 文本
        /// </summary>
        public string text { get; set; }
        #endregion

        #region 状态 —— string state
        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>"open"或"closed"</remarks>
        public string state { get; set; }
        #endregion

        #region 是否选中 —— bool @checked
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool @checked { get; set; }
        #endregion

        #region 自定义特性 —— object attributes
        /// <summary>
        /// 自定义特性
        /// </summary>
        public object attributes { get; set; }
        #endregion

        #region 子节点集 —— ICollection<Node> children
        /// <summary>
        /// 子节点集
        /// </summary>
        public ICollection<Node> children { get; set; }
        #endregion

        #endregion
    }
}
