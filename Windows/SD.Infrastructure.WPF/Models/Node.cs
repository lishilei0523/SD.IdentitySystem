using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SD.Infrastructure.WPF.Models
{
    /// <summary>
    /// 树节点
    /// </summary>
    public class Node : DependencyObject
    {
        #region # 字段及构造器

        /// <summary>
        /// 文件图标
        /// </summary>
        private const string FileIcon = "M703.952018 0.406439v255.89839h255.87581L703.952018 0.406439z m-63.991533 0H192.132657a127.983065 127.983065 0 0 0-127.960485 127.960485v767.672591a127.983065 127.983065 0 0 0 127.960485 127.960485H831.844763a127.983065 127.983065 0 0 0 127.983065-127.960485V320.273782H639.960485V0.406439z m63.991533 831.664123h-383.858876v-63.968952h383.858876v63.968952z m0-191.906857h-383.858876v-63.968953h383.858876v63.968953z m0-255.920971v63.968953h-383.858876v-63.968953h383.858876z";

        /// <summary>
        /// 文件夹图标
        /// </summary>
        private const string FolderIcon = "M928 444H820V330.4c0-17.7-14.3-32-32-32H473L355.7 186.2c-1.5-1.4-3.5-2.2-5.5-2.2H96c-17.7 0-32 14.3-32 32v592c0 17.7 14.3 32 32 32h698c13 0 24.8-7.9 29.7-20l134-332c1.5-3.8 2.3-7.9 2.3-12 0-17.7-14.3-32-32-32z m-180 0H238c-13 0-24.8 7.9-29.7 20L136 643.2V256h188.5l119.6 114.4H748V444z";

        /// <summary>
        /// 静态构造器
        /// </summary>
        static Node()
        {
            _IsChecked = DependencyProperty.Register(nameof(IsChecked), typeof(bool?), typeof(Node), new PropertyMetadata(false, OnIsCheckedChanged));
        }

        /// <summary>
        /// 无参构造器
        /// </summary>
        public Node()
        {
            this.SubNodes = new ObservableCollection<Node>();
        }

        /// <summary>
        /// 创建节点构造器
        /// </summary>
        /// <param name="id">标识Id</param>
        /// <param name="name">名称</param>
        /// <param name="isChecked">是否选中</param>
        /// <param name="parentNode">上级节点</param>
        public Node(Guid id, string name, bool? isChecked, Node parentNode)
            : this()
        {
            this.Id = id;
            this.Name = name;
            this.IsChecked = isChecked;
            this.ParentNode = parentNode;
            parentNode?.SubNodes.Add(this);
        }

        #endregion

        #region # 属性

        #region 标识Id —— Guid Id
        /// <summary>
        /// 标识Id
        /// </summary>
        public Guid Id { get; set; }
        #endregion

        #region 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region 是否勾选 —— bool? IsChecked

        /// <summary>
        /// 是否勾选依赖属性
        /// </summary>
        private static readonly DependencyProperty _IsChecked;

        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool? IsChecked
        {
            get { return base.GetValue(_IsChecked) == null ? (bool?)null : Convert.ToBoolean(base.GetValue(_IsChecked)); }
            set { base.SetValue(_IsChecked, (bool?)value); }
        }

        #endregion

        #region 只读属性 - 图标 —— string Icon
        /// <summary>
        /// 只读属性 - 图标
        /// </summary>
        public string Icon
        {
            get { return this.IsLeaf ? FileIcon : FolderIcon; }
        }
        #endregion

        #region 只读属性 - 是否有三种状态 —— bool IsThreeState
        /// <summary>
        /// 只读属性 - 是否有三种状态
        /// </summary>
        public bool IsThreeState
        {
            get { return !this.IsLeaf; }
        }
        #endregion

        #region 只读属性 - 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 只读属性 - 是否是根级节点
        /// </summary>
        public bool IsRoot
        {
            get { return this.ParentNode == null; }
        }
        #endregion

        #region 只读属性 - 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 只读属性 - 是否是叶子级节点
        /// </summary>
        public bool IsLeaf
        {
            get { return !this.SubNodes.Any(); }
        }
        #endregion

        #region 导航属性 - 上级节点 —— Node ParentNode
        /// <summary>
        /// 导航属性 - 上级节点
        /// </summary>
        public Node ParentNode { get; set; }
        #endregion

        #region 导航属性 - 下级节点集 —— ObservableCollection<Node> SubNodes
        /// <summary>
        /// 导航属性 - 下级节点集
        /// </summary>
        public ObservableCollection<Node> SubNodes { get; set; }
        #endregion

        #endregion

        #region # 方法

        #region 是否选中改变回调方法 —— static void OnIsCheckedChanged(DependencyObject dependencyObject...
        /// <summary>
        /// 是否选中改变回调方法
        /// </summary>
        private static void OnIsCheckedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Node node = (Node)dependencyObject;
            if (node.IsChecked == true)
            {
                CheckDown(node);
            }
            else if (node.IsChecked == false)
            {
                UncheckDown(node);
            }

            RefreshUp(node);

            node.IsChecked = (bool?)eventArgs.NewValue;
        }
        #endregion

        #region 向上刷新 —— static void RefreshUp(Node node)
        /// <summary>
        /// 向上刷新
        /// </summary>
        /// <param name="node">节点</param>
        private static void RefreshUp(Node node)
        {
            if (node.ParentNode != null)
            {
                if (node.ParentNode.SubNodes.All(x => x.IsChecked == true))
                {
                    node.ParentNode.IsChecked = true;
                }
                else if (node.ParentNode.SubNodes.All(x => x.IsChecked == false))
                {
                    node.ParentNode.IsChecked = false;
                }
                else
                {
                    node.ParentNode.IsChecked = null;
                }

                RefreshUp(node.ParentNode);
            }
        }
        #endregion

        #region 向下选中 —— static void CheckDown(Node node)
        /// <summary>
        /// 向下选中
        /// </summary>
        /// <param name="node">节点</param>
        private static void CheckDown(Node node)
        {
            foreach (Node subNode in node.SubNodes)
            {
                subNode.IsChecked = true;
                CheckDown(subNode);
            }
        }
        #endregion

        #region 向下取消选中 —— static void UncheckDown(Node node)
        /// <summary>
        /// 向下取消选中
        /// </summary>
        /// <param name="node">节点</param>
        private static void UncheckDown(Node node)
        {
            foreach (Node subNode in node.SubNodes)
            {
                subNode.IsChecked = false;
                UncheckDown(subNode);
            }
        }
        #endregion 

        #endregion
    }
}
