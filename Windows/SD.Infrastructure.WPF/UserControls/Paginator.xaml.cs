using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SD.Infrastructure.WPF.UserControls
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public partial class Paginator : UserControl
    {
        #region # 构造器

        /// <summary>
        /// 最小页码
        /// </summary>
        private const int MinPageIndex = 1;

        /// <summary>
        /// 默认页容量
        /// </summary>
        private const int DefaultPageSize = 15;

        /// <summary>
        /// 页容量集合
        /// </summary>
        private static readonly int[] _PageSizes;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static Paginator()
        {
            //初始化页容量集合
            Paginator._PageSizes = new[] { 5, 10, Paginator.DefaultPageSize, 20, 30, 50 };

            //注册依赖属性
            Paginator._PageIndex = DependencyProperty.Register(nameof(Paginator.PageIndex), typeof(int), typeof(Paginator), new PropertyMetadata(Paginator.MinPageIndex, Paginator.OnPageIndexChanged));
            Paginator._PageSize = DependencyProperty.Register(nameof(Paginator.PageSize), typeof(int), typeof(Paginator), new PropertyMetadata(Paginator.DefaultPageSize, Paginator.OnPageSizeChanged));
            Paginator._RowCount = DependencyProperty.Register(nameof(Paginator.RowCount), typeof(int), typeof(Paginator), new PropertyMetadata(0, Paginator.OnRowCountChanged));
            Paginator._PageCount = DependencyProperty.Register(nameof(Paginator.PageCount), typeof(int), typeof(Paginator), new PropertyMetadata(0, Paginator.OnPageCountChanged));
            Paginator._StartRowIndex = DependencyProperty.Register(nameof(Paginator.StartRowIndex), typeof(int), typeof(Paginator), new PropertyMetadata(1, Paginator.OnStartRowIndexChanged));
            Paginator._EndRowIndex = DependencyProperty.Register(nameof(Paginator.EndRowIndex), typeof(int), typeof(Paginator), new PropertyMetadata(0));

            //注册路由事件
            Paginator._RefreshEvent = EventManager.RegisterRoutedEvent(nameof(Paginator.Refresh), RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Paginator));
        }

        /// <summary>
        /// 实例构造器
        /// </summary>
        public Paginator()
        {
            this.InitializeComponent();
        }

        #endregion

        #region # 依赖属性

        #region 页码 —— int PageIndex

        /// <summary>
        /// 页码依赖属性
        /// </summary>
        private static readonly DependencyProperty _PageIndex;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return Convert.ToInt32(base.GetValue(Paginator._PageIndex)); }
            set { base.SetValue(Paginator._PageIndex, value); }
        }

        #endregion

        #region 页容量 —— int PageSize

        /// <summary>
        /// 页容量依赖属性
        /// </summary>
        private static readonly DependencyProperty _PageSize;

        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize
        {
            get { return Convert.ToInt32(base.GetValue(Paginator._PageSize)); }
            set { base.SetValue(Paginator._PageSize, value); }
        }

        #endregion

        #region 总记录数 —— int RowCount

        /// <summary>
        /// 总记录数依赖属性
        /// </summary>
        private static readonly DependencyProperty _RowCount;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowCount
        {
            get { return Convert.ToInt32(base.GetValue(Paginator._RowCount)); }
            set { base.SetValue(Paginator._RowCount, value); }
        }

        #endregion

        #region 总页数 —— int PageCount

        /// <summary>
        /// 总页数依赖属性
        /// </summary>
        private static readonly DependencyProperty _PageCount;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return Convert.ToInt32(base.GetValue(Paginator._PageCount)); }
            set { base.SetValue(Paginator._PageCount, value); }
        }

        #endregion

        #region 起始行 —— int StartRowIndex

        /// <summary>
        /// 起始行依赖属性
        /// </summary>
        private static readonly DependencyProperty _StartRowIndex;

        /// <summary>
        /// 起始行
        /// </summary>
        public int StartRowIndex
        {
            get { return Convert.ToInt32(base.GetValue(Paginator._StartRowIndex)); }
            set { base.SetValue(Paginator._StartRowIndex, value); }
        }

        #endregion

        #region 终止行 —— int EndRowIndex

        /// <summary>
        /// 终止行依赖属性
        /// </summary>
        private static readonly DependencyProperty _EndRowIndex;

        /// <summary>
        /// 终止行
        /// </summary>
        public int EndRowIndex
        {
            get { return Convert.ToInt32(base.GetValue(Paginator._EndRowIndex)); }
            set { base.SetValue(Paginator._EndRowIndex, value); }
        }

        #endregion

        #region 只读属性 - 页容量列表 —— int[] PageSizes
        /// <summary>
        /// 只读属性 - 页容量列表
        /// </summary>
        public int[] PageSizes
        {
            get { return Paginator._PageSizes; }
        }
        #endregion

        #endregion

        #region # 回调方法

        #region 页码改变回调方法 —— static void OnPageIndexChanged(...
        /// <summary>
        /// 页码改变回调方法
        /// </summary>
        private static void OnPageIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Paginator paginator = (Paginator)dependencyObject;

            int pageIndex = Convert.ToInt32(eventArgs.NewValue);
            if (pageIndex <= Paginator.MinPageIndex)
            {
                pageIndex = Paginator.MinPageIndex;
            }
            if (paginator.PageCount != 0 && pageIndex > paginator.PageCount)
            {
                pageIndex = paginator.PageCount;
            }

            paginator.PageIndex = pageIndex;
            paginator.StartRowIndex = (paginator.PageIndex * paginator.PageSize) - paginator.PageSize + 1;
            paginator.EndRowIndex = (paginator.StartRowIndex + paginator.PageSize) > paginator.RowCount
                ? paginator.RowCount
                : paginator.StartRowIndex - 1 + paginator.PageSize;

            //挂起路由事件
            paginator.RaiseEvent(new RoutedEventArgs(Paginator._RefreshEvent, paginator));
        }
        #endregion

        #region 页容量改变回调方法 —— static void OnPageSizeChanged(...
        /// <summary>
        /// 页容量改变回调方法
        /// </summary>
        private static void OnPageSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Paginator paginator = (Paginator)dependencyObject;

            int pageSize = Convert.ToInt32(eventArgs.NewValue);
            if (!Paginator._PageSizes.Contains(pageSize))
            {
                pageSize = Paginator.DefaultPageSize;
            }

            paginator.PageSize = pageSize;
            paginator.PageCount = (int)Math.Ceiling(paginator.RowCount * 1.0 / paginator.PageSize);
            paginator.StartRowIndex = (paginator.PageIndex * paginator.PageSize) - paginator.PageSize + 1;
            paginator.EndRowIndex = paginator.StartRowIndex + paginator.PageSize > paginator.RowCount
                ? paginator.RowCount
                : paginator.StartRowIndex - 1 + paginator.PageSize;

            //挂起路由事件
            paginator.RaiseEvent(new RoutedEventArgs(Paginator._RefreshEvent, paginator));
        }
        #endregion

        #region 总记录数改变回调方法 —— static void OnRowCountChanged(...
        /// <summary>
        /// 总记录数改变回调方法
        /// </summary>
        private static void OnRowCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Paginator paginator = (Paginator)dependencyObject;
            paginator.EndRowIndex = paginator.StartRowIndex + paginator.PageSize > paginator.RowCount
                ? paginator.RowCount
                : paginator.StartRowIndex - 1 + paginator.PageSize;
        }
        #endregion

        #region 总页数改变回调方法 —— static void OnPageCountChanged(...
        /// <summary>
        /// 总页数改变回调方法
        /// </summary>
        private static void OnPageCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Paginator paginator = (Paginator)dependencyObject;
            if (paginator.PageIndex > paginator.PageCount)
            {
                paginator.PageIndex = paginator.PageCount;
            }
        }
        #endregion

        #region 起始行改变回调方法 —— static void OnStartRowIndexChanged(...
        /// <summary>
        /// 起始行改变回调方法
        /// </summary>
        private static void OnStartRowIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            Paginator paginator = (Paginator)dependencyObject;
            paginator.EndRowIndex = paginator.StartRowIndex + paginator.PageSize > paginator.RowCount
                ? paginator.RowCount
                : paginator.StartRowIndex - 1 + paginator.PageSize;
        }
        #endregion

        #endregion

        #region # 路由事件

        #region 刷新数据路由事件 —— RoutedEvent Refresh

        /// <summary>
        /// 刷新数据路由事件
        /// </summary>
        private static readonly RoutedEvent _RefreshEvent;

        /// <summary>
        /// 刷新数据路由事件处理程序
        /// </summary>
        public event RoutedEventHandler Refresh
        {
            add { base.AddHandler(Paginator._RefreshEvent, value); }
            remove { base.RemoveHandler(Paginator._RefreshEvent, value); }
        }

        #endregion

        #endregion

        #region # 事件处理程序

        #region 页码文本框输入事件 —— void TxtPageIndexTextInput(...
        /// <summary>
        /// 页码文本框输入事件
        /// </summary>
        private void TxtPageIndexTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int _);
        }
        #endregion

        #region 页码文本框失去焦点事件 —— void TxtPageIndexLostFocus(...
        /// <summary>
        /// 页码文本框失去焦点事件
        /// </summary>
        private void TxtPageIndexLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            this.PageIndex = int.Parse(textBox.Text);
        }
        #endregion

        #region 页码文本框按回车键事件 —— void TxtPageIndexKeyDown(...
        /// <summary>
        /// 页码文本框按回车键事件
        /// </summary>
        private void TxtPageIndexKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Return))
            {
                TextBox textBox = (TextBox)sender;
                this.PageIndex = int.Parse(textBox.Text);
            }
        }
        #endregion

        #region 第一页按钮点击事件 —— void BtnFirstPageClick(...
        /// <summary>
        /// 第一页按钮点击事件
        /// </summary>
        private void BtnFirstPageClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex == Paginator.MinPageIndex)
            {
                return;
            }

            this.PageIndex = Paginator.MinPageIndex;
        }
        #endregion

        #region 上一页按钮点击事件 —— void BtnPrevPageClick(...
        /// <summary>
        /// 上一页按钮点击事件
        /// </summary>
        private void BtnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex <= Paginator.MinPageIndex)
            {
                this.PageIndex = Paginator.MinPageIndex;
                return;
            }

            this.PageIndex--;
        }
        #endregion

        #region 下一页按钮点击事件 —— void BtnNextPageClick(...
        /// <summary>
        /// 下一页按钮点击事件
        /// </summary>
        private void BtnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex >= this.PageCount)
            {
                this.PageIndex = this.PageCount;
                return;
            }

            this.PageIndex++;
        }
        #endregion

        #region 最后一页按钮点击事件 —— void BtnLastPageClick(...
        /// <summary>
        /// 最后一页按钮点击事件
        /// </summary>
        private void BtnLastPageClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex == this.PageCount)
            {
                return;
            }

            this.PageIndex = this.PageCount;
        }
        #endregion

        #region 刷新按钮点击事件 —— void BtnRefreshClick(...
        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void BtnRefreshClick(object sender, RoutedEventArgs e)
        {
            base.RaiseEvent(new RoutedEventArgs(Paginator._RefreshEvent, this));
        }
        #endregion

        #endregion
    }
}
