using System.Windows;
using System.Windows.Controls;

namespace SD.Infrastructure.WPF.Controls
{
    /// <summary>
    /// TreeListView
    /// </summary>
    public class TreeListView : TreeView
    {
        public TreeListView()
        {
            this.Loaded += (sender, _) => ((TreeListView)sender).Tag = null;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }
    }
}
