using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace SD.Infrastructure.WPF.CustomControls
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

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            this.Tag = null;
            base.OnItemsChanged(e);
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
